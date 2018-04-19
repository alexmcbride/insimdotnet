using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Mode change packet.
    /// </summary>
    /// <remarks>
    /// Used to change the LFS screen mode.
    /// </remarks>
    public class IS_MOD : IPacket, ISendable {
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
        /// Gets or sets if in 16-bit screen mode.
        /// </summary>
        public int Bits16 { get; set; }

        /// <summary>
        /// Gets or sets the screen refresh rate (LFS will use the highest
        /// available refresh rate less than or equal to the specified one).
        /// </summary>
        public int RR { get; set; }

        /// <summary>
        /// Gets or sets the width of the screen resolution (if both Width and
        /// Height are 0 LFS will go into windowed mode.)
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the width of the screen resolution (if both Width and
        /// Height are 0 LFS will go into windowed mode.)
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Creates a new Mode change packet.
        /// </summary>
        public IS_MOD() {
            Size = 20;
            Type = PacketType.ISP_MOD;
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
            writer.Write(Bits16);
            writer.Write(RR);
            writer.Write(Width);
            writer.Write(Height);
            return writer.GetBuffer();
        }
    }
}
