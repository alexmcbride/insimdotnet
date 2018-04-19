using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Lap time packet.
    /// </summary>
    /// <remarks>
    /// Sent when a player completes a lap.
    /// </remarks>
    public class IS_LAP : IPacket {
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
        /// Gets the unique ID of the player who completed the lap.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the lap time of the player.
        /// </summary>
        public TimeSpan LTime { get; private set; }

        /// <summary>
        /// Gets the total elapsed race time of the player.
        /// </summary>
        public TimeSpan ETime { get; private set; }

        /// <summary>
        /// Gets the number of laps this player has done.
        /// </summary>
        public int LapsDone { get; private set; }

        /// <summary>
        /// Gets the flags for this player.
        /// </summary>
        public PlayerFlags Flags { get; private set; }

        /// <summary>
        /// Gets the current penalty of the player.
        /// </summary>
        public PenaltyValue Penalty { get; private set; }

        /// <summary>
        /// Gets the number of pit stops completed by the player.
        /// </summary>
        public byte NumStops { get; private set; }

        /// <summary>
        /// Creates a new lap time packet.
        /// </summary>
        public IS_LAP() {
            Size = 20;
            Type = PacketType.ISP_LAP;
        }

        /// <summary>
        /// Creates a new lap time packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_LAP(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            LTime = TimeSpan.FromMilliseconds(reader.ReadUInt32()); ;
            ETime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            LapsDone = reader.ReadUInt16();
            Flags = (PlayerFlags)reader.ReadUInt16();
            reader.Skip(1);
            Penalty = (PenaltyValue)reader.ReadByte();
            NumStops = reader.ReadByte();
        }
    }
}
