using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the IS_OCO start light bulb flags.
    /// </summary>
    [Flags]
    public enum BulbInfo {
        /// <summary>
        /// When the...
        /// </summary>
        Red1 = 1,

        /// <summary>
        /// Three lights...
        /// </summary>
        Red2 = 2,

        /// <summary>
        /// Go out...
        /// </summary>
        Red3 = 4,

        /// <summary>
        /// It's GO GO GO!
        /// </summary>
        Green = 8
    }
}
