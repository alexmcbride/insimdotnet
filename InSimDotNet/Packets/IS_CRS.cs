using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Car reset packet.
    /// </summary>
    /// <remarks>
    /// Sent when player resets car (pressed space).
    /// </remarks>
    public class IS_CRS : IPacket {
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
        /// Creates a new car reset packet.
        /// </summary>
        public IS_CRS() {
            Size = 4;
            Type = PacketType.ISP_CRS;
        }

        /// <summary>
        /// Creates a new car reset packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_CRS(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
        }
    }
}
