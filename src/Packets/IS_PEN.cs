using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Penalty packet.
    /// </summary>
    /// <remarks>
    /// Sent when penalty given or cleared.
    /// </remarks>
    public class IS_PEN : IPacket {
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
        /// Gets the old penalty value.
        /// </summary>
        public PenaltyValue OldPen { get; private set; }

        /// <summary>
        /// Gets the new penalty value.
        /// </summary>
        public PenaltyValue NewPen { get; private set; }

        /// <summary>
        /// Gets the reason for the penalty.
        /// </summary>
        public PenaltyReason Reason { get; private set; }

        /// <summary>
        /// Creates a new penalty packet.
        /// </summary>
        public IS_PEN() {
            Size = 8;
            Type = PacketType.ISP_PEN;
        }

        /// <summary>
        /// Creates a new penalty packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_PEN(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            OldPen = (PenaltyValue)reader.ReadByte();
            NewPen = (PenaltyValue)reader.ReadByte();
            Reason = (PenaltyReason)reader.ReadByte();
        }
    }
}
