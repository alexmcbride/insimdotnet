using System.Net;

namespace InSimDotNet.Packets {
    /// <summary>
    /// New Conn Info
    /// </summary>
    /// <remarks>
    /// Sent on host only if an admin password has been set.
    /// </remarks>
    public class IS_NCI : IPacket {
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
        /// Gets the new connection's unique id (0 = host)
        /// </summary>
        public byte UCID { get; private set; }		

        /// <summary>
        /// Gets the language.
        /// </summary>
        public LfsLanguage Language { get; private set; }

        /// <summary>
        /// Gets the LFS user ID.
        /// </summary>
        public long UserID { get; private set; }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        public IPAddress IPAddress { get; private set; }

        /// <summary>
        /// Creates a new IS_NCI class.
        /// </summary>
        public IS_NCI() {
            Size = 16;
            Type = PacketType.ISP_NCI;
        }

        /// <summary>
        /// Creates a new IS_NCI class.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet data.</param>
        public IS_NCI(byte[] buffer):this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            Language = (LfsLanguage)reader.ReadByte();
            reader.Skip(3);
            UserID = reader.ReadUInt32();
            IPAddress = new IPAddress(reader.ReadUInt32());
        }
    }
}
