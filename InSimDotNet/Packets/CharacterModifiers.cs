using System;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Represents the <see cref="IS_SCH"/> flags.
    /// </summary>
    [Flags]
    public enum CharacterModifiers {
        /// <summary>
        /// Shift key.
        /// </summary>
        SHIFT = 1,

        /// <summary>
        /// Control key.
        /// </summary>
        CTRL = 2,
    }
}
