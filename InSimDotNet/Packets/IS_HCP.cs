
namespace InSimDotNet.Packets {
    /// <summary>
    /// HandiCaPs
    /// </summary>
    public class IS_HCP : IPacket, ISendable {
        private const int NumCars = 32;

        /// <summary>
        /// Gets the packet size.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the packet type.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets the handicap info for each car in order (XF GTI first, XR GT second etc..).
        /// </summary>
        public CarHCP[] Info { get; private set; }

        /// <summary>
        /// Creates a new IS_HCP packet.
        /// </summary>
        public IS_HCP() {
            Size = 68;
            Type = PacketType.ISP_HCP;
            Info = new CarHCP[NumCars];
        }

        /// <summary>
        /// Gets the packet buffer.
        /// </summary>
        /// <returns>An array containing the packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Skip(1);

            foreach (CarHCP info in Info) {
                info.GetBuffer(writer);
            }

            return writer.GetBuffer();
        }
    }
}
