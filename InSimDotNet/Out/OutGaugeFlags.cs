using System;

namespace InSimDotNet.Out {
    /// <summary>
    /// OutGauge packet flags.
    /// </summary>
    [Flags]
    public enum OutGaugeFlags {
        /// <summary>
        /// Key,
        /// </summary>
        OG_SHIFT = 1,

        /// <summary>
        /// Key,
        /// </summary>
        OG_CTRL = 2,

        /// <summary>
        /// Show turbo gauge
        /// </summary>
        OG_TURBO = 8192,

        /// <summary>
        /// Use prefers KM (as opposed to Miles).
        /// </summary>
        OG_KM = 16384,

        /// <summary>
        /// Use prefers Bars (as opposed to PSI).
        /// </summary>
        OG_BAR = 32768,
    }
}
