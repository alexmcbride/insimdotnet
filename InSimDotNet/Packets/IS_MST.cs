using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Message type packet.
    /// </summary>
    /// <remarks>
    /// Used to type message or command into LFS.
    /// </remarks>
    public class IS_MST : IPacket, ISendable {
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
        /// Gets or sets the packet request ID. 
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the message to send (up to 64 characters).
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Creates a new message type packet.
        /// </summary>
        public IS_MST() {
            Size = 68;
            Type = PacketType.ISP_MST;
            Msg = String.Empty;
        }

        internal IS_MST(byte[] message) : this() {
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
                writer.Write(Msg, 64);
            }
            else {
                writer.Write(message);
            }
            return writer.GetBuffer();
        }
    }
}
