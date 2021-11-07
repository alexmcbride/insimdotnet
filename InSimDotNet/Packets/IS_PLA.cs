using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Pit lane packet.
    /// </summary>
    /// <remarks>
    /// Sent when player enters or leaves pit lane.
    /// </remarks>
    public class IS_PLA : IPacket {
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
        /// Gets if the player has entered or left the pit lane.
        /// </summary>
        public PitLaneFact Fact { get; private set; }

        /// <summary>
        /// Creates a new pit lane packet.
        /// </summary>
        public IS_PLA() {
            Size = 8;
            Type = PacketType.ISP_PLA;
        }

        /// <summary>
        /// Creates a new pit lane packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_PLA(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            Fact = (PitLaneFact)reader.ReadByte();
        }
    }
}
