using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// State packet.
    /// </summary>
    /// <remarks>
    /// Sent when LFS state changes. To request one to be sent send a 
    /// <see cref="IS_TINY"/> with a SubT of TINY_SST.
    /// </remarks>
    public class IS_STA : IPacket {
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
        /// Gets the current replay speed.
        /// </summary>
        public float ReplaySpeed { get; private set; }

        /// <summary>
        /// Gets the state flags.
        /// </summary>
        public StateFlags Flags { get; private set; }

        /// <summary>
        /// Gets the current view.
        /// </summary>
        public ViewIndentifier InGameCam { get; private set; }

        /// <summary>
        /// Gets the unique ID of the currently viewed player.
        /// </summary>
        public byte ViewPLID { get; private set; }

        /// <summary>
        /// Gets the number of players in the race.
        /// </summary>
        public byte NumP { get; private set; }

        /// <summary>
        /// Gets the number of connections on the host.
        /// </summary>
        public byte NumConns { get; private set; }

        /// <summary>
        /// Gets the number of players who have finished the race.
        /// </summary>
        public byte NumFinished { get; private set; }

        /// <summary>
        /// Gets if the race is in progress (0 - no race / 1 - race / 2 - qualifying).
        /// </summary>
        public byte RaceInProg { get; private set; }

        /// <summary>
        /// Gets the qualifying minutes of the race.
        /// </summary>
        public byte QualMins { get; private set; }

        /// <summary>
        /// Gets the race laps.
        /// </summary>
        public byte RaceLaps { get; private set; }

        /// <summary>
        /// Gets the current track.
        /// </summary>
        public string Track { get; private set; }

        /// <summary>
        /// Gets the current weather.
        /// </summary>
        public byte Weather { get; private set; }

        /// <summary>
        /// Gets the current wind.
        /// </summary>
        public byte Wind { get; private set; }

        /// <summary>
        /// Creates a new state packet.
        /// </summary>
        public IS_STA() {
            Size = 28;
            Type = PacketType.ISP_STA;
            Track = String.Empty;
        }

        /// <summary>
        /// Creates a new state packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_STA(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            ReplaySpeed = reader.ReadSingle();
            Flags = (StateFlags)reader.ReadUInt16();
            InGameCam = (ViewIndentifier)reader.ReadByte();
            ViewPLID = reader.ReadByte();
            NumP = reader.ReadByte();
            NumConns = reader.ReadByte();
            NumFinished = reader.ReadByte();
            RaceInProg = reader.ReadByte();
            QualMins = reader.ReadByte();
            RaceLaps = reader.ReadByte();
            reader.Skip(2);
            Track = reader.ReadString(6);
            Weather = reader.ReadByte();
            Wind = reader.ReadByte();
        }
    }
}
