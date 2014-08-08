using System;

namespace InSimDotNet.Out {
    /// <summary>
    /// Class to represent an OutSim packet.
    /// </summary>
    public class OutSimPack {
        internal const int MaxSize = 68;
        internal const int MinSize = 64;

        /// <summary>
        /// Gets the time in milliseconds (to check order).
        /// </summary>
        public TimeSpan Time { get; private set; }

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
        /// Gets the optional OutSim ID (if specified in cfg.txt).
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="OutSimPack"/> class.
        /// </summary>
        /// <param name="buffer">A buffer containing the packet data.</param>
        public OutSimPack(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            PacketReader reader = new PacketReader(buffer);
            Time = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            AngVel = new Vector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Heading = reader.ReadSingle();
            Pitch = reader.ReadSingle();
            Roll = reader.ReadSingle();
            Accel = new Vector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Vel = new Vector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Pos = new Vec(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

            if (buffer.Length == MaxSize) {
                ID = reader.ReadInt32();
            }
        }
    }
}
