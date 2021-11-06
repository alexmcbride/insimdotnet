using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Screenshot packet.
    /// </summary>
    /// <remarks>
    /// Used to take a screenshot in LFS. It will be saved as either bmp, jpg, or png, depending 
    /// on what is set in your LFS > Misc settings.
    /// </remarks>
    public class IS_SSH : IPacket, ISendable {
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
        /// Gets the screenshot error.
        /// </summary>
        public ScreenshotError Error { get; private set; }

        /// <summary>
        /// Gets or sets the screenshot name. If you leave this blank LFS is invent a new name for the screenshot.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a new screenshot packet.
        /// </summary>
        public IS_SSH() {
            Size = 40;
            Type = PacketType.ISP_SSH;
            Name = String.Empty;
        }

        /// <summary>
        /// Creates a new screenshot packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_SSH(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            Error = (ScreenshotError)reader.ReadByte();
            reader.Skip(4);
            Name = reader.ReadString(32);
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
            writer.Write((byte)Error);
            writer.Skip(4);
            writer.Write(Name, 32);
            return writer.GetBuffer();
        }
    }
}
