using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Result packet.
    /// </summary>
    /// <remarks>
    /// Sent when the players result is confirmed, either at race finished or 
    /// qualifying lap completed.
    /// </remarks>
    public class IS_RES : IPacket {
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
        /// Gets the username.
        /// </summary>
        public string UName { get; private set; }

        /// <summary>
        /// Gets the player name.
        /// </summary>
        public string PName { get; private set; }

        /// <summary>
        /// Gets the number plate of the car.
        /// </summary>
        public string Plate { get; private set; }

        /// <summary>
        /// Gets the car name.
        /// </summary>
        public string CName { get; private set; }

        /// <summary>
        /// Gets the total race time.
        /// </summary>
        public TimeSpan TTime { get; private set; }

        /// <summary>
        /// Gets the best lap time.
        /// </summary>
        public TimeSpan BTime { get; private set; }

        /// <summary>
        /// Gets the number of pit stops.
        /// </summary>
        public byte NumStops { get; private set; }

        /// <summary>
        /// Gets the confirmation flags.
        /// </summary>
        public ConfirmationFlags Confirm { get; private set; }

        /// <summary>
        /// Gets the laps completed.
        /// </summary>
        public int LapsDone { get; private set; }

        /// <summary>
        /// Gets the player flags.
        /// </summary>
        public PlayerFlags Flags { get; private set; }

        /// <summary>
        /// Gets the finish or qualify position (0 = win / 255 = not added to table).
        /// </summary>
        public byte ResultNum { get; private set; }

        /// <summary>
        /// Gets the total number of results.
        /// </summary>
        public byte NumRes { get; private set; }

        /// <summary>
        /// Gets the penalty seconds.
        /// </summary>
        public TimeSpan PSeconds { get; private set; }

        /// <summary>
        /// Creates a new result packet.
        /// </summary>
        public IS_RES() {
            Size = 84;
            Type = PacketType.ISP_RES;
            UName = String.Empty;
            PName = String.Empty;
            Plate = String.Empty;
            CName = String.Empty;
        }

        /// <summary>
        /// Creates a new result packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_RES(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            UName = reader.ReadString(24);
            PName = reader.ReadString(24);
            Plate = reader.ReadString(8);
            CName = reader.ReadString(4);
            TTime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            BTime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            reader.Skip(1);
            NumStops = reader.ReadByte();
            Confirm = (ConfirmationFlags)reader.ReadByte();
            reader.Skip(1);
            LapsDone = reader.ReadUInt16();
            Flags = (PlayerFlags)reader.ReadUInt16();
            ResultNum = reader.ReadByte();
            NumRes = reader.ReadByte();
            PSeconds = TimeSpan.FromSeconds(reader.ReadUInt16());
        }
    }
}
