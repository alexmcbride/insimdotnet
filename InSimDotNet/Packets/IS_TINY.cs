using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// General purpose packet four-byte packet.
    /// </summary>
    public class IS_TINY : IPacket, ISendable {
        /// <summary>
        /// Gets the packet size.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the packet type.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the sub-type.
        /// </summary>
        public TinyType SubT { get; set; }

        /// <summary>
        /// Creates a new general purpose packet.
        /// </summary>
        public IS_TINY() {
            Size = 4;
            Type = PacketType.ISP_TINY;
        }

        /// <summary>
        /// Creates a new general purpose packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_TINY(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            SubT = (TinyType)reader.ReadByte();
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)SubT);
            return writer.GetBuffer();
        }
    }
}
