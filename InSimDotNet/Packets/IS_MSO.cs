using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Message out packet.
    /// </summary>
    /// <remarks>
    /// Sent by LFS containing system and user messages.
    /// </remarks>
    public class IS_MSO : IPacket {
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
        /// Gets the unique ID of the connection who sent the message (0 if host).
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets the unique ID of the player who sent the message (if 0 use UCID).
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the source that the message was sent from.
        /// </summary>
        public UserType UserType { get; private set; }

        /// <summary>
        /// Gets the first character of the message text after the player name.
        /// </summary>
        public byte TextStart { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Msg { get; private set; }

        /// <summary>
        /// Creates a new message out packet.
        /// </summary>
        public IS_MSO() {
            Size = DefaultSize;
            Type = PacketType.ISP_MSO;
            Msg = String.Empty;
        }

        /// <summary>
        /// Creates a new message out packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_MSO(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            UCID = reader.ReadByte();
            PLID = reader.ReadByte();
            UserType = (UserType)reader.ReadByte();
            int textStart = reader.ReadByte();

            // the length of the message will be the size of the packet minus all the above crap.
            int msgLength = Size - DefaultSize;

            // Need to correct textstart after we've converted string to unicode.
            if (textStart > 0) {
                string pname = reader.ReadString(textStart);
                TextStart = (byte)pname.Length;
                Msg = pname + reader.ReadString(msgLength - textStart);
            }
            else {
                Msg = reader.ReadString(msgLength);
            }
        }
    }
}
