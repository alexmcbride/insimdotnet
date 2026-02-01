using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Message to connection packet.
    /// </summary>
    /// <remarks>
    /// Used to send a message to a specific connection or player (can only be used on hosts).
    /// </remarks>
    public class IS_MTC : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the sound effect.
        /// </summary>
        public MessageSound Sound { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the connection to send the message to (0 = host / 255 = all).
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the player to send the message to (if 0 use UCID).
        /// </summary>
        public byte PLID { get; set; }

        /// <summary>
        /// Gets or sets the message to send.
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Gets or sets the raw bytes of <see cref="Msg"/> string.
        /// </summary>
        public byte[] RawMsg { get => rawMsg; set => rawMsg = value; }
        private byte[] rawMsg;

        /// <summary>
        /// Creates a new message to connection packet.
        /// </summary>
        public IS_MTC() {
            Size = 8;
            Type = PacketType.ISP_MTC;
            Msg = String.Empty;
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            const int DefaultSize = 8;
            const int TextSize = 128;

            byte[] buffer = new byte[TextSize];
            int length;
            if (RawMsg == null) {
                // Encode string first so we can figure out the packet size.
                length = LfsEncoding.Current.GetBytes(Msg, buffer, 0, buffer.Length);

                // Get the packet size (MTC needs trailing zero).
                Size = (byte)(DefaultSize + Math.Min(length + (4 - (length % 4)), TextSize));
            }
            else
            {
                int rawLength = RawMsg.Length;
                // If rawLength is above TextSize, truncate it.
                if (rawLength > TextSize) {
                    rawLength = TextSize;
                }

                // No need to manually null terminate it since the buffer is filled with 0s by default.
                Buffer.BlockCopy(RawMsg, 0, buffer, 0, rawLength);

                // If rawLength is not a multiple of 4, complete the buffer length to a multiple of 4.
                length = (rawLength % 4 != 0) ? rawLength + (4 - (rawLength % 4)) : rawLength;

                Size = (byte)(DefaultSize + length);
            }

            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)Sound);
            writer.Write(UCID);
            writer.Write(PLID);
            writer.Skip(2);
            writer.Write(buffer, length);
            return writer.GetBuffer();
        }
    }
}
