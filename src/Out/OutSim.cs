using System;
using System.Threading;

namespace InSimDotNet.Out {
    /// <summary>
    /// Class to manage a OutSim connection with LFS.
    /// </summary>
    public class OutSim : OutClient {
        /// <summary>
        /// Occurs when a OutSim packet is received.
        /// </summary>
        public event EventHandler<OutSimEventArgs> PacketReceived;

        /// <summary>
        /// Creates a new instance of the <see cref="OutClient"/> class.
        /// </summary>
        public OutSim()
            : this(TimeSpan.Zero) { }

        /// <summary>
        /// Creates a new instance of the <see cref="OutSim"/> class with the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout period in milliseconds.</param>
        public OutSim(int timeout)
            : this(TimeSpan.FromMilliseconds(timeout)) { }

        /// <summary>
        /// Creates a new instance of the <see cref="OutSim"/> class with the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout period</param>
        public OutSim(TimeSpan timeout)
            : base(timeout) { }

        /// <summary>
        /// Called when packet data is received. Override to implement handling for specific packets.
        /// </summary>
        /// <param name="buffer">The packet data.</param>
        protected override void HandlePacket(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            if (buffer.Length == OutSimPack.MinSize || buffer.Length == OutSimPack.MaxSize) {
                OutSimPack packet = new OutSimPack(buffer);
                OnPacketReceived(new OutSimEventArgs(packet));
            }
        }

        /// <summary>
        /// Raises the PacketReceived event.
        /// </summary>
        /// <param name="e">The <see cref="OutSimEventArgs"/> object containing the event data.</param>
        protected virtual void OnPacketReceived(OutSimEventArgs e) {
            EventHandler<OutSimEventArgs> temp = PacketReceived;
            if (temp != null) {
                temp(this, e);
            }
        }
    }
}
