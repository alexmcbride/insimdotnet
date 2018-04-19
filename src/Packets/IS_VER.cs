using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Version packet.
    /// </summary>
    /// <remarks>
    /// Sent in response to a <see cref="IS_TINY"/> with a SubT of TINY_VER.
    /// </remarks>
    public class IS_VER : IPacket {
        /// <summary>
        /// Gets the packet size.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the packet type.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the LFS version, e.g. 0.3G.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Gets the product : DEMO or S1
        /// </summary>
        public string Product { get; private set; }

        /// <summary>
        /// Gets the InSim Version : increased when InSim packets change.
        /// </summary>
        public int InSimVer { get; private set; }

        /// <summary>
        /// Creates a new version packet.
        /// </summary>
        public IS_VER() {
            Size = 20;
            Type = PacketType.ISP_VER;
            Version = String.Empty;
            Product = String.Empty;
        }

        /// <summary>
        /// Creates a new version packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_VER(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            Version = reader.ReadString(8);
            Product = reader.ReadString(6);
            InSimVer = reader.ReadByte();
            reader.Skip(1);
        }
    }
}
