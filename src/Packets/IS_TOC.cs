using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Take over car packet.
    /// </summary>
    /// <remarks>
    /// Sent when a player takes over another players car.
    /// </remarks>
    public class IS_TOC : IPacket {
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
        /// Gets the old connection ID of the player.
        /// </summary>
        public byte OldUCID { get; private set; }

        /// <summary>
        /// Gets new connection ID of the player.
        /// </summary>
        public byte NewUCID { get; private set; }

        /// <summary>
        /// Creates a new take over car packet.
        /// </summary>
        public IS_TOC() {
            Size = 8;
            Type = PacketType.ISP_TOC;
        }

        /// <summary>
        /// Creates a new take over car packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_TOC(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            OldUCID = reader.ReadByte();
            NewUCID = reader.ReadByte();
        }
    }
}
