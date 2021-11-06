using System;
using System.Diagnostics;
using System.Threading;

namespace InSimDotNet.Out {
    /// <summary>
    /// Abstract class for managing OutSim and OutGauge UDP connections with LFS.
    /// </summary>
    public abstract class OutClient : IDisposable {
        private readonly UdpSocket udpSocket;
        private Timer timeoutTimer;
        private bool isDisposed;

        /// <summary>
        /// Occurs when the connection times out.
        /// </summary>
        public event EventHandler TimedOut;

        /// <summary>
        /// Occurs when an error occurs on the internal receive thread.
        /// </summary>
        public event EventHandler<OutErrorEventArgs> OutError;

        /// <summary>
        /// Gets if the socket is connected.
        /// </summary>
        public bool IsConnected
        {
            get { return timeoutTimer != null; }
        }

        /// <summary>
        /// Gets the amount of time to wait before timing out.
        /// </summary>
        public TimeSpan Timeout { get; private set; }

        /// <summary>
        /// Gets or sets whether packet handlers should be marshalled back onto the original context.
        /// </summary>
        public bool ContinueOnCapturedContext {
            get { return udpSocket.ContinueOnCapturedContext; }
            set { udpSocket.ContinueOnCapturedContext = value; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="OutClient"/> class.
        /// </summary>
        protected OutClient()
            : this(TimeSpan.Zero) { }

        /// <summary>
        /// Creates a new instance of the <see cref="OutClient"/> class with the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout period in milliseconds.</param>
        protected OutClient(int timeout)
            : this(TimeSpan.FromMilliseconds(timeout)) { }

        /// <summary>
        /// Creates a new instance of the <see cref="OutClient"/> class with the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout period for the socket.</param>
        protected OutClient(TimeSpan timeout) {
            udpSocket = new UdpSocket();
            udpSocket.PacketDataReceived += new EventHandler<PacketDataEventArgs>(udpSocket_PacketDataReceived);
            udpSocket.SocketError += new EventHandler<InSimErrorEventArgs>(udpSocket_SocketError);

            Timeout = timeout;
        }

        /// <summary>
        /// Starts listening for packets from LFS.
        /// </summary>
        /// <param name="host">The host to listen to.</param>
        /// <param name="port">The port to listen on.</param>
        public void Connect(string host, int port) {
            ThrowIfDisposed();

            udpSocket.Bind(host, port);

            if (Timeout > TimeSpan.Zero)
            {
                if (timeoutTimer != null)
                {
                    timeoutTimer.Dispose();
                }

                timeoutTimer = new Timer(TimerElasped, null, TimeSpan.Zero, Timeout);
            }
        }

        /// <summary>
        /// Disconnects from LFS.
        /// </summary>
        public void Disconnect() {
            ThrowIfDisposed();

            udpSocket.Disconnect();

            Dispose();
        }

        /// <summary>
        /// Disposes the connection.
        /// </summary>
        public void Dispose() {
            if (!isDisposed) {
                Dispose(true);

                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Disposes the connection.
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (!isDisposed && disposing) {
                isDisposed = true;

                udpSocket.Dispose();
                udpSocket.PacketDataReceived -= udpSocket_PacketDataReceived;
                udpSocket.SocketError -= udpSocket_SocketError;

                if (timeoutTimer != null)
                {
                    timeoutTimer.Dispose();
                    timeoutTimer = null;
                }
            }
        }

        private void udpSocket_PacketDataReceived(object sender, PacketDataEventArgs e) {
            HandlePacket(e.GetBuffer());

            if (timeoutTimer != null)
            {
                // Reset timer.
                timeoutTimer.Change(TimeSpan.Zero, Timeout);
            }
        }

        private void udpSocket_SocketError(object sender, InSimErrorEventArgs e) {
            Disconnect();
            OnOutError(new OutErrorEventArgs(e.Exception));
        }

        private void TimerElasped(object state) {
            Disconnect();

            OnTimedOut(EventArgs.Empty);
        }

        /// <summary>
        /// Called when packet data is received. Override to implement handling for specific packets.
        /// </summary>
        /// <param name="buffer">The packet data.</param>
        protected abstract void HandlePacket(byte[] buffer);

        [DebuggerStepThrough]
        private void ThrowIfDisposed() {
            if (isDisposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Raises the TimedOut event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object containing the event data.</param>
        protected virtual void OnTimedOut(EventArgs e) {
            EventHandler temp = TimedOut;
            if (temp != null) {
                temp(this, e);
            }
        }

        /// <summary>
        /// Raises the OutError event.
        /// </summary>
        /// <param name="e">The <see cref="OutErrorEventArgs"/> object containing the event data.</param>
        protected virtual void OnOutError(OutErrorEventArgs e) {
            EventHandler<OutErrorEventArgs> temp = OutError;
            if (temp != null) {
                temp(this, e);
            }
        }
    }
}
