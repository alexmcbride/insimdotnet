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
        /// Gets the raw bytes of <see cref="HName"/> string.
        /// </summary>
        public byte[] RawHName => rawHName;
        private readonly byte[] rawHName;

        /// <summary>
        /// Gets the raw bytes of <see cref="Track"/> string.
        /// </summary>
        public byte[] RawTrack => rawTrack;
        private readonly byte[] rawTrack;

        /// <summary>
        /// Creates a new <see cref="HInfo"/> sub-packet.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> contaning the packet object.</param>
        public HInfo(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            HName = reader.ReadString(32, out rawHName);
            Track = reader.ReadString(6, out rawTrack);
            Flags = (HostFlags)reader.ReadByte();
            NumConns = reader.ReadByte();
        }
    }
}
