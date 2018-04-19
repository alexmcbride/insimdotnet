using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Admin request packet.
    /// </summary>
    /// <remarks>
    ///  When sent LFS will respond with a <see cref="IR_ARP"/> admin response packet.
    /// </remarks>
    public class IR_ARQ : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Creates a new admin request packet.
        /// </summary>
        public IR_ARQ() {
            Size = 4;
            Type = PacketType.IRP_ARQ;
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write((byte)Size);
            writer.Write((byte)Type);
            writer.Write((byte)ReqI);
            return writer.GetBuffer();
        }
    }
}
