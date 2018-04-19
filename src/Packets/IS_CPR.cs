using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Connection player rename packet.
    /// </summary>
    /// <remarks>
    /// Sent when a player is renamed.
    /// </remarks>
    public class IS_CPR : IPacket {
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
        /// Gets the unique ID of the connection being renamed.
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets the new name of the player.
        /// </summary>
        public string PName { get; private set; }

        /// <summary>
        /// Gets the new number plate of the player.
        /// </summary>
        public string Plate { get; private set; }

        /// <summary>
        /// Creates a new connection player rename packet.
        /// </summary>
        public IS_CPR() {
            Size = 36;
            Type = PacketType.ISP_CPR;
            PName = String.Empty;
            Plate = String.Empty;
        }

        /// <summary>
        /// Creates a new connection player rename packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_CPR(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            PName = reader.ReadString(24);
            Plate = reader.ReadString(8);
        }
    }
}
