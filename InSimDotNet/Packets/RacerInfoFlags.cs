using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_NPL"/> RacerInfoFlags flags.
    /// </summary>
    [Flags]
    public enum RacerInfoFlags
    {
        /// <summary>
        /// Joined after race started
        /// </summary>
        RIF_LATE_START = 1,

        /// <summary>
        /// Unmovable object without collision
        /// </summary>
        RIF_SAI_NON_SOLID = 8,

        /// <summary>
        /// RIF_SAI_0
        /// </summary>
        RIF_SAI_0 = 16,

        /// <summary>
        /// RIF_SAI_1
        /// </summary>
        RIF_SAI_1 = 32,
    }
}
