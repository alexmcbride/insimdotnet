using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace InSimDotNet {
    /// <summary>
    /// Manages a TCP connection with LFS.
    /// </summary>
    public class TcpSocket : IDisposable {
        private const int BufferSize = 2048;

        private readonly TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[BufferSize];
        private int offset;
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
            get { return client.Connected; }
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
            get { return client.Client; }
        }

        /// <summary>
        /// Gets the total number of bytes sent by this socket.
        /// </summary>
        public int BytesSent { get; private set; }

        /// <summary>
        /// Gets the total number of bytes received by this socket.
        /// </summary>
        public int BytesReceived { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="TcpSocket"/> class.
        /// </summary>
        public TcpSocket() {
            client = new TcpClient();
            client.NoDelay = true;
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

                if (stream != null) {
                    stream.Dispose();
                }

                if (client != null) {
                    client.Close();
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

            client.Connect(host, port);
            stream = client.GetStream();

            BytesSent = 0;
            BytesReceived = 0;

            ReceiveAsync();
        }

        /// <summary>
        /// Disconnects from LFS.
        /// </summary>
        public void Disconnect() {
            ThrowIfDisposed();
            ThrowIfNotConnected();

            if (stream != null) {
                stream.Dispose();
            }

            client.Close();
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

            stream.Write(buffer, 0, buffer.Length);
            BytesSent += buffer.Length;
        }

        private async void ReceiveAsync() {
            if (client.Connected) {
                try {
                    int count = await stream.ReadAsync(buffer, offset, buffer.Length - offset);

                    if (count == 0) {
                        Disconnect();
                        OnConnectionLost(EventArgs.Empty);
                    }
                    else {
                        BytesReceived += count;
                        offset += count;
                        HandlePackets();
                        ReceiveAsync();
                    }
                }
                catch (Exception ex) {
                    Debug.WriteLine(String.Format(StringResources.TcpSocketDebugErrorMessage, ex));
                    Disconnect();
                    OnSocketError(new InSimErrorEventArgs(ex));
                }
            }
        }

        private void HandlePackets() {
            int read = 0;

            while (offset > 0 && offset >= buffer[read]) {
                int size = buffer[read];

                // If size not multiple of four packet is corrupt.
                if (size % 4 > 0) {
                    throw new InSimException(StringResources.PacketSizeErrorMessage);
                }

                // Raise packet event.
                byte[] temp = new byte[size];
                Buffer.BlockCopy(buffer, read, temp, 0, size);
                OnPacketDataReceived(new PacketDataEventArgs(temp));

                // Move indices to next packet.
                offset -= size;
                read += size;
            }

            // Copy leftover bytes to front of buffer.
            if (offset > 0) {
                Buffer.BlockCopy(
                    buffer,
                    read,
                    buffer,
                    0,
                    buffer.Length - read);
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
