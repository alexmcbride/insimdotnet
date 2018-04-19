using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the car setup flags.
    /// </summary>
    [Flags]
    public enum SetupFlags {
        /// <summary>
        /// Symmetrical wheels.
        /// </summary>
        SETF_SYMM_WHEELS = 1,

        /// <summary>
        /// Traction control enabled.
        /// </summary>
        SETF_TC_ENABLE = 2,

        /// <summary>
        /// Anti-lock brakes enabled.
        /// </summary>
        SETF_ABS_ENABLE = 4,
    }
}
