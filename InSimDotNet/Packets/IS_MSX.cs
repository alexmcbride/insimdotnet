using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Message extended packet.
    /// </summary>
    /// <remarks>
    /// Like <see cref="IS_MST"/> but longer (cannot be used for commands).
    /// </remarks>
    public class IS_MSX : IPacket, ISendable {
        private byte[] message;

        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the message to send (up to 96 characters).
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Creates a new message extended packet.
        /// </summary>
        public IS_MSX() {
            Size = 100;
            Type = PacketType.ISP_MSX;
            Msg = String.Empty;
        }

        internal IS_MSX(byte[] message) : this() {
            this.message = message;
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Skip(1);
            if (message == null) {
                writer.Write(Msg, 96);
            }
            else {
                writer.Write(message);
            }
            return writer.GetBuffer();
        }
    }
}
