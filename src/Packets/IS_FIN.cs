using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Finished race notification packet.
    /// </summary>
    /// <remarks>
    /// Sent when player crosses the finish line but before final result has been 
    /// confirmed. For the final result use the <see cref="IS_RES"/> result packet.
    /// </remarks>
    public class IS_FIN : IPacket {
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
        /// Gets the unique ID of the player.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the total race time of the player.
        /// </summary>
        public TimeSpan TTime { get; private set; }

        /// <summary>
        /// Gets the best lap time of the player.
        /// </summary>
        public TimeSpan BTime { get; private set; }

        /// <summary>
        /// Gets the number of pit stops completed by the player.
        /// </summary>
        public byte NumStops { get; private set; }

        /// <summary>
        /// Gets the players confirmation flags.
        /// </summary>
        public ConfirmationFlags Confirm { get; private set; }

        /// <summary>
        /// Gets the number of laps completed by the player.
        /// </summary>
        public int LapsDone { get; private set; }

        /// <summary>
        /// Gets the player flags.
        /// </summary>
        public PlayerFlags Flags { get; private set; }

        /// <summary>
        /// Creates a new finished race notification packet.
        /// </summary>
        public IS_FIN() {
            Size = 20;
            Type = PacketType.ISP_FIN;
        }

        /// <summary>
        /// Creates a new finished race notification packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_FIN(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            TTime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            BTime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            reader.Skip(1);
            NumStops = reader.ReadByte();
            Confirm = (ConfirmationFlags)reader.ReadByte();
            reader.Skip(1);
            LapsDone = reader.ReadUInt16();
            Flags = (PlayerFlags)reader.ReadUInt16();
        }
    }
}
