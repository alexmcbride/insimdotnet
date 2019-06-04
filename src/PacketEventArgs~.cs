using System;
using InSimDotNet.Packets;

namespace InSimDotNet {
    /// <summary>
    /// Provides data for the <see cref="InSimClient"/> PacketReceived event.
    /// </summary>
    public class PacketEventArgs<T> : EventArgs where T : IPacket {
        /// <summary>
        /// Gets the packet.
        /// </summary>
        public T Packet { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="PacketEventArgs"/> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketEventArgs(T packet) {
            Packet = packet;
        }
    }
}
