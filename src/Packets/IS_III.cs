using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// InSim info packet.
    /// </summary>
    /// <remarks>
    /// Sent when a player sends a /i message to a host.
    /// </remarks>
    public class IS_III : IPacket {
        private const int DefaultSize = 8;

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
        /// Gets the unique ID of the connection who sent the message.
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets the unique ID of the player who sent the message (if 0 use UCID).
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Msg { get; private set; }

        /// <summary>
        /// Creates a new InSim info packet.
        /// </summary>
        public IS_III() {
            Size = DefaultSize;
            Type = PacketType.ISP_III;
            Msg = String.Empty;
        }

        /// <summary>
        /// Creates a new InSim info packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_III(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            UCID = reader.ReadByte();
            PLID = reader.ReadByte();
            reader.Skip(2);

            // read variable sized packet.
            int msgLength = Size - DefaultSize;
            Msg = reader.ReadString(msgLength);
        }
    }
}
