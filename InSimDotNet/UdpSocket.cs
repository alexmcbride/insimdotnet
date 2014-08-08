using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace InSimDotNet {
    /// <summary>
    /// Manages a UDP connection with LFS.
    /// </summary>
    public class UdpSocket : IDisposable {
        private const int BufferSize = 512;
        private readonly Socket socket;
        private bool isDisposed;

        /// <summary>
        /// Occurs when packet data is received.
        /// </summary>
        public event EventHandler<PacketDataEventArgs> PacketDataReceived;

        /// <summary>
        /// Occurs when the connection is lost.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        /// Occurs when an internal socket error occurs.
        /// </summary>
        public event EventHandler<InSimErrorEventArgs> SocketError;

        /// <summary>
        /// Gets if InSim is connected.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets the host the socket is connected to.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Gets the port the socket is connected to.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Exposes the underlying socket.
        /// </summary>
        protected Socket Socket {
            get { return socket; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UdpSocket"/> class.
        /// </summary>
        public UdpSocket() {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <summary>
        /// Disposes the <see cref="UdpSocket"/>.
        /// </summary>
        public void Dispose() {
            if (!isDisposed) {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Disposes the <see cref="UdpSocket"/> object.
        /// </summary>
        /// <param name="disposing">Set true to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (!isDisposed && disposing) {
                IsConnected = false;
                isDisposed = true;
                ((IDisposable)socket).Dispose();
            }
        }

        /// <summary>
        /// Binds the connection to LFS.
        /// </summary>
        /// <param name="host">The host where LFS is running.</param>
        /// <param name="port">The port to bind to the host through.</param>
        public void Bind(string host, int port) {
            ThrowIfDisposed();
            ThrowIfConnected();

            Host = host;
            Port = port;

            IsConnected = true;

            socket.Bind(new IPEndPoint(IPAddress.Parse(host), port));

            HandleReceive();
        }

        /// <summary>
        /// Disconnects from LFS.
        /// </summary>
        public void Disconnect() {
            if (IsConnected && socket != null) {
                IsConnected = false;

                socket.Close();
            }
        }

        /// <summary>
        /// Sends byte data to LFS.
        /// </summary>
        /// <param name="buffer">The data to send.</param>
        public void Send(byte[] buffer) {
            ThrowIfDisposed();

            socket.Send(buffer);
        }

        private void HandleReceive() {
            HandleReceive(new SocketState(socket, new byte[BufferSize]));
        }

        private void HandleReceive(SocketState state) {
            if (IsConnected) {
                state.Socket.BeginReceive(
                    state.Buffer,
                    0,
                    state.Buffer.Length,
                    SocketFlags.None,
                    ReceiveCallback,
                    state);
            }
        }

        /// <summary>
        /// Ends a pending asynchronous receive operation.
        /// </summary>
        /// <param name="asyncResult">The result of the receive operation.</param>
        private void ReceiveCallback(IAsyncResult asyncResult) {
            SocketState state = (SocketState)asyncResult.AsyncState;

            if (IsConnected) {
                try {
                    int bytesReceived = state.Socket.EndReceive(asyncResult);

                    if (bytesReceived == 0) {
                        Disconnect();
                        OnConnectionLost(EventArgs.Empty);
                    }
                    else {
                        state.Offset = bytesReceived;
                        HandlePacket(state);
                        HandleReceive(state);
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(String.Format(CultureInfo.CurrentCulture, StringResources.UdpSocketDebugErrorMessage, ex));
                    Disconnect();
                    OnSocketError(new InSimErrorEventArgs(ex));
                }
            }
        }

        private void HandlePacket(SocketState state) {
            // If size not multiple of four, packet is corrupt.
            if (state.Offset % 4 > 0) {
                throw new InSimException(StringResources.PacketSizeErrorMessage);
            }

            byte[] buffer = new byte[state.Offset];
            Buffer.BlockCopy(state.Buffer, 0, buffer, 0, state.Offset);
            OnPacketDataReceived(new PacketDataEventArgs(buffer));
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

        /// <summary>
        /// Raises the PacketDataReceived event.
        /// </summary>
        /// <param name="e">The <see cref="PacketDataEventArgs"/> object containing the event data</param>
        protected virtual void OnPacketDataReceived(PacketDataEventArgs e) {
            EventHandler<PacketDataEventArgs> temp = PacketDataReceived;
            if (temp != null) {
                temp(this, e);
            }
        }

        /// <summary>
        /// Raises the ConnectionLost event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object containing the event data</param>
        protected virtual void OnConnectionLost(EventArgs e) {
            EventHandler temp = ConnectionLost;
            if (temp != null) {
                temp(this, e);
            }
        }

        /// <summary>
        /// Raises the SocketError event.
        /// </summary>
        /// <param name="e">The <see cref="InSimErrorEventArgs"/> object containing the event data</param>
        protected virtual void OnSocketError(InSimErrorEventArgs e) {
            EventHandler<InSimErrorEventArgs> temp = SocketError;
            if (temp != null) {
                temp(this, e);
            }
        }
    }
}
