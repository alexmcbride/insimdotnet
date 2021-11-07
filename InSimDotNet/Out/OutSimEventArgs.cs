using System;

namespace InSimDotNet.Out {
    /// <summary>
    /// Provides data for the OutSim PacketReceived event.
    /// </summary>
    public class OutSimEventArgs : EventArgs {
        /// <summary>
        /// Gets the OutSim packet.
        /// </summary>
        public OutSimPack Packet { get; private set; }

        /// <summary>
        /// Gets the time in milliseconds (to check order).
        /// </summary>
        public TimeSpan Time {
            get { return Packet.Time; }
        }

        /// <summary>
        /// Gets the angular velocity.
        /// </summary>
        public Vector AngVel {
            get { return Packet.AngVel; }
        }

        /// <summary>
        /// Gets the current heading (anticlockwise from above (Z)).
        /// </summary>
        public float Heading {
            get { return Packet.Heading; }
        }

        /// <summary>
        /// Gets the current pitch (anticlockwise from right (X)).
        /// </summary>
        public float Pitch {
            get { return Packet.Pitch; }
        }

        /// <summary>
        /// Gets the current roll (anticlockwise from front (Y)).
        /// </summary>
        public float Roll {
            get { return Packet.Roll; }
        }

        /// <summary>
        /// Gets the current acceleration.
        /// </summary>
        public Vector Accel {
            get { return Packet.Accel; }
        }

        /// <summary>
        /// Gets the current velocity.
        /// </summary>
        public Vector Vel {
            get { return Packet.Vel; }
        }

        /// <summary>
        /// Gets the current position (1m = 65536).
        /// </summary>
        public Vec Pos {
            get { return Packet.Pos; }
        }

        /// <summary>
        /// Gets the optional OutSim ID (if specified in cfg.txt).
        /// </summary>
        public int ID {
            get { return Packet.ID; }
        }

        /// <summary>
        /// Creates a new OutSimEventArgs object.
        /// </summary>
        /// <param name="packet">The OutSim packet.</param>
        public OutSimEventArgs(OutSimPack packet) {
            Packet = packet;
        }
    }
}
