using System;

namespace InSimDotNet.Out {
    /// <summary>
    /// OutGauge packet ShowLights and DashLight flags.
    /// </summary>
    [Flags]
    public enum DashLightFlags : long {
        /// <summary>
        /// Shift light on.
        /// </summary>
        DL_SHIFT = 1,

        /// <summary>
        /// Headlights on full beam.
        /// </summary>
        DL_FULLBEAM = 2,

        /// <summary>
        /// Handbrake on.
        /// </summary>
        DL_HANDBRAKE = 4,

        /// <summary>
        /// Pit lane speed limiter on.
        /// </summary>
        DL_PITSPEED = 8,

        /// <summary>
        /// Traction-control on.
        /// </summary>
        DL_TC = 16,

        /// <summary>
        /// Left turn signal on.
        /// </summary>
        DL_SIGNAL_L = 32,

        /// <summary>
        /// Right turn signal on.
        /// </summary>
        DL_SIGNAL_R = 64,

        /// <summary>
        /// Shared turn signal on (hazard lights).
        /// </summary>
        DL_SIGNAL_ANY = 128,

        /// <summary>
        /// Oil pressure warning on.
        /// </summary>
        DL_OILWARN = 256,

        /// <summary>
        /// Battery warning light on.
        /// </summary>
        DL_BATTERY = 512,

        /// <summary>
        /// Anti-lock brakes active.
        /// </summary>
        DL_ABS = 1024,

        /// <summary>
        /// engine damage
        /// </summary>
        DL_ENGINE = 2048,

        /// <summary>
        /// Fog rear
        /// </summary>
        DL_FOG_REAR = 4096,

        /// <summary>
        /// Fog front
        /// </summary>
        DL_FOG_FRONT = 8192,

        /// <summary>
        /// dipped headlight symbol
        /// </summary>
        DL_DIPPED = 16384,

        /// <summary>
        /// low fuel warning light
        /// </summary>
        DL_FUELWARN = 32768,

        /// <summary>
        /// sidelights symbol
        /// </summary>
        DL_SIDELIGHTS = 65536,

        /// <summary>
        /// sidelights symbol
        /// </summary>
        DL_NEUTRAL = 131072,

        /// <summary>
        /// set if engine damage is severe
        /// </summary>
        DLF_ENGINE_SEVERE = 0x10000000,
    }
}
