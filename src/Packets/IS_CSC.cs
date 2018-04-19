using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Car State Changed - reports a change in a car's state (currently start or stop).
    /// </summary>
    public class IS_CSC : IPacket {
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
        /// Gets the player's unique ID.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the current action for the car.
        /// </summary>
        public CSCAction CSCAction { get; private set; }

        /// <summary>
        /// Gets the time since race start for the car as in SMALL_RTP.
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets the car object.
        /// </summary>
        public CarContOBJ C { get; private set; }

        /// <summary>
        /// Creates a new IS_CSC object.
        /// </summary>
        /// <param name="buffer"></param>
        public IS_CSC(byte[] buffer) {
            var reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            reader.Skip(1);
            CSCAction = (CSCAction)reader.ReadByte();
            reader.Skip(2);
            Time = TimeSpan.FromMilliseconds(reader.ReadUInt32() * 10);
            C = new CarContOBJ(reader);
        }
    }
}
