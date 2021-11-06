using System;

namespace InSimDotNet.Out {
    /// <summary>
    /// Provides data for the OutGauge PacketReceived event.
    /// </summary>
    public class OutGaugeEventArgs : EventArgs {
        /// <summary>
        /// Gets the OutGauge packet.
        /// </summary>
        public OutGaugePack Packet { get; private set; }

        /// <summary>
        /// Gets the time (to check order).
        /// </summary>
        public TimeSpan Time {
            get { return Packet.Time; }
        }

        /// <summary>
        /// Gets the car name.
        /// </summary>
        public string Car {
            get { return Packet.Car; }
        }

        /// <summary>
        /// Gets the OutGauge info flags.
        /// </summary>
        public OutGaugeFlags Flags {
            get { return Packet.Flags; }
        }

        /// <summary>
        /// Gets the current gear (reverse: 0, neutral: 1, first: 2 etc..).
        /// </summary>
        public byte Gear {
            get { return Packet.Gear; }
        }

        /// <summary>
        /// Gets the PLID of the player.
        /// </summary>
        public byte PLID {
            get { return Packet.PLID; }
        }

        /// <summary>
        /// Gets the speed in meters per second.
        /// </summary>
        public float Speed {
            get { return Packet.Speed; }
        }

        /// <summary>
        /// Gets the RPM.
        /// </summary>
        public float RPM {
            get { return Packet.RPM; }
        }

        /// <summary>
        /// Gets the turbo BAR.
        /// </summary>
        public float Turbo {
            get { return Packet.Turbo; }
        }

        /// <summary>
        /// Gets the engine temperature in degrees centigrade. 
        /// </summary>
        public float EngTemp {
            get { return Packet.EngTemp; }
        }

        /// <summary>
        /// Gets the fuel (0.0 to 1.0).
        /// </summary>
        public float Fuel {
            get { return Packet.Fuel; }
        }

        /// <summary>
        /// Gets the oil pressure in BAR.
        /// </summary>
        public float OilPressure {
            get { return Packet.OilPressure; }
        }

        /// <summary>
        /// Gets the oil temperature in degrees centigrade.
        /// </summary>
        public float OilTemp {
            get { return Packet.OilTemp; }
        }

        /// <summary>
        /// Gets which dashboard lights available for this car.
        /// </summary>
        public DashLightFlags DashLights {
            get { return Packet.DashLights; }
        }

        /// <summary>
        /// Gets the dashboard lights currently switched on.
        /// </summary>
        public DashLightFlags ShowLights {
            get { return Packet.ShowLights; }
        }

        /// <summary>
        /// Gets the throttle position (0.0 to 1.0).
        /// </summary>
        public float Throttle {
            get { return Packet.Throttle; }
        }

        /// <summary>
        /// Gets the brake position (0.0 to 1.0).
        /// </summary>
        public float Brake {
            get { return Packet.Brake; }
        }

        /// <summary>
        ///  Gets the clutch position (0.0 to 1.0).
        /// </summary>
        public float Clutch {
            get { return Packet.Clutch; }
        }

        /// <summary>
        /// Gets the first LCD display (usually fuel).
        /// </summary>
        public string Display1 {
            get { return Packet.Display1; }
        }

        /// <summary>
        /// Gets the second LCD display (usually settings).
        /// </summary>
        public string Display2 {
            get { return Packet.Display2; }
        }

        /// <summary>
        /// Gets the optional OutGauge ID (if specified in cfg.txt).
        /// </summary>
        public int ID {
            get { return Packet.ID; }
        }

        /// <summary>
        /// Creates a new OutGaugeEventArgs object.
        /// </summary>
        /// <param name="packet">The OutGauge packet.</param>
        public OutGaugeEventArgs(OutGaugePack packet) {
            Packet = packet;
        }
    }
}
