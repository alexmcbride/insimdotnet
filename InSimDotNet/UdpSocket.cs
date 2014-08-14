using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace InSimDotNet {
    /// <summary>
    /// Manages a UDP connection with LFS.
    /// </summary>
    public class UdpSocket : IDisposable {
        private const int BufferSize = 512;

        private readonly UdpClient client;
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
        /// Creates a new instance of the <see cref="UdpSocket"/> class.
        /// </summary>
        public UdpSocket() {
            client = new UdpClient();
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

            IsConnected = true;

            client.Connect(host, port);

            BytesSent = 0;
            BytesReceived = 0;

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

        private async void ReceiveAsync() {
            if (IsConnected) {
                try {
                    UdpReceiveResult result = await client.ReceiveAsync();

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
                    throw;
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
