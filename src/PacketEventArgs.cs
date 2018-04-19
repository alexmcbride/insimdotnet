using System;
using InSimDotNet.Packets;

namespace InSimDotNet {
    /// <summary>
    /// Provides data for the <see cref="InSim"/> PacketReceived event.
    /// </summary>
    public class PacketEventArgs : EventArgs {
        /// <summary>
        /// Gets the packet.
        /// </summary>
        public IPacket Packet { get; private set; }

        /// <summary>
        /// Gets or sets if the <see cref="IPacket"/> has been handled. If set to true no packet bindings will be raised.
        /// </summary>
        public bool IsHandled { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="PacketEventArgs"/> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        public PacketEventArgs(IPacket packet) {
            Packet = packet;
        }
    }
}
