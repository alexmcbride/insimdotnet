
using System;
namespace InSimDotNet.Packets {
    /// <summary>
    /// Class to represent the <see cref="IS_OBH"/> C field.
    /// </summary>
    public class CarContOBJ {
        /// <summary>
        /// Gets the car's motion if Speed > 0 : 0 = world y direction, 128 = 180 deg.
        /// </summary>
        public byte Direction { get; private set; }

        /// <summary>
        /// Gets the direction of forward axis : 0 = world y direction, 128 = 180 deg.
        /// </summary>
        public byte Heading { get; private set; }

        /// <summary>
        /// Gets the speed in meters per second.
        /// </summary>
        public byte Speed { get; private set; }

        /// <summary>
        /// Gets the altitude of the car.
        /// </summary>
        public byte Zbyte { get; private set; }

        /// <summary>
        /// Gets the X position (1 meter = 16).
        /// </summary>
        public short X { get; private set; }

        /// <summary>
        /// Gets the Y position  (1 metre = 16).
        /// </summary>
        public short Y { get; private set; }

        /// <summary>
        /// Creates a new <see cref="CarContOBJ"/> object.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> contaning the data.</param>
        public CarContOBJ(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            Direction = reader.ReadByte();
            Heading = reader.ReadByte();
            Speed = reader.ReadByte();
            reader.Skip(1);
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
        }
    };
}
