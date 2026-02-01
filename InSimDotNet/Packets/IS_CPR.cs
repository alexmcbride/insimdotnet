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
        public int Size { get; private set; }

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
        /// Gets the raw bytes of <see cref="PName"/> string.
        /// </summary>
        public byte[] RawPName => rawPName;
        private readonly byte[] rawPName;

        /// <summary>
        /// Gets the raw bytes of <see cref="Plate"/> string.
        /// </summary>
        public byte[] RawPlate => rawPlate;
        private readonly byte[] rawPlate;

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
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            PName = reader.ReadString(24, out rawPName);
            Plate = reader.ReadString(8, out rawPlate);
        }
    }
}
