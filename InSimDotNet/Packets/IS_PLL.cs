using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Player leave packet.
    /// </summary>
    /// <remarks>
    /// Sent when player leaves race (spectated, removed from player list).
    /// </remarks>
    public class IS_PLL : IPacket {
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
        /// Creates a new player leave packet.
        /// </summary>
        public IS_PLL() {
            Size = 4;
            Type = PacketType.ISP_PLL;
        }

        /// <summary>
        /// Creates a new player leave packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_PLL(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
        }
    }
}
