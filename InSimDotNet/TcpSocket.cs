using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace InSimDotNet
{
    /// <summary>
    /// Manages a TCP connection with LFS.
    /// </summary>
    public class TcpSocket : IDisposable
    {
        private const int BufferSize = 8192;

        private readonly TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[BufferSize];
        private int bufferBytes;

        /// <summary>
        /// Occurs when packet data is received.
        /// </summary>
        public event EventHandler<PacketDataEventArgs> PacketDataReceived;

        /// <summary>
        /// Occurs when the connection is lost.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        /// Occurs when an error is thrown while dispatching a packet event.
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
        public Socket Socket {
            get { return client.Client; }
        }

        /// <summary>
        /// Gets the total number of bytes sent by this socket.
        /// </summary>
        public long BytesSent { get; private set; }

        /// <summary>
        /// Gets the total number of bytes received by this socket.
        /// </summary>
        public long BytesReceived { get; private set; }

        /// <summary>
        /// Gets or sets whether events should be marshalled back onto the original context.
        /// </summary>
        public bool ContinueOnCapturedContext { get; set; }

        /// <summary>
        /// Gets if the object is disposed
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="TcpSocket"/> class.
        /// </summary>
        public TcpSocket()
        {
            client = new TcpClient();
            client.NoDelay = true; // Disable Nagle algorithm, which increases bandwidth but lowers latency.

            BytesSent = 0;
            BytesReceived = 0;
            ContinueOnCapturedContext = true;
        }

        /// <summary>
        /// Disposes the <see cref="TcpSocket"/> object.
        /// </summary>
        public void Dispose() {
            if (!IsDisposed) {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Disposes the <see cref="TcpSocket"/> object.
        /// </summary>
        /// <param name="disposing">Set true to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (!IsDisposed && disposing) {
                IsDisposed = true;

                if (stream != null) {
                    stream.Dispose();
                }

                client.Dispose();
            }
        }

        /// <summary>
        /// Establishes the TCP connection with LFS asynchronously.
        /// </summary>
        /// <param name="host">The host to connect to.</param>
        /// <param name="port">The port to connect to the host through.</param>
        /// <returns>An async task object.</returns>
        public async Task ConnectAsync(string host, int port) {
            ThrowIfDisposed();
            ThrowIfConnected();

            Host = host;
            Port = port;

            await client.ConnectAsync(host, port);
            stream = client.GetStream();

            ReceiveAsync(); // Start receiving packets.
        }

        /// <summary>
        /// Disconnects from LFS.
        /// </summary>
        public void Disconnect() {
            ThrowIfDisposed();
            ThrowIfNotConnected();

            Dispose();
        }


        /// <summary>
        /// Sends byte data to LFS asynchronously.
        /// </summary>
        /// <param name="buffer">The data to send.</param>
        /// <returns>An async task object.</returns>
        public async Task SendAsync(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            ThrowIfDisposed();
            ThrowIfNotConnected();

            // Keep sending until whole buffer sent.
            await stream.WriteAsync(buffer, 0, buffer.Length);
            BytesSent += buffer.Length;
        }

        // This is the main TCP receive code. TCP data arrives as a steam.
        private async void ReceiveAsync() {
            if (!stream.CanRead) {
                return;
            }

            try {
                // Read from socket into buffer.
                int count = await stream
                    .ReadAsync(buffer, bufferBytes, buffer.Length - bufferBytes)
                    .ConfigureAwait(ContinueOnCapturedContext);

                if (count == 0) {
                    // Connection Lost.
                    Dispose();
                    OnConnectionLost(EventArgs.Empty);
                }
                else {
                    // Handle received packets.
                    BytesReceived += count;
                    bufferBytes += count;

                    HandlePackets();

                    // Start receiving next batch of packets.
                    ReceiveAsync();
                }
            }
            catch (ObjectDisposedException) {
                // Do nothing... this gets thrown if Dispose is called while waiting for a read to complete.
            }
            catch (Exception ex) {
                Debug.WriteLine(String.Format("TCP Receive Error: {0}", ex));
                Dispose();
                OnSocketError(new InSimErrorEventArgs(ex));
            }
        }

        private void HandlePackets() {
            int read = 0;

            // Loop through all completed packets in the buffer.
            while (bufferBytes > 0 && bufferBytes >= buffer[read]) {
                int size = buffer[read] * 4;
                
                // Raise packet event.
                byte[] temp = new byte[size];
                Buffer.BlockCopy(buffer, read, temp, 0, size);
                OnPacketDataReceived(new PacketDataEventArgs(temp));

                // Move to next packet.
                bufferBytes -= size;
                read += size;
            }

            // Copy any leftover bytes to front of buffer.
            if (bufferBytes > 0) {
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
            if (IsDisposed) {
                throw new ObjectDisposedException(GetType().ToString());
            }
        }

        [DebuggerStepThrough]
        private void ThrowIfConnected() {
            if (IsConnected) {
                throw new InSimException("InSim is already connected");
            }
        }

        [DebuggerStepThrough]
        private void ThrowIfNotConnected() {
            if (!IsConnected) {
                throw new InSimException("InSim is not connected");
            }
        }

        /// <summary>
        /// Raises the PacketDataReceived event.
        /// </summary>
        /// <param name="e">The <see cref="PacketDataEventArgs"/> object containing the event data</param>
        protected virtual void OnPacketDataReceived(PacketDataEventArgs e) {
            PacketDataReceived?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the ConnectionLost event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object containing the event data</param>
        protected virtual void OnConnectionLost(EventArgs e) {
            ConnectionLost?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the SocketError event.
        /// </summary>
        /// <param name="e">The <see cref="InSimErrorEventArgs"/> object containing the event data</param>
        protected virtual void OnSocketError(InSimErrorEventArgs e) {
            SocketError?.Invoke(this, e);
        }
    }
}
