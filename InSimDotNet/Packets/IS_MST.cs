using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Message type packet.
    /// </summary>
    /// <remarks>
    /// Used to type message or command into LFS.
    /// </remarks>
    public class IS_MST : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

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
        /// Gets or sets the raw bytes of <see cref="Msg"/> string.
        /// </summary>
        public byte[] RawMsg { get => rawMsg; set => rawMsg = value; }
        private byte[] rawMsg;

        /// <summary>
        /// Creates a new message type packet.
        /// </summary>
        public IS_MST() {
            Size = 68;
            Type = PacketType.ISP_MST;
            Msg = String.Empty;
        }

        public IS_MST(byte[] rawMsg) : this() {
            RawMsg = rawMsg;
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Skip(1);
            writer.Write(rawMsg, Msg, 64);
            return writer.GetBuffer();
        }
    }
}
