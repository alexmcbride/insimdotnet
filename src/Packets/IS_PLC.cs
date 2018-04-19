using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// PLayer Cars.
    /// </summary>
    /// <remarks>
    /// You can send a packet to limit the cars that can be used by a given connection. 
    /// The resulting set of selectable cars is a subset of the cars set to be available 
    /// on the host (by the /cars command).
    /// </remarks>
    public class IS_PLC : IPacket, ISendable {
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
        /// Gets or sets the unique ID of the connection on which to limit the cars.
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets or sets the cars to limit.
        /// </summary>
        public CarFlags Cars { get; set; }

        /// <summary>
        /// Creates a new <see cref="IS_PLC"/> packet.
        /// </summary>
        public IS_PLC() {
            Size = 12;
            Type = PacketType.ISP_PLC;
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>An array contaning the packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Skip(1);
            writer.Write(UCID);
            writer.Skip(3);
            writer.Write((uint)Cars);
            return writer.GetBuffer();
        }
    }
}
