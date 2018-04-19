using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Player pits packet.
    /// </summary>
    /// <remarks>
    /// Sent when player pits (goes to garage screen).
    /// </remarks>
    public class IS_PLP : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the unique ID of the player.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Creates a new player pits packet.
        /// </summary>
        public IS_PLP() {
            Size = 4;
            Type = PacketType.ISP_PLP;
        }

        /// <summary>
        /// Creates a new player pits packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_PLP(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
        }
    }
}
