using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// InSim initialization packet.
    /// </summary>
    /// <remarks>
    /// Sent to initialize the InSim system.
    /// </remarks>
    public class IS_ISI : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the <see cref="PacketType"/>.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the packet request ID. If set to non-zero LFS will respond with a <see cref="IS_VER"/> packet.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the port for UDP replies from LFS (0 to 65535).
        /// </summary>
        public int UDPPort { get; set; }

        /// <summary>
        /// Gets or sets the InSim initialization options.
        /// </summary>
        public InSimFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the InSim version.
        /// </summary>
        public byte InSimVer { get; set; }

        /// <summary>
        /// Gets or sets the special host message prefix character.
        /// </summary>
        public char Prefix { get; set; }

        /// <summary>
        /// Gets or sets the time in milliseconds between NLP or MCI packets (0 = none).
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or sets the admin password (if set in LFS).
        /// </summary>
        public string Admin { get; set; }

        /// <summary>
        /// Gets or sets a short name for the program.
        /// </summary>
        public string IName { get; set; }

        /// <summary>
        /// Creates a new InSim initialization packet.
        /// </summary>
        public IS_ISI() {
            Size = 44;
            Type = PacketType.ISP_ISI;
            Admin = String.Empty;
            IName = String.Empty;
        }

        /// <summary>
        /// Returns the packet data as an array of bytes.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Skip(1);
            writer.Write((ushort)UDPPort);
            writer.Write((ushort)Flags);
            writer.Write(InSimVer);
            writer.Write(Prefix);
            writer.Write((ushort)Interval);
            writer.Write(Admin, 16);
            writer.Write(IName, 16);
            return writer.GetBuffer();
        }
    }
}
