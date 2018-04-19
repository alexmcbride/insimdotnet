using System;
using System.Threading;

namespace InSimDotNet.Out {
    /// <summary>
    /// Class to manage a OutGauge connection with LFS.
    /// </summary>
    public class OutGauge : OutClient {
        /// <summary>
        /// Occurs when a OutGauge packet is received.
        /// </summary>
        public event EventHandler<OutGaugeEventArgs> PacketReceived;

        /// <summary>
        /// Creates a new instance of the <see cref="OutGauge"/> class.
        /// </summary>
        public OutGauge()
            : this(TimeSpan.Zero) { }

        /// <summary>
        /// Creates a new instance of the <see cref="OutGauge"/> class with the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout period in milliseconds.</param>
        public OutGauge(int timeout)
            : this(TimeSpan.FromMilliseconds(timeout)) { }

        /// <summary>
        /// Creates a new instance of the <see cref="OutGauge"/> class with the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout period</param>
        public OutGauge(TimeSpan timeout)
            : base(timeout) { }

        /// <summary>
        /// Called when packet data is received. Override to implement handling for specific packets.
        /// </summary>
        /// <param name="buffer">The packet data.</param>
        protected override void HandlePacket(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            if (buffer.Length == OutGaugePack.MinSize || buffer.Length == OutGaugePack.MaxSize) {
                OutGaugePack packet = new OutGaugePack(buffer);
                OnPacketReceived(new OutGaugeEventArgs(packet));
            }
        }

        /// <summary>
        /// Raises the PacketReceived event.
        /// </summary>
        /// <param name="e">The <see cref="OutGaugeEventArgs"/> object containing the event data.</param>
        protected virtual void OnPacketReceived(OutGaugeEventArgs e) {
            EventHandler<OutGaugeEventArgs> temp = PacketReceived;
            if (temp != null) {
                temp(this, e);
            }
        }
    }
}
