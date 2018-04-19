using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Split time packet.
    /// </summary>
    /// <remarks>
    /// Sent when a player completes a split.
    /// </remarks>
    public class IS_SPX : IPacket {
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
        /// Gets the split time.
        /// </summary>
        public TimeSpan STime { get; private set; }

        /// <summary>
        /// Gets the total elapsed time.
        /// </summary>
        public TimeSpan ETime { get; private set; }

        /// <summary>
        /// Gets the split number.
        /// </summary>
        public byte Split { get; private set; }

        /// <summary>
        /// Gets the currently penalty value of the player.
        /// </summary>
        public PenaltyValue Penalty { get; private set; }

        /// <summary>
        /// Gets the number of pit stops the player has completed.
        /// </summary>
        public byte NumStops { get; private set; }

        /// <summary>
        /// Creates a new split time packet.
        /// </summary>
        public IS_SPX() {
            Size = 16;
            Type = PacketType.ISP_SPX;
        }

        /// <summary>
        /// Creates a new split time packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_SPX(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            STime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            ETime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            Split = reader.ReadByte();
            Penalty = (PenaltyValue)reader.ReadByte();
            NumStops = reader.ReadByte();
        }
    }
}
