using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using InSimDotNet.Packets;

namespace InSimDotNet {
    /// <summary>
    /// Manages connecting to LFS using the InSim protocol.
    /// </summary>
    public class InSim : IDisposable {
        /// <summary>
        /// Gets the current InSim version. This is a constant field.
        /// </summary>
        public const int InSimVersion = 5;
        private const string RelayHost = "isrelay.lfs.net";
        private const int RelayPort = 47474;

        private readonly TcpSocket tcpSocket;
        private readonly UdpSocket udpSocket;
        private readonly BindingManager bindings = new BindingManager();
        private bool isDisposed;

        /// <summary>
        /// Occurs when InSim is initialized.
        /// </summary>
        public event EventHandler<InitializeEventArgs> Initialized;

        /// <summary>
        /// Occurs when InSim is disconnected.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Occurs when an exception is thrown on the internal receive thread.
        /// </summary>
        public event EventHandler<InSimErrorEventArgs> InSimError;

        /// <summary>
        /// Occurs when a packet is received.
        /// </summary>
        public event EventHandler<PacketEventArgs> PacketReceived;

        /// <summary>
        /// Gets if LFS is connected.
        /// </summary>
        public bool IsConnected {
            get { return tcpSocket == null ? false : tcpSocket.IsConnected; }
        }

        /// <summary>
        /// Gets a read-only version of the <see cref="InSimSettings"/> used to initialize the 
        /// connection with LFS.
        /// </summary>
        public ReadOnlyInSimSettings Settings { get; private set; }

        /// <summery>
        /// Gets or sets whether all packets should be built.
        /// </summery>
        protected bool BuildAllPackets { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="InSim"/> class.
        /// </summary>
        public InSim() {
            tcpSocket = new TcpSocket();
            tcpSocket.PacketDataReceived += tcpSocket_PacketDataReceived;
            tcpSocket.ConnectionLost += tcpSocket_ConnectionLost;
            tcpSocket.SocketError += tcpSocket_SocketError;

            udpSocket = new UdpSocket();
            udpSocket.PacketDataReceived += udpSocket_PacketDataReceived;
            udpSocket.SocketError += udpSocket_SocketError;
        }

        /// <summary>
        /// Releases all resources used by the connection.
        /// </summary>
        public void Dispose() {
            if (!isDisposed) {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Releases all resources used by the connection.
        /// </summary>
        /// <param name="disposing">Set true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (!isDisposed && disposing) {
                isDisposed = true;

                // Dispose TCP socket.
                tcpSocket.Dispose();
                tcpSocket.PacketDataReceived -= tcpSocket_PacketDataReceived;
                tcpSocket.ConnectionLost -= tcpSocket_ConnectionLost;
                tcpSocket.SocketError -= tcpSocket_SocketError;

                // Dispose UDP socket.
                udpSocket.Dispose();
                udpSocket.PacketDataReceived -= udpSocket_PacketDataReceived;
                udpSocket.SocketError -= udpSocket_SocketError;
            }
        }

        /// <summary>
        /// Initializes the connection with LFS.
        /// </summary>
        /// <param name="settings">A <see cref="InSimSettings"/> object containing information to initialize the connection with.</param>
        public void Initialize(InSimSettings settings) {
            if (settings == null) {
                throw new ArgumentNullException("settings");
            }

            ThrowIfDisposed();
            ThrowIfConnected();

            Settings = new ReadOnlyInSimSettings(settings);

            try {
                if (Settings.IsRelayHost) {
                    tcpSocket.Connect(RelayHost, RelayPort);
                }
                else {
                    tcpSocket.Connect(Settings.Host, Settings.Port);

                    tcpSocket.Send(new IS_ISI {
                        Admin = Settings.Admin,
                        Flags = Settings.Flags,
                        IName = Settings.IName,
                        Interval = Settings.Interval,
                        Prefix = Settings.Prefix,
                        ReqI = 1, // Request version.
                        UDPPort = Settings.UdpPort,
                    }.GetBuffer());

                    if (Settings.UdpPort > 0) {
                        udpSocket.Bind(Settings.Host, Settings.UdpPort);
                    }
                }

                OnInitialized(new InitializeEventArgs(Settings));
            }
            catch (SocketException ex) {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused) {
                    throw new InSimException(StringResources.InSimCouldNotConnectMessage, ex);
                }
                throw;
            }
        }

        /// <summary>
        /// Disconnects from LFS and releases all resources associated with the connection.
        /// </summary>
        public void Disconnect() {
            ThrowIfDisposed();
            ThrowIfNotConnected();

            tcpSocket.Disconnect();
            udpSocket.Disconnect();

            OnDisconnected(new DisconnectedEventArgs(DisconnectReason.Request));
        }

