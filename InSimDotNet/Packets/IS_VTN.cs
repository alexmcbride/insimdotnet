using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Vote notify.
    /// </summary>
    /// <remarks>
    /// Sent when a vote is initiated. When the vote is completed LFS sends a 
    /// <see cref="IS_SMALL"/> with a SubT of SMALL_VTA. To cancel a vote send 
    /// a <see cref="IS_TINY"/> with a SubT of TINY_VTC.
    /// </remarks>
    public class IS_VTN : IPacket {
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
        /// Gets or sets the unique ID of the connection.
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets or sets the vote action.
        /// </summary>
        public VoteAction Action { get; set; }

        /// <summary>
        /// Creates a new vote notify.
        /// </summary>
        public IS_VTN() {
            Size = 8;
            Type = PacketType.ISP_VTN;
        }

        /// <summary>
        /// Creates a new vote notify.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_VTN(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            UCID = reader.ReadByte();
            Action = (VoteAction)reader.ReadByte();
        }
    }
}
