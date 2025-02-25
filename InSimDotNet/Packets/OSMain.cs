using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Class to represent an OSMain packet.
    /// </summary>
    public class OSMain {
        /// <summary>
        /// Gets the angular velocity.
        /// </summary>
        public Vector AngVel { get; private set; }

        /// <summary>
        /// Gets the current heading (anticlockwise from above (Z)).
        /// </summary>
        public float Heading { get; private set; }

        /// <summary>
        /// Gets the current pitch (anticlockwise from right (X)).
        /// </summary>
        public float Pitch { get; private set; }

        /// <summary>
        /// Gets the current roll (anticlockwise from front (Y)).
        /// </summary>
        public float Roll { get; private set; }

        /// <summary>
        /// Gets the current acceleration.
        /// </summary>
        public Vector Accel { get; private set; }

        /// <summary>
        /// Gets the current velocity.
        /// </summary>
        public Vector Vel { get; private set; }

        /// <summary>
        /// Gets the current position (1m = 65536).
        /// </summary>
        public Vec Pos { get; private set; }


        /// <summary>
        /// Creates a new instance of the <see cref="OSMain"/> class.
        /// </summary>
        /// <param name="reader">A packerReader containing the packet data.</param>
        public OSMain(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            AngVel = new Vector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Heading = reader.ReadSingle();
            Pitch = reader.ReadSingle();
            Roll = reader.ReadSingle();
            Accel = new Vector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Vel = new Vector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Pos = new Vec(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }
    }
}
