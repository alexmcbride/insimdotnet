using System;
using System.Diagnostics.CodeAnalysis;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_OBH"/> OBHFlags.
    /// </summary>
    [Flags]
    public enum ObjectFlags {
        /// <summary>
        /// An added object.
        /// </summary>
        OBH_LAYOUT = 1,

        /// <summary>
        /// A movable object.
        /// </summary>
        OBH_CAN_MOVE = 2,

        /// <summary>
        /// Was moving before this hit.
        /// </summary>
        OBH_WAS_MOVING = 4,

        /// <summary>
        /// Object in original position.
        /// </summary>
        OBH_ON_SPOT = 8,
    }
}
