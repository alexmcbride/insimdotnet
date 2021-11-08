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
            // Encode string first so we can figure out the packet size.
            byte[] buffer = new byte[128];
            int length = LfsEncoding.Current.GetBytes(Msg, buffer, 0, 128);

            // Get the packet size (MTC needs trailing zero).
            Size = (byte)(8 + Math.Min(length + (4 - (length % 4)), 128));

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
