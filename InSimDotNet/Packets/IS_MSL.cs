using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Message local packet.
    /// </summary>
    /// <remarks>
    /// Used to send a message that appears on local computer only.
    /// </remarks>
    public class IS_MSL : IPacket, ISendable {
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
        /// Gets or sets the sound effect to play with this message.
        /// </summary>
        public MessageSound Sound { get; set; }

        /// <summary>
        /// Gets or sets the message to send.
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Creates a new message local packet.
        /// </summary>
        public IS_MSL() {
            Size = 132;
            Type = PacketType.ISP_MSL;
            Msg = String.Empty;
        }

        /// <summary>
        /// Returns the data for the packet.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)Sound);
            writer.Write(Msg, 128);
            return writer.GetBuffer();
        }
    }
}
