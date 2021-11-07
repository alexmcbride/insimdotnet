using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Camera change packet.
    /// </summary>
    /// <remarks>
    /// Sent when a user changes camera.
    /// </remarks>
    public class IS_CCH : IPacket {
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
        /// Gets the unique ID of the player who changed cameras.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the view identifier of the current player.
        /// </summary>
        public ViewIndentifier Camera { get; private set; }

        /// <summary>
        /// Creates a new camera change packet.
        /// </summary>
        public IS_CCH() {
            Size = 8;
            Type = PacketType.ISP_CCH;
        }

        /// <summary>
        /// Creates a new camera change packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_CCH(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            Camera = (ViewIndentifier)reader.ReadByte();
        }
    }
}
