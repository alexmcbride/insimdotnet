using System;
using System.Diagnostics.CodeAnalysis;

namespace InSimDotNet.Out {
    /// <summary>
    /// OutGauge packet ShowLights and DashLight flags.
    /// </summary>
    [Flags]
    public enum DashLightFlags {
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
    }
}
