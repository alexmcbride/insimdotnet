using System;
using System.Collections.ObjectModel;

namespace InSimDotNet {
    /// <summary>
    /// Provides data for the PacketDataReceived event.
    /// </summary>
    public class PacketDataEventArgs : EventArgs {
        private readonly byte[] buffer;

        /// <summary>
        /// Creates a new instance of the <see cref="PacketDataEventArgs"/> class.
        /// </summary>
        /// <param name="buffer">An array of bytes containing the packet data.</param>
        public PacketDataEventArgs(byte[] buffer) {
            this.buffer = buffer;
        }

        /// <summary>
        /// Returns the packet buffer.
        /// </summary>
        /// <returns>An array of bytes containing the packet data.</returns>
        public byte[] GetBuffer() {
            return buffer;
        }
    }
}
