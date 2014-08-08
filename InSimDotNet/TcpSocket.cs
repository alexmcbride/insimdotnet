using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;

namespace InSimDotNet {
    /// <summary>
    /// Manages a TCP connection with LFS.
    /// </summary>
    public class TcpSocket : IDisposable {
        private const int BufferSize = 2048;
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
        /// Gets if the socket is connected.
        /// </summary>
        public bool IsConnected {
            get { return socket.Connected; }
        }

        /// <summary>
        /// Gets the host the socket is connected to.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Gets the port the socket is connected to.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Gets the underlying socket.
        /// </summary>
        protected Socket Socket {
            get { return socket; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TcpSocket"/> class.
        /// </summary>
        public TcpSocket() {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = true;
        }

        /// <summary>
        /// Disposes the <see cref="TcpSocket"/> object.
        /// </summary>
        public void Dispose() {
            if (!isDisposed) {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Disposes the <see cref="TcpSocket"/> object.
        /// </summary>
        /// <param name="disposing">Set true to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (!isDisposed && disposing) {
                isDisposed = true;
                if (socket != null) {
                    ((IDisposable)socket).Dispose();
                }
            }
        }

        /// <summary>
        /// Establishes the TCP connection with LFS.
        /// </summary>
        /// <param name="host">The host to connect to.</param>
        /// <param name="port">The port to connect to the host through.</param>
        public void Connect(string host, int port) {
            ThrowIfDisposed();
            ThrowIfConnected();

            Host = host;
            Port = port;

            socket.Connect(host, port);

            HandleReceive();
        }

        /// <summary>
        /// Disconnects from LFS.
        /// </summary>
        public void Disconnect() {
            ThrowIfDisposed();
            ThrowIfNotConnected();

            socket.Close();
        }

        /// <summary>
        /// Sends byte data to LFS.
        /// </summary>
        /// <param name="buffer">The data to send.</param>
        public void Send(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            HandleSend(new SocketState(socket, buffer));
        }

        private void HandleSend(SocketState state) {
            if (state.Socket.Connected) {
                state.Socket.BeginSend(
                      state.Buffer,
                      state.Offset,
                      state.Buffer.Length - state.Offset,
                      SocketFlags.None,
                      SendCallback,
                      state);
            }
        }

        private void SendCallback(IAsyncResult asyncResult) {
            SocketState state = (SocketState)asyncResult.AsyncState;

            try {
                int sent = state.Socket.EndSend(asyncResult);

                // If full buffer not sent, resend.
                state.Offset += sent;
                if (state.Offset < state.Buffer.Length) {
                    HandleSend(state);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine("TCP Send Error: " + ex);
                state.Socket.Close();
                OnSocketError(new InSimErrorEventArgs(ex));
            }
        }

        private void HandleReceive() {
            HandleReceive(new SocketState(socket, new byte[BufferSize]));
        }

        private void HandleReceive(SocketState state) {
            if (state.Socket.Connected) {
                state.Socket.BeginReceive(
                    state.Buffer,
                    state.Offset,
                    state.Buffer.Length - state.Offset,
                    SocketFlags.None,
                    ReceiveCallback,
                    state);
            }
        }

        private void ReceiveCallback(IAsyncResult asyncResult) {
            SocketState state = (SocketState)asyncResult.AsyncState;

            if (state.Socket.Connected) {
                try {
                    int bytesReceived = state.Socket.EndReceive(asyncResult);

                    if (bytesReceived == 0) {
                        state.Socket.Close();
                        OnConnectionLost(EventArgs.Empty);
                    }
                    else {
                        state.Offset += bytesReceived;
                        HandlePackets(state);
                        HandleReceive(state);
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(String.Format(CultureInfo.CurrentCulture, StringResources.TcpSocketDebugErrorMessage, ex));
                    state.Socket.Close();
                    OnSocketError(new InSimErrorEventArgs(ex));
                }
            }
        }

        private void HandlePackets(SocketState state) {
            int read = 0;

            while (state.Offset > 0 && state.Offset >= state.Buffer[read]) {
                int size = state.Buffer[read];

                // If size not multiple of four, packet is corrupt.
                if (size % 4 > 0) {
                    throw new InSimException(StringResources.PacketSizeErrorMessage);
                }

                // Raise packet event.
                byte[] buffer = new byte[size];
                Buffer.BlockCopy(state.Buffer, read, buffer, 0, size);
                OnPacketDataReceived(new PacketDataEventArgs(buffer));

                // Move indices to next packet.
                state.Offset -= size;
                read += size;
            }

            // Copy leftover bytes to front of buffer.
            if (state.Offset > 0) {
                Buffer.BlockCopy(
                    state.Buffer,
                    read,
                    state.Buffer,
                    0,
                    state.Buffer.Length - read);
            }
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
