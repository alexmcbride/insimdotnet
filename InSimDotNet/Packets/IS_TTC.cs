namespace InSimDotNet.Packets {
    /// <summary>
    /// General purpose packet eight byte packet.
    /// </summary>
    public class IS_TTC : IPacket, ISendable
    {
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
        public TtcType SubT { get; set; }

        /// <summary>
        /// Gets or sets the connection's unique id (0 = local).
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets or sets a value that may be used in various ways depending on SubT.
        /// </summary>
        public byte B1 { get; set; }

        /// <summary>
        /// Gets or sets a value that may be used in various ways depending on SubT.
        /// </summary>
        public byte B2 { get; set; }

        /// <summary>
        /// Gets or sets a value that may be used in various ways depending on SubT.
        /// </summary>
        public byte B3 { get; set; }

        /// <summary>
        /// Creates a new general purpose packet.
        /// </summary>
        public IS_TTC()
        {
            Size = 8;
            Type = PacketType.ISP_TTC;
        }

        /// <summary>
        /// Creates a new general purpose packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_TTC(byte[] buffer)
            : this()
        {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            SubT = (TtcType)reader.ReadByte();
            UCID = reader.ReadByte();
            B1 = reader.ReadByte();
            B2 = reader.ReadByte();
            B3 = reader.ReadByte();
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer()
        {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)SubT);
            writer.Write(UCID);
            writer.Write(B1);
            writer.Write(B2);
            writer.Write(B3);
            return writer.GetBuffer();
        }
    }
}
