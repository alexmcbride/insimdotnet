using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Host response packet.
    /// </summary>
    /// <remarks>
    /// Sent in reply to <see cref="IR_HLR"/> host list request.
    /// </remarks>
    public class IR_HOS : IPacket {
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
        /// Gets the number of hosts described in this packet.
        /// </summary>
        public byte NumHosts { get; private set; }

        /// <summary>
        /// Gets a collection of up to six <see cref="HInfo"/> packets, one for each host in the relay.
        /// If more than six hosts are online then multiple <see cref="IR_HOS"/> packets are sent.
        /// </summary>
        public ReadOnlyCollection<HInfo> Info { get; private set; }

        /// <summary>
        /// Creates a new host response packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IR_HOS(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumHosts = reader.ReadByte();

            List<HInfo> info = new List<HInfo>(NumHosts);
            for (int i = 0; i < NumHosts; i++) {
                info.Add(new HInfo(reader));
            }
            Info = new ReadOnlyCollection<HInfo>(info);
        }
    }
}
