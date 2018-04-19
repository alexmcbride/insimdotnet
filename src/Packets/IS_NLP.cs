using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Node and lap packet.
    /// </summary>
    /// <remarks>
    /// Used to provide node and position updates. To enable these packets see 
    /// the Flags and Interval properties when initializing InSim. 
    /// </remarks>
    public class IS_NLP : IPacket {
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
        /// Gets the number of <see cref="NodeLap"/> packets in the <see cref="IS_NLP"/>.
        /// </summary>
        public byte NumP { get; private set; }

        /// <summary>
        /// Gets a collection of <see cref="NodeLap"/> packets.
        /// </summary>
        public ReadOnlyCollection<NodeLap> Info { get; private set; }

        /// <summary>
        /// Creates a new node and lap packet.
        /// </summary>
        public IS_NLP() {
            Size = 4;
            Type = PacketType.ISP_NLP;
        }

        /// <summary>
        /// Creates a new node and lap packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_NLP(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumP = reader.ReadByte();

            List<NodeLap> info = new List<NodeLap>(NumP);
            for (int i = 0; i < NumP; i++) {
                info.Add(new NodeLap(reader));
            }
            Info = new ReadOnlyCollection<NodeLap>(info);
        }
    }
}
