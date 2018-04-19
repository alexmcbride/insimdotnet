
namespace InSimDotNet.Packets {
    /// <summary>
    /// Admin Command Report
    /// </summary>
    /// <remarks>
    /// Any user typed an admin command.
    /// </remarks>
    public class IS_ACR : IPacket {
        private const int DefaultSize = 8;

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
        /// Gets the unique ID of the connection.
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets if the user is an admin.
        /// </summary>
        public bool Admin { get; private set; }

        /// <summary>
        /// Gets whether the command was processed or not.
        /// </summary>
        public AdminResult Result { get; private set; }

        /// <summary>
        /// Gets the admin command text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Creates a new <see cref="IS_ACR"/> object.
        /// </summary>
        public IS_ACR() {
            Size = DefaultSize;
            Type = PacketType.ISP_ACR;
        }

        /// <summary>
        /// Creates a new <see cref="IS_ACR"/> object.
        /// </summary>
        /// <param name="buffer">The packet data.</param>
        public IS_ACR(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            UCID = reader.ReadByte();
            Admin = reader.ReadBoolean();
            Result = (AdminResult)reader.ReadByte();
            reader.Skip(1);

            // read out variable sized packet.
            int textLength = Size - DefaultSize;
            Text = reader.ReadString(textLength);
        }
    }
}
