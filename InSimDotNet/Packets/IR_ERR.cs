using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// InSim Relay error packet.
    /// </summary>
    /// <remarks>
    ///  Sent when an error occurs.
    /// </remarks>
    public class IR_ERR : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID, as given in <see cref="IR_SEL"/>.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the error information.
        /// </summary>
        public RelayError ErrNo { get; private set; }

        /// <summary>
        /// Creates a new InSim Relay error packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IR_ERR(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            ErrNo = (RelayError)reader.ReadByte();
        }
    };
}
