using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Class for the <see cref="IS_MCI"/> Info collection.
    /// </summary>
    public class CompCar {
        /// <summary>
        /// Gets the current path node
        /// </summary>
        public int Node { get; private set; }

        /// <summary>
        /// Gets the current lap
        /// </summary>
        public int Lap { get; private set; }

        /// <summary>
        /// Gets the unique ID of the player.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the current race position : 0 = unknown, 1 = leader, etc...
        /// </summary>
        public byte Position { get; private set; }

        /// <summary>
        /// Gets the car flags and other info
        /// </summary>
        public CompCarFlags Info { get; private set; }

        /// <summary>
        /// Gets the cars current X coordinate (65536 = 1 metre)
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the cars current Y coordinate (65536 = 1 metre)
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Gets the cars current Z coordinate (65536 = 1 metre)
        /// </summary>
        public int Z { get; private set; }

        /// <summary>
        /// Gets the cars current speed (32768 = 100 m/s)
        /// </summary>
        public int Speed { get; private set; }

        /// <summary>
        /// Gets the direction of car's motion : 0 = world Y direction, 32768 = 180 deg
        /// </summary>
        public int Direction { get; private set; }

        /// <summary>
        /// Gets the cars current direction of forward axis : 0 = world Y direction, 32768 = 180 deg
        /// </summary>
        public int Heading { get; private set; }

        /// <summary>
        /// Gets the cars rate of change of heading : (16384 = 360 deg/s)
        /// </summary>
        public short AngVel { get; private set; }

        /// <summary>
        /// Creates a new CompCar sub-packet.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> containing the packet data.</param>
        public CompCar(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            Node = reader.ReadUInt16();
            Lap = reader.ReadUInt16();
            PLID = reader.ReadByte();
            Position = reader.ReadByte();
            Info = (CompCarFlags)reader.ReadByte();
            reader.Skip(1);
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
            Z = reader.ReadInt32();
            Speed = reader.ReadUInt16();
            Direction = reader.ReadUInt16();
            Heading = reader.ReadUInt16();
            AngVel = reader.ReadInt16();
        }
    }
}
