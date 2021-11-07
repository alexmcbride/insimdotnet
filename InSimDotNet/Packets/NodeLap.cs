using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Car info.
    /// </summary>
    public class NodeLap {
        /// <summary>
        /// Gets the current path node.
        /// </summary>
        public int Node { get; private set; }

        /// <summary>
        /// Gets the current lap.
        /// </summary>
        public int Lap { get; private set; }

        /// <summary>
        /// Gets the unique ID of the player.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the current race position.
        /// </summary>
        public byte Position { get; private set; }

        /// <summary>
        /// Creates a new NodeLap sub-packet.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> contaning the packet data.</param>
        public NodeLap(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            Node = reader.ReadUInt16();
            Lap = reader.ReadUInt16();
            PLID = reader.ReadByte();
            Position = reader.ReadByte();
        }
    }
}
