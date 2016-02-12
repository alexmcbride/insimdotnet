using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace InSimDotNet {
    /// <summary>
    /// Manages a UDP connection with LFS.
    /// </summary>
    public class UdpSocket : IDisposable {
        private const int BufferSize = 256;

        private readonly UdpClient client;

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
        /// Gets if the object is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="UdpSocket"/> class.
        /// </summary>
        public UdpSocket() {
            client = new UdpClient(); 
            
            BytesSent = 0;
            BytesReceived = 0;
            ContinueOnCapturedContext = true;
        }

        /// <summary>
        /// Disposes the <see cref="UdpSocket"/>.
        /// </summary>
        public void Dispose() {
            if (!IsDisposed) {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Disposes the <see cref="UdpSocket"/> object.
        /// </summary>
        /// <param name="disposing">Set true to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (!IsDisposed && disposing) {
                IsConnected = false;
                IsDisposed = true;

                client.Close();
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

            client.Client.Bind(new IPEndPoint(IPAddress.Parse(host), port));
            IsConnected = true;

            ReceiveAsync();
        }

        /// <summary>
        /// Disconnects from LFS.
        /// </summary>
        public void Disconnect() {
            Dispose();
        }

        /// <summary>
        /// Sends byte data to LFS.
        /// </summary>
        /// <param name="buffer">The data to send.</param>
        public void Send(byte[] buffer) {
            ThrowIfDisposed();

            client.Send(buffer, buffer.Length);
            BytesSent += buffer.Length;
        }

        /// <summary>
        /// Sends byte data to LFS asynchronously.
        /// </summary>
        /// <param name="buffer">The data to send.</param>
        /// <returns>An async task object.</returns>
        public async Task SendAsync(byte[] buffer) {
            ThrowIfDisposed();

            await client.SendAsync(buffer, buffer.Length);
            BytesSent += buffer.Length;
        }

        private async void ReceiveAsync() {
            if (IsConnected) {
                try {
                    UdpReceiveResult result = await client
                        .ReceiveAsync()
                        .ConfigureAwait(ContinueOnCapturedContext);

                    if (result.Buffer.Length == 0) {
                        Disconnect();
                        OnConnectionLost(EventArgs.Empty);
                    }
                    else {
                        BytesReceived += result.Buffer.Length;
                        HandlePacket(result.Buffer);
                        ReceiveAsync();
                    }
                }
                catch (ObjectDisposedException) {
                    // Do nothing... this gets thrown if Dispose is called while waiting for a read to complete.
                }
                catch (Exception ex) {
                    Debug.WriteLine(String.Format(StringResources.UdpSocketDebugErrorMessage, ex));
                    Dispose();
                    OnSocketError(new InSimErrorEventArgs(ex));
                }
            }
        }

        private void HandlePacket(byte[] buffer) {
            // If size not multiple of four, packet is corrupt.
            if (buffer.Length % 4 > 0) {
                throw new InSimException(StringResources.PacketSizeErrorMessage);
            }

            OnPacketDataReceived(new PacketDataEventArgs(buffer));
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
