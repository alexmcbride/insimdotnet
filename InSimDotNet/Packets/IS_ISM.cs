using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// InSim multiplayer packet.
    /// </summary>
    /// <remarks>
    /// Send when a host is started or joined. To request one to be sent send a 
    /// <see cref="IS_TINY"/> with a SubT of  TINY_ISM.
    /// </remarks>
    public class IS_ISM : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets if the client a guest or a host.
        /// </summary>
        public HostType Host { get; private set; }

        /// <summary>
        /// Gets the name of the host joined or started.
        /// </summary>
        public string HName { get; private set; }

        /// <summary>
        /// Creates a new InSim multiplayer packet.
        /// </summary>
        public IS_ISM() {
            Size = 40;
            Type = PacketType.ISP_ISM;
            HName = String.Empty;
        }

        /// <summary>
        /// Creates a new InSim multiplayer packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_ISM(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            Host = (HostType)reader.ReadByte();
            reader.Skip(3);
            HName = reader.ReadString(32);
        }
    }
}
