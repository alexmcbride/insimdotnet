using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace InSimDotNet
{
    /// <summary>
    /// Manages connecting to LFS using the InSim protocol.
    /// </summary>
    public partial class InSimClient : IDisposable
    {
        /// <summary>
        /// Gets the current InSim version.
        /// </summary>
        public byte InSimVersion = 9;
        private const string RelayHost = "isrelay.lfs.net";
        private const int RelayPort = 47474;

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
        /// Gets if LFS is connected.
        /// </summary>
        public bool IsConnected => TcpSocket.IsConnected;

        /// <summary>
        /// Gets a read-only version of the <see cref="InSimSettings"/> used to initialize the 
        /// connection with LFS.
        /// </summary>
        public InSimSettings Settings { get; private set; }

        /// <summery>
        /// Gets or sets whether all packets should be built.
        /// </summery>
        protected bool BuildAllPackets { get; set; }

        /// <summary>
        /// Gets the total number of bytes sent to LFS.
        /// </summary>
        public long BytesSent => TcpSocket.BytesSent + UdpSocket.BytesSent;

        /// <summary>
        /// Gets the total number of bytes received from LFS.
        /// </summary>
        public long BytesReceived => TcpSocket.BytesReceived + UdpSocket.BytesReceived;

        /// <summary>
        /// Gets the underlying TcpSocket used to communicate with LFS.
        /// </summary>
        public TcpSocket TcpSocket { get; private set; }

        /// <summary>
        /// Gets the underlying UdpSocket used to communicate with Lfs.
        /// </summary>
        public UdpSocket UdpSocket { get; private set; }

        /// <summary>
        /// Gets or sets whether packet handlers should be marshalled back onto the original context.
        /// </summary>
        public bool ContinueOnCapturedContext
        {
            get => TcpSocket.ContinueOnCapturedContext && UdpSocket.ContinueOnCapturedContext;
            set
            {
                TcpSocket.ContinueOnCapturedContext = value;
                UdpSocket.ContinueOnCapturedContext = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="InSimClient"/> class.
        /// </summary>
        public InSimClient()
        {
            InitializeSockets();
            LfsEncoding.Initialize();
            IS_TINY += InSimClient_IS_TINY;
        }


        /// <summary>
        /// Releases all resources used by the connection.
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Releases all resources used by the connection.
        /// </summary>
        /// <param name="disposing">Set true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                isDisposed = true;

                if (TcpSocket != null)
                {
                    TcpSocket.Dispose();
                }

                if (UdpSocket != null)
                {
                    UdpSocket.Dispose();
                }
            }
        }

        /// <summary>
        /// Initialize InSim asynchronously.
        /// </summary>
        /// <param name="settings">The InSim settings.</param>
        /// <returns>A task object.</returns>
        public async Task InitializeAsync(InSimSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            ThrowIfDisposed();
            ThrowIfConnected();

            try
            {
                InitializeSockets();

                Settings = settings;
                if (Settings.IsRelayHost)
                {
                    await TcpSocket.ConnectAsync(RelayHost, RelayPort);
                }
                else
                {
                    await TcpSocket.ConnectAsync(Settings.Host, Settings.Port);

                    await SendAsync(new IS_ISI
                    {
                        Admin = Settings.Admin,
                        Flags = Settings.Flags,
                        IName = Settings.IName,
                        Interval = Settings.Interval,
                        Prefix = Settings.Prefix,
                        ReqI = 1, // Request IS_VER sent after connect.
                        UDPPort = Settings.UdpPort,
                        InSimVer = InSimVersion, // request latest InSim version
                    });

                    // If UDP port set then init UDP connection
                    if (Settings.UdpPort > 0)
                    {
                        UdpSocket.Bind(Settings.Host, Settings.UdpPort);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    throw new InSimException("Could not connect to InSim", ex);
                }
                throw;
            }
        }

        private void InitializeSockets()
        {
            if (TcpSocket is { IsDisposed: true })
            {
                // Cleanup old socket.
                TcpSocket.PacketDataReceived -= Socket_PacketDataReceived;
                TcpSocket.ConnectionLost -= TcpSocket_ConnectionLost;
                TcpSocket.SocketError -= TcpSocket_SocketError;
            }

            TcpSocket = new TcpSocket();
            TcpSocket.PacketDataReceived += Socket_PacketDataReceived;
            TcpSocket.ConnectionLost += TcpSocket_ConnectionLost;
            TcpSocket.SocketError += TcpSocket_SocketError;

            if (UdpSocket is { IsDisposed: true })
            {
                // Cleanup of old socket.
                UdpSocket.PacketDataReceived -= Socket_PacketDataReceived;
                UdpSocket.SocketError -= UdpSocket_SocketError;
            }

            UdpSocket = new UdpSocket();
            UdpSocket.PacketDataReceived += Socket_PacketDataReceived;
            UdpSocket.SocketError += UdpSocket_SocketError;
        }

        /// <summary>
        /// Disconnects from LFS and releases all resources associated with the connection.
        /// </summary>
        public void Disconnect()
        {
            ThrowIfDisposed();
            ThrowIfNotConnected();

            TcpSocket.Disconnect();
            UdpSocket.Disconnect();

            OnDisconnected(new DisconnectedEventArgs(DisconnectReason.Request));
        }

        /// <summary>
        /// Sends the specified packet to LFS asynchronously.
        /// </summary>
        /// <param name="packet">The<see cref="ISendable"/> packet to send.</param>
        /// <returns>An awaitable async task object.</returns>
        public Task SendAsync(ISendable packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException("packet");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            return TcpSocket.SendAsync(packet.GetBuffer());
        }

        /// <summary>
        /// Sends the specified sequence of packets to LFS asynchronously.
        /// </summary>
        /// <param name="packets">The sequence of <see cref="ISendable"/> packets to send.</param>
        /// <returns>An awaitable async task object.</returns>
        public Task SendAsync(params ISendable[] packets)
        {
            if (packets == null)
            {
                throw new ArgumentNullException("packets");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            return TcpSocket.SendAsync(GetSendBuffer(packets));
        }

        private static byte[] GetSendBuffer(ISendable[] packets)
        {
            int size = packets.Sum(p => p.Size);
            List<byte> buffer = new List<byte>(size);
            foreach (var packet in packets)
            {
                buffer.AddRange(packet.GetBuffer());
            }
            return buffer.ToArray();
        }

        /// <summary>
        /// Sends a message or command to LFS asynchronously.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="args">Arguments to format the message with.</param>
        /// <returns>An awaitable async task object.</returns>
        public Task SendAsync(string message, params object[] args)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            return SendAsync(GetMessagePacket(message, args));
        }

        private ISendable GetMessagePacket(string message, object[] args)
        {
            const int MsxLen = 96;
            const int MstLen = 64;
            const string CommandPrefix = "/";

            message = String.Format(message, args);
            if (message.StartsWith(CommandPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return new IS_MST { Msg = message }; // Send command.
            }
            else
            {
                // We need to know the length the string will be once converted into bytes so we 
                // know which packet to send, so we just convert it here.
                byte[] buffer = new byte[MsxLen];
                int length = LfsEncoding.Current.GetBytes(message, buffer, 0, MsxLen);
                if (length < MstLen)
                {
                    return new IS_MST(buffer.Take(MstLen).ToArray()); // Send normal message (MST expects shorter array).
                }
                else
                {
                    return new IS_MSX(buffer); // Send extended message.
                }
            }
        }

        /// <summary>
        /// Sends a message to a specific connection asynchronously.
        /// </summary>
        /// <param name="ucid">The ID of the connection to send the message to.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="args">An object array containing zero or more objects to format the message with.</param>
        /// <returns>An awaitable async task object.</returns>
        public Task SendAsync(byte ucid, string message, params object[] args)
        {
            return SendAsync(ucid, 0, message, args);
        }

        /// <summary>
        /// Sends a message to a specific connection, of if the ucid is 0 to a specific player.
        /// </summary>
        /// <param name="ucid">The ID of the connection to send the message to.</param>
        /// <param name="plid">The ID of the player to send the message to.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="args">Arguments to format the message with.</param>
        /// <returns>An awaitable async task object.</returns>
        public Task SendAsync(byte ucid, byte plid, string message, params object[] args)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            message = string.Format(message, args);
            return SendAsync(new IS_MTC { Msg = message, PLID = plid, UCID = ucid });
        }

        private void Socket_PacketDataReceived(object sender, PacketDataEventArgs e)
        {
            HandlePacketEvent(e.GetBuffer());
        }

        private async void InSimClient_IS_TINY(object sender, PacketEventArgs<IS_TINY> e)
        {        // Handle keep alive
            if (e.Packet.SubT == TinyType.TINY_NONE)
            {
                await SendAsync(e.Packet);
            }
        }

        private void TcpSocket_ConnectionLost(object sender, EventArgs e)
        {
            UdpSocket.Disconnect();
            OnDisconnected(new DisconnectedEventArgs(DisconnectReason.Lost));
        }

        private void TcpSocket_SocketError(object sender, InSimErrorEventArgs e)
        {
            UdpSocket.Disconnect();
            OnInSimError(e);
        }

        private void UdpSocket_SocketError(object sender, InSimErrorEventArgs e)
        {
            TcpSocket.Disconnect();
            OnInSimError(e);
        }

        [DebuggerStepThrough]
        private void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().ToString());
            }
        }

        [DebuggerStepThrough]
        private void ThrowIfConnected()
        {
            if (IsConnected)
            {
                throw new InSimException("InSim is already connected");
            }
        }

        [DebuggerStepThrough]
        private void ThrowIfNotConnected()
        {
            if (!IsConnected)
            {
                throw new InSimException("InSim is not connected");
            }
        }

        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">The <see cref="InitializeEventArgs"/> object containing the event data</param>
        protected virtual void OnInitialized(InitializeEventArgs e)
        {
            Initialized?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the Disconnected event.
        /// </summary>
        /// <param name="e">The <see cref="DisconnectedEventArgs"/> object containing the event data</param>
        protected virtual void OnDisconnected(DisconnectedEventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the InSimError event.
        /// </summary>
        /// <param name="e">The <see cref="InSimErrorEventArgs"/> object containing the event data</param>
        protected virtual void OnInSimError(InSimErrorEventArgs e)
        {
            InSimError?.Invoke(this, e);
        }
    }
}
