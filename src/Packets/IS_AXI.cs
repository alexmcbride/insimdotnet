using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// AutoX info packet.
    /// </summary>
    /// <remarks>
    /// Contains information about the current AutoX layout. To request one of these to 
    /// be sent send a <see cref="IS_TINY"/> with a SubT of TINY_AXI.
    /// </remarks>
    public class IS_AXI : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet request ID. Always zero unless a reply to an TINY_AXI request.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the autocross start position.
        /// </summary>
        public byte AXStart { get; private set; }

        /// <summary>
        /// Gets the number of checkpoints.
        /// </summary>
        public byte NumCP { get; private set; }

        /// <summary>
        /// Gets the number of objects.
        /// </summary>
        public int NumO { get; private set; }

        /// <summary>
        /// Gets the name of the layout last loaded (if loaded locally).
        /// </summary>
        public string LName { get; private set; }

        /// <summary>
        /// Creates a new AutoX info packet.
        /// </summary>
        public IS_AXI() {
            Size = 40;
            Type = PacketType.ISP_AXI;
            LName = String.Empty;
        }

        /// <summary>
        /// Creates a new AutoX info packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_AXI(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            AXStart = reader.ReadByte();
            NumCP = reader.ReadByte();
            NumO = reader.ReadUInt16();
            LName = reader.ReadString(32);
        }
    }
}
