using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Race start packet.
    /// </summary>
    /// <remarks>
    /// Sent when a race is started or restarted. To request to be sent send a 
    /// <see cref="IS_TINY"/> with a SubT of TINY_RST.
    /// </remarks>
    public class IS_RST : IPacket {
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
        /// Gets the laps of the race (0 if qualifying).
        /// </summary>
        public byte RaceLaps { get; private set; }

        /// <summary>
        /// Gets the qualifying minutes of the race (0 if race).
        /// </summary>
        public byte QualMins { get; private set; }

        /// <summary>
        /// Gets the number of players in the race.
        /// </summary>
        public byte NumP { get; private set; }

        /// <summary>
        /// Gets the lap timing.
        /// </summary>
        public byte Timing { get; private set; }

        /// <summary>
        /// Gets the track.
        /// </summary>
        public string Track { get; private set; }

        /// <summary>
        /// Gets the weather of the race.
        /// </summary>
        public byte Weather { get; private set; }

        /// <summary>
        /// Gets the wind of the race. 
        /// </summary>
        public byte Wind { get; private set; }

        /// <summary>
        /// Gets the host flags.
        /// </summary>
        public RaceFlags Flags { get; private set; }

        /// <summary>
        /// Gets the number of nodes of the track.
        /// </summary>
        public int NumNodes { get; private set; }

        /// <summary>
        /// Gets the number of the finish line node.
        /// </summary>
        public int Finish { get; private set; }

        /// <summary>
        /// Gets the number of the split 1 node.
        /// </summary>
        public int Split1 { get; private set; }

        /// <summary>
        /// Gets the number of the split 2 node.
        /// </summary>
        public int Split2 { get; private set; }

        /// <summary>
        /// Gets the number of the split 3 node.
        /// </summary>
        public int Split3 { get; private set; }

        /// <summary>
        /// Creates a new race start packet.
        /// </summary>
        public IS_RST() {
            Size = 28;
            Type = PacketType.ISP_RST;
            Track = String.Empty;
        }

        /// <summary>
        /// Creates a new race start packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_RST(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            RaceLaps = reader.ReadByte();
            QualMins = reader.ReadByte();
            NumP = reader.ReadByte();
            Timing = reader.ReadByte();
            Track = reader.ReadString(6);
            Weather = reader.ReadByte();
            Wind = reader.ReadByte();
            Flags = (RaceFlags)reader.ReadUInt16();
            NumNodes = reader.ReadUInt16();
            Finish = reader.ReadUInt16();
            Split1 = reader.ReadUInt16();
            Split2 = reader.ReadUInt16();
            Split3 = reader.ReadUInt16();
        }
    }
}