        /// <summary>
        /// Sends the specified packet to LFS.
        /// </summary>
        /// <param name="packet">The<see cref="ISendable"/> packet to send.</param>
        public void Send(ISendable packet) {
            if (packet == null) {
                throw new ArgumentNullException("packet");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            tcpSocket.Send(packet.GetBuffer());
        }

        /// <summary>
        /// Sends the specified sequence of packets to LFS.
        /// </summary>
        /// <param name="packets">The sequence of <see cref="ISendable"/> packets to send.</param>
        public void Send(IEnumerable<ISendable> packets) {
            if (packets == null) {
                throw new ArgumentNullException("packets");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            int size = packets.Sum(p => p.Size);
            List<byte> buffer = new List<byte>(size);
            foreach (ISendable packet in packets) {
                buffer.AddRange(packet.GetBuffer());
            }
            tcpSocket.Send(buffer.ToArray());
        }

        /// <summary>
        /// Sends a message or command to LFS.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="args">Arguments to format the message with.</param>
        public void Send(string message, params object[] args) {
            if (message == null) {
                throw new ArgumentNullException("message");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            message = String.Format(CultureInfo.CurrentCulture, message, args);
            if (message.StartsWith("/", StringComparison.OrdinalIgnoreCase)) {
                Send(new IS_MST { Msg = message }); // Send command.
            }
            else {
                // Get the length the string will be once it's encoded.
                int length = LfsEncoding.GetByteCount(message, 96);
                if (length < 64) {
                    Send(new IS_MST { Msg = message }); // Send normal message.
                }
                else {
                    Send(new IS_MSX { Msg = message }); // Send extended message.
                }
            }
        }

        /// <summary>
        /// Sends a message to a specific connection.
        /// </summary>
        /// <param name="ucid">The ID of the connection to send the message to.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="args">An object array containing zero or more objects to format the message with.</param>
        public void Send(byte ucid, string message, params object[] args) {
            Send(ucid, 0, message, args);
        }

        /// <summary>
        /// Sends a message to a specific connection, of if the ucid is 0 to a specific player.
        /// </summary>
        /// <param name="ucid">The ID of the connection to send the message to.</param>
        /// <param name="plid">The ID of the player to send the message to.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="args">Arguments to format the message with.</param>
        public void Send(byte ucid, byte plid, string message, params object[] args) {
            if (message == null) {
                throw new ArgumentNullException("message");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            message = String.Format(CultureInfo.CurrentCulture, message, args);
            Send(new IS_MTC { Msg = message, PLID = plid, UCID = ucid });
        }

        /// <summary>
        /// Binds a packet callback with this InSim connection.
        /// </summary>
        /// <typeparam name="TPacket">The type of <see cref="IPacket"/> to bind.</typeparam>
        /// <param name="callback">The method to invoke when the packet is received.</param>
        public void Bind<TPacket>(PacketHandler<TPacket> callback) where TPacket : IPacket {
            ThrowIfDisposed();

            PacketType packetType = PacketFactory.PacketLookup(typeof(TPacket));
            bindings.Bind<TPacket>(packetType, callback);
        }

        /// <summary>
        /// Binds a packet callback with this InSim connection.
        /// </summary>
        /// <param name="packetType">The type of the packet to bind.</param>
        /// <param name="callback">The method to invoke when the packet is received.</param>
        public void Bind(PacketType packetType, PacketHandler callback) {
            ThrowIfDisposed();

            bindings.Bind(packetType, callback);
        }

        /// <summary>
        /// Unbinds a packet callback from this InSim connection.
        /// </summary>
        /// <typeparam name="TPacket">The type of <see cref="IPacket"/> to unbind.</typeparam>
        /// <param name="callback">The method to unbind.</param>
        public void Unbind<TPacket>(PacketHandler<TPacket> callback)
            where TPacket : IPacket {
            ThrowIfDisposed();

            PacketType packetType = PacketFactory.PacketLookup(typeof(TPacket));
            bindings.Unbind<TPacket>(packetType, callback);
        }

        /// <summary>
        /// Unbinds a packet callback from this InSim connection.
        /// </summary>
        /// <param name="packetType">The type of packet to unbind</param>
        /// <param name="callback">The method to unbind.</param>
        public void Unbind(PacketType packetType, PacketHandler callback) {
            ThrowIfDisposed();

            bindings.Unbind(packetType, callback);
        }

        /// <summary>
        /// Determines if a packet callback has been bound with this InSim connection.
        /// </summary>
        /// <typeparam name="TPacket">The type of <see cref="IPacket"/> to check.</typeparam>
        /// <param name="callback">The method to check.</param>
        /// <returns>True if the method has been bound.</returns>
        public bool IsBound<TPacket>(PacketHandler<TPacket> callback)
            where TPacket : IPacket {
            ThrowIfDisposed();

            PacketType packetType = PacketFactory.PacketLookup(typeof(TPacket));
            return bindings.IsBound<TPacket>(packetType, callback);
        }

        /// <summary>
        /// Determines if a packet callback has been bound with this InSim connection.
        /// </summary>
        /// <param name="packetType">The type of packet to check.</param>
        /// <param name="callback">The method to check.</param>
        /// <returns>True if the method has been bound.</returns>
        public bool IsBound(PacketType packetType, PacketHandler callback) {
            ThrowIfDisposed();

            return bindings.IsBound(packetType, callback);
        }

        private bool IsPacketEventNeeded(PacketType type) {
            return BuildAllPackets || PacketReceived != null || bindings.IsBound(type);
        }

        private void RaisePacketEvent(IPacket packet) {
            PacketEventArgs e = new PacketEventArgs(packet);

            OnPacketReceived(e);

            if (!e.IsHandled) {
                bindings.ExecuteCallbacks(this, packet);
            }
        }

        private void HandleKeepAlive(PacketType type, byte[] buffer) {
            if (type == PacketType.ISP_TINY) {
                if ((TinyType)buffer[3] == TinyType.TINY_NONE) {
                    tcpSocket.Send(buffer);
                }
            }
        }

        private void tcpSocket_PacketDataReceived(object sender, PacketDataEventArgs e) {
            byte[] buffer = e.GetBuffer();
            PacketType type = (PacketType)buffer[1];

            if (IsPacketEventNeeded(type)) {
                IPacket packet = PacketFactory.BuildPacket(buffer);

                if (packet != null) {
                    RaisePacketEvent(packet);
                }
            }

            HandleKeepAlive(type, buffer);
        }

        private void tcpSocket_ConnectionLost(object sender, EventArgs e) {
            udpSocket.Disconnect();
            OnDisconnected(new DisconnectedEventArgs(DisconnectReason.Lost));
        }

        private void tcpSocket_SocketError(object sender, InSimErrorEventArgs e) {
            udpSocket.Disconnect();
            OnInSimError(e);
        }

        private void udpSocket_PacketDataReceived(object sender, PacketDataEventArgs e) {
            byte[] buffer = e.GetBuffer();
            PacketType type = (PacketType)buffer[1];

            if ((type == PacketType.ISP_MCI || type == PacketType.ISP_NLP) && IsPacketEventNeeded(type)) {
                IPacket packet = PacketFactory.BuildPacket(buffer);

                if (packet != null) {
                    RaisePacketEvent(packet);
                }
            }
        }

        private void udpSocket_SocketError(object sender, InSimErrorEventArgs e) {
            tcpSocket.Disconnect();
            OnInSimError(e);
        }

        [DebuggerStepThrough]
        private void ThrowIfDisposed() {
            if (isDisposed) {
                throw new ObjectDisposedException(GetType().ToString());
            }
        }

        [DebuggerStepThrough]
        private void ThrowIfConnected() {
            if (IsConnected) {
                throw new InSimException(StringResources.InSimConnectedMessage);
            }
        }

        [DebuggerStepThrough]
        private void ThrowIfNotConnected() {
            if (!IsConnected) {
                throw new InSimException(StringResources.InSimNotConnectedMessage);
            }
        }

        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">The <see cref="InitializeEventArgs"/> object containing the event data</param>
        protected virtual void OnInitialized(InitializeEventArgs e) {
            EventHandler<InitializeEventArgs> temp = Initialized;
            if (temp != null) {
                temp(this, e);
            }
        }

        /// <summary>
        /// Raises the Disconnected event.
        /// </summary>
        /// <param name="e">The <see cref="DisconnectedEventArgs"/> object containing the event data</param>
        protected virtual void OnDisconnected(DisconnectedEventArgs e) {
            EventHandler<DisconnectedEventArgs> temp = Disconnected;
            if (temp != null) {
                temp(this, e);
            }
        }

        /// <summary>
        /// Raises the InSimError event.
        /// </summary>
        /// <param name="e">The <see cref="InSimErrorEventArgs"/> object containing the event data</param>
        protected virtual void OnInSimError(InSimErrorEventArgs e) {
            EventHandler<InSimErrorEventArgs> temp = InSimError;
            if (temp != null) {
                temp(this, e);
            }
        }

        /// <summary>
        /// Raises the PacketReceived event.
        /// </summary>
        /// <param name="e">The <see cref="PacketEventArgs"/> object containing the event data</param>
        protected virtual void OnPacketReceived(PacketEventArgs e) {
            EventHandler<PacketEventArgs> temp = PacketReceived;
            if (temp != null) {
                temp(this, e);
            }
        }
    }
}
