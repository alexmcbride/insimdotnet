using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Host list request packet.
    /// </summary>
    /// <remarks>
    ///  Used to request a series <see cref="IR_HOS"/> host list packet to be sent.
    /// </remarks>
    public class IR_HLR : IPacket, ISendable {
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
        /// Creates a new host list request packet.
        /// </summary>
        public IR_HLR() {
            Size = 4;
            Type = PacketType.IRP_HLR;
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write((byte)Size);
            writer.Write((byte)Type);
            writer.Write((byte)ReqI);
            return writer.GetBuffer();
        }
    };
}
