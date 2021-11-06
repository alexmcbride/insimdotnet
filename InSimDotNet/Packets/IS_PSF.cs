using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Pit stop finished.
    /// </summary>
    /// <remarks>
    /// Sent when player completes a pit stop.
    /// </remarks>
    public class IS_PSF : IPacket {
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
        /// Gets the time of the pit stop.
        /// </summary>
        public TimeSpan STime { get; private set; }

        /// <summary>
        /// Creates a new pit stop finished.
        /// </summary>
        public IS_PSF() {
            Size = 12;
            Type = PacketType.ISP_PSF;
        }

        /// <summary>
        /// Creates a new pit stop finished.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_PSF(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            STime = TimeSpan.FromMilliseconds(reader.ReadUInt32());
        }
    }
}
