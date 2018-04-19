
using System;
namespace InSimDotNet.Packets {
    /// <summary>
    /// Car contact packet.
    /// </summary>
    public class IS_CON : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID of the packet.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the closing speed of the two cars (high 4 bits : reserved / low 12 bits : closing speed (10 = 1 m/s)).
        /// </summary>
        public int SpClose { get; private set; }

        /// <summary>
        /// Gets the timestamp (looping time stamp (hundredths - time since reset - like TINY_GTH)).
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets the first car in the collision.
        /// </summary>
        public CarContact A { get; private set; }

        /// <summary>
        /// Gets the second car in the collision.
        /// </summary>
        public CarContact B { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="IS_CON"/> packet.
        /// </summary>
        public IS_CON() {
            Size = 40;
            Type = PacketType.ISP_CON;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IS_CON"/> packet.
        /// </summary>
        /// <param name="buffer">A buffer containing the packet data.</param>
        public IS_CON(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            reader.Skip(1);
            SpClose = reader.ReadUInt16();
            Time = TimeSpan.FromMilliseconds(reader.ReadUInt16() * 10);
            A = new CarContact(reader);
            B = new CarContact(reader);
        }
    }
}
