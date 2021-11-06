using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Host info sub packet for <see cref="IR_HOS"/> Info collection.
    /// </summary>
    public class HInfo {
        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        public string HName { get; private set; }

        /// <summary>
        /// Gets the short track name.
        /// </summary>
        public string Track { get; private set; }

        /// <summary>
        /// Gets the <see cref="HostFlags"/> for the host.
        /// </summary>
        public HostFlags Flags { get; private set; }

        /// <summary>
        /// Gets the number of connections on the host.
        /// </summary>
        public byte NumConns { get; private set; }

        /// <summary>
        /// Creates a new <see cref="HInfo"/> sub-packet.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> contaning the packet object.</param>
        public HInfo(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            HName = reader.ReadString(32);
            Track = reader.ReadString(6);
            Flags = (HostFlags)reader.ReadByte();
            NumConns = reader.ReadByte();
        }
    }
}
