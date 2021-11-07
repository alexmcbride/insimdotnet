namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents an InSim packet.
    /// </summary>
    public interface IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        byte Size { get; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        PacketType Type { get; }

        /// <summary>
        /// Gets the packet request ID.
        /// </summary>
        byte ReqI { get; }
    }
}
