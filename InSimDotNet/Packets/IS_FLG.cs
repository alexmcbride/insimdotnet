using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Flag packet.
    /// </summary>
    /// <remarks>
    /// Sent when yellow or blue flag changes.
    /// </remarks>
    public class IS_FLG : IPacket {
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
        /// Gets the unique ID of the player the flag is being shown to.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets if the flag is on or off (true = on).
        /// </summary>
        public bool OffOn { get; private set; }

        /// <summary>
        /// Gets the flag being caused.
        /// </summary>
        public FlagType Flag { get; private set; }

        /// <summary>
        /// Gets the unique ID of the obstructed player if blue flag.
        /// </summary>
        public byte CarBehind { get; private set; }

        /// <summary>
        /// Creates a new flag packet.
        /// </summary>
        public IS_FLG() {
            Size = 8;
            Type = PacketType.ISP_FLG;
        }

        /// <summary>
        /// Creates a new flag packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_FLG(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            OffOn = reader.ReadBoolean();
            Flag = (FlagType)reader.ReadByte();
            CarBehind = reader.ReadByte();
        }
    }
}
