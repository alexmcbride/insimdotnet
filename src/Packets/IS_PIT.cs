using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Pit stop packet.
    /// </summary>
    /// <remarks>
    /// Sent when player completes a pit stop.
    /// </remarks>
    public class IS_PIT : IPacket {
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
        /// Gets the laps completed by the player.
        /// </summary>
        public int LapsDone { get; private set; }

        /// <summary>
        /// Gets the player flags.
        /// </summary>
        public PlayerFlags Flags { get; private set; }

        /// <summary>
        /// Gets the players current penalty value.
        /// </summary>
        public PenaltyValue Penalty { get; private set; }

        /// <summary>
        /// Gets the number of pit stops completed by the player.
        /// </summary>
        public byte NumStops { get; private set; }

        /// <summary>
        /// Gets the tyres changed during the pit stop.
        /// </summary>
        public Tyres Tyres { get; private set; }

        /// <summary>
        /// Gets the work completed during the pit stop.
        /// </summary>
        public PitWorkFlags Work { get; private set; }

        /// <summary>
        /// Creates a new pit stop packet.
        /// </summary>
        public IS_PIT() {
            Size = 24;
            Type = PacketType.ISP_PIT;
        }

        /// <summary>
        /// Creates a new pit stop packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_PIT(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            LapsDone = reader.ReadUInt16();
            Flags = (PlayerFlags)reader.ReadUInt16();
            reader.Skip(1);
            Penalty = (PenaltyValue)reader.ReadByte();
            NumStops = reader.ReadByte();
            reader.Skip(1);
            Tyres = new Tyres(
                (TyreCompound)reader.ReadByte(),
                (TyreCompound)reader.ReadByte(),
                (TyreCompound)reader.ReadByte(),
                (TyreCompound)reader.ReadByte());
            Work = (PitWorkFlags)reader.ReadUInt32();
        }
    }
}
