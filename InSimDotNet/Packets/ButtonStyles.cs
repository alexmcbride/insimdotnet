using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_BTN"/> BStyle.
    /// </summary>
    [Flags]
    public enum ButtonStyles {
        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C0 = 0,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C1 = 1,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C2 = 2,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C3 = 3,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C4 = 4,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C5 = 5,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C6 = 6,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C7 = 7,

        /// <summary>
        /// Choose a standard interface color.
        /// </summary>
        ISB_C8 = 8,

        /// <summary>
        /// When clicked this button will send a <see cref="IS_BTC"/> packet.
        /// </summary>
        ISB_CLICK = 8,

        /// <summary>
        /// Light button.
        /// </summary>
        ISB_LIGHT = 16,

        /// <summary>
        /// Dark button.
        /// </summary>
        ISB_DARK = 32,

        /// <summary>
        /// Align text to left.
        /// </summary>
        ISB_LEFT = 64,

        /// <summary>
        /// Align text to right.
        /// </summary>
        ISB_RIGHT = 128,
    }
}
