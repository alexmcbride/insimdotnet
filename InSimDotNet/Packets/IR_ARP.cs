
namespace InSimDotNet.Packets {
    /// <summary>
    /// Admin response packet.
    /// </summary>
    /// <remarks>
    /// Sent in reply to an <see cref="IR_ARQ"/> admin request.
    /// </remarks>
    public class IR_ARP : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets if you are an admin on the host.
        /// </summary>
        public bool Admin { get; private set; }

        /// <summary>
        /// Creates a new admin response packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IR_ARP(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            Admin = reader.ReadBoolean();
        }
    }
}
