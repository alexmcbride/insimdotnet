using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Set car camera packet.
    /// </summary>
    /// <remarks>
    /// A simplified version of <see cref="IS_CPP"/> (not available in SHIFT+U mode).
    /// </remarks>
    public class IS_SCC : IPacket, ISendable {
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
        /// Gets or sets the unique ID of the viewed player.
        /// </summary>
        public byte ViewPLID { get; set; }

        /// <summary>
        /// Gets or sets the in game camera.
        /// </summary>
        public ViewIndentifier InGameCam { get; set; }

        /// <summary>
        /// Creates a new set car camera packet.
        /// </summary>
        public IS_SCC() {
            Size = 8;
            Type = PacketType.ISP_SCC;
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
            writer.Write(ViewPLID);
            writer.Write((byte)InGameCam);
            return writer.GetBuffer();
        }
    }
}
