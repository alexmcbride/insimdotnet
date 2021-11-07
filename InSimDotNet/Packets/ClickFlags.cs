using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_BTC"/> CFlags.
    /// </summary>
    [Flags]
    public enum ClickFlags {
        /// <summary>
        /// Left mouse button.
        /// </summary>
        ISB_LMB = 1,

        /// <summary>
        /// Right mouse button.
        /// </summary>
        ISB_RMB = 2,

        /// <summary>
        /// Ctrl + click.
        /// </summary>
        ISB_CTRL = 4,

        /// <summary>
        /// Shift + click.
        /// </summary>
        ISB_SHIFT = 8,
    }
}
