using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the IS_OCO start light bulb flags.
    /// </summary>
    [Flags]
    public enum BulbInfo {
        /// <summary>
        /// First start light
        /// </summary>
        Red1 = 1,

        /// <summary>
        /// Second start light
        /// </summary>
        Red2 = 2,

        /// <summary>
        /// Third start light
        /// </summary>
        Red3 = 4,

        /// <summary>
        /// GO GO GO!
        /// </summary>
        Green = 8
    }
}
