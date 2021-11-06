using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Connection Leave packet.
    /// </summary>
    /// <remarks>
    /// Sent when a connection leaves the host.
    /// </remarks>
    public class IS_CNL : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the packet request ID. 
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the unique id of the connection which left.
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets the reason the connection left.
        /// </summary>
        public LeaveReason Reason { get; private set; }

        /// <summary>
        /// Gets the total number of connections (including host).
        /// </summary>
        public byte Total { get; private set; }

        /// <summary>
        /// Creates a new connection leave packet.
        /// </summary>
        public IS_CNL() {
            Size = 8;
            Type = PacketType.ISP_CNL;
        }

        /// <summary>
        /// Creates a new connection leave packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_CNL(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            Reason = (LeaveReason)reader.ReadByte();
            Total = reader.ReadByte();
        }
    }
}
