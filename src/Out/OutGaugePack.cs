using InSimDotNet.Packets;
using System;

namespace InSimDotNet.Out {
    /// <summary>
    /// Class to represent an OutGauge packet.
    /// </summary>
    public class OutGaugePack {
        internal const int MaxSize = 96;
        internal const int MinSize = 92;

        /// <summary>
        /// Gets the time (to check order).
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets the car name.
        /// </summary>
        public string Car { get; private set; }

        /// <summary>
        /// Gets the OutGauge info flags.
        /// </summary>
        public OutGaugeFlags Flags { get; private set; }

        /// <summary>
        /// Gets the current gear (reverse: 0, neutral: 1, first: 2 etc..).
        /// </summary>
        public byte Gear { get; private set; }

        /// <summary>
        /// Gets the PLID of the player.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the speed in meters per second.
        /// </summary>
        public float Speed { get; private set; }

        /// <summary>
        /// Gets the RPM.
        /// </summary>
        public float RPM { get; private set; }

        /// <summary>
        /// Gets the turbo BAR.
        /// </summary>
        public float Turbo { get; private set; }

        /// <summary>
        /// Gets the engine temperature in degrees centigrade. 
        /// </summary>
        public float EngTemp { get; private set; }

        /// <summary>
        /// Gets the fuel (0.0 to 1.0).
        /// </summary>
        public float Fuel { get; private set; }

        /// <summary>
        /// Gets the oil pressure in BAR.
        /// </summary>
        public float OilPressure { get; private set; }

        /// <summary>
        /// Gets the oil temperature in degrees centigrade.
        /// </summary>
        public float OilTemp { get; private set; }

        /// <summary>
        /// Gets which dashboard lights available for this car.
        /// </summary>
        public DashLightFlags DashLights { get; private set; }

        /// <summary>
        /// Gets the dashboard lights currently switched on.
        /// </summary>
        public DashLightFlags ShowLights { get; private set; }

        /// <summary>
        /// Gets the throttle position (0.0 to 1.0).
        /// </summary>
        public float Throttle { get; private set; }

        /// <summary>
        /// Gets the brake position (0.0 to 1.0).
        /// </summary>
        public float Brake { get; private set; }

        /// <summary>
        ///  Gets the clutch position (0.0 to 1.0).
        /// </summary>
        public float Clutch { get; private set; }

        /// <summary>
        /// Gets the first LCD display (usually fuel).
        /// </summary>
        public string Display1 { get; private set; }

        /// <summary>
        /// Gets the second LCD display (usually settings).
        /// </summary>
        public string Display2 { get; private set; }

        /// <summary>
        /// Gets the optional OutGauge ID (if specified in cfg.txt).
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="OutGaugePack"/> class.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public OutGaugePack(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            PacketReader reader = new PacketReader(buffer);
            Time = TimeSpan.FromMilliseconds(reader.ReadUInt32());
            Car = reader.ReadString(4);
            Flags = (OutGaugeFlags)reader.ReadUInt16();
            Gear = reader.ReadByte();
            PLID = reader.ReadByte();
            Speed = reader.ReadSingle();
            RPM = reader.ReadSingle();
            Turbo = reader.ReadSingle();
            EngTemp = reader.ReadSingle();
            Fuel = reader.ReadSingle();
            OilPressure = reader.ReadSingle();
            OilTemp = reader.ReadSingle();
            DashLights = (DashLightFlags)reader.ReadUInt32();
            ShowLights = (DashLightFlags)reader.ReadUInt32();
            Throttle = reader.ReadSingle();
            Brake = reader.ReadSingle();
            Clutch = reader.ReadSingle();
            Display1 = reader.ReadString(16);
            Display2 = reader.ReadString(16);

            // ID is optional.
            if (buffer.Length == MaxSize) {
                ID = reader.ReadInt32();
            }
        }
    }
}
