using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// General purpose packet eight byte packet.
    /// </summary>
    public class IS_SMALL : IPacket, ISendable {
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
        /// Gets or sets the packet sub-type.
        /// </summary>
        public SmallType SubT { get; set; }

        /// <summary>
        /// Gets or sets the packet value.
        /// </summary>
        public long UVal { get; set; }

        /// <summary>
        /// Creates a new general purpose packet.
        /// </summary>
        public IS_SMALL() {
            Size = 8;
            Type = PacketType.ISP_SMALL;
        }

        /// <summary>
        /// Creates a new general purpose packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_SMALL(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            SubT = (SmallType)reader.ReadByte();
            UVal = reader.ReadUInt32();
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
            writer.Write((uint)UVal);
            return writer.GetBuffer();
        }
    }
}
