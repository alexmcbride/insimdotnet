using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Select host packet.
    /// </summary>
    /// <remarks>
    /// Used to to select a InSim relay host.
    /// </remarks>
    public class IR_SEL : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the request ID. If set InSim Relay will reply with an <see cref="IS_VER"/> packet.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the name of the host to select (with or without colors).
        /// </summary>
        public string HName { get; set; }

        /// <summary>
        /// Gets or sets the host admin password (to gain admin access to host).
        /// </summary>
        public string Admin { get; set; }

        /// <summary>
        /// Gets or sets the spectator password, if required by the host.
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// Creates a new select host packet.
        /// </summary>
        public IR_SEL() {
            Size = 68;
            Type = PacketType.IRP_SEL;
            HName = String.Empty;
            Admin = String.Empty;
            Spec = String.Empty;
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            PacketWriter writer = new PacketWriter(Size);
            writer.Write((byte)Size);
            writer.Write((byte)Type);
            writer.Write((byte)ReqI);
            writer.Skip(1);
            writer.Write(HName, 32);
            writer.Write(Admin, 16);
            writer.Write(Spec, 16);
            return writer.GetBuffer();
        }
    }
}
