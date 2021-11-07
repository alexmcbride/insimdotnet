using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Class to represent the <see cref="IS_CON"/> sub-packet.
    /// </summary>
    public class CarContact {
        /// <summary>
        /// Gets the unique player ID of the car.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the car info flags.
        /// </summary>
        public ContactFlags Info { get; private set; }

        /// <summary>
        /// Gets the front-wheel steer in degrees (right positive).
        /// </summary>
        public short Steer { get; private set; }

        /// <summary>
        /// Gets the combined throttle and brake (high 4 bits : throttle / low 4 bits : brake (0 to 15)).
        /// </summary>
        public byte ThrBrk { get; private set; }

        /// <summary>
        /// Gets the combined clutch and handbrake (high 4 bits : clutch / low 4 bits : handbrake (0 to 15)).
        /// </summary>
        public byte CluHan { get; private set; }

        /// <summary>
        /// Gets the current gear (high 4 bits : gear (15=R) / low 4 bits : spare).
        /// </summary>
        public byte GearSp { get; private set; }

        /// <summary>
        /// Gets the speed in meters/per second.
        /// </summary>
        public byte Speed { get; private set; }

        /// <summary>
        /// Gets the direction of the car motion (if Speed > 0 : 0 = world y direction, 128 = 180 deg).
        /// </summary>
        public byte Direction { get; private set; }

        /// <summary>
        /// Gets the direction of forward axis (direction of forward axis : 0 = world y direction, 128 = 180 deg).
        /// </summary>
        public byte Heading { get; private set; }

        /// <summary>
        /// Gets the forward acceleration of the car (m/s^2 longitudinal acceleration (forward positive)).
        /// </summary>
        public short AccelF { get; private set; }

        /// <summary>
        /// Gets the lateral acceleration of the car (m/s^2 lateral acceleration (right positive)).
        /// </summary>
        public short AccelR { get; private set; }

        /// <summary>
        /// Gets the X coordinate of the car (1 meter = 16).
        /// </summary>
        public short X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate of the car (1 metre = 16).
        /// </summary>
        public short Y { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="CarContact"/> class.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> containing the packet data.</param>
        public CarContact(PacketReader reader) {
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }

            PLID = reader.ReadByte();
            Info = (ContactFlags)reader.ReadByte();
            reader.Skip(1);
            Steer = reader.ReadSByte();
            ThrBrk = reader.ReadByte();
            CluHan = reader.ReadByte();
            GearSp = reader.ReadByte();
            Speed = reader.ReadByte();
            Direction = reader.ReadByte();
            Heading = reader.ReadByte();
            AccelF = reader.ReadSByte();
            AccelR = reader.ReadSByte();
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
        }
    }
}
