using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_HLV"/> packets's HLVC property.
    /// </summary>
    [Flags]
    public enum HlvcFlags {
        /// <summary>
        /// Car has hit the groud.
        /// </summary>
        Ground = 0,

        /// <summary>
        /// Car has hit a wall.
        /// </summary>
        Wall = 1,

        /// <summary>
        /// Car was speeding in pits.
        /// </summary>
        Speeding = 4,
    }
}
