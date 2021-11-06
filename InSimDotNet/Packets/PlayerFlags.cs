using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the player flags.
    /// </summary>
    [Flags]
    public enum PlayerFlags {
        /// <summary>
        /// Left-hand drive.
        /// </summary>
        PIF_SWAPSIDE = 1,

        /// <summary>
        /// Auto gears enabled.
        /// </summary>
        PIF_AUTOGEARS = 8,

        /// <summary>
        /// H-Shifter.
        /// </summary>
        PIF_SHIFTER = 16,

        /// <summary>
        /// Brake help.
        /// </summary>
        PIF_HELP_B = 64,

        /// <summary>
        /// Axis clutch pedal.
        /// </summary>
        PIF_AXIS_CLUTCH = 128,

        /// <summary>
        /// In pits.
        /// </summary>
        PIF_INPITS = 256,

        /// <summary>
        /// Auto-clutch.
        /// </summary>
        PIF_AUTOCLUTCH = 512,

        /// <summary>
        /// Mouse steering.
        /// </summary>
        PIF_MOUSE = 1024,

        /// <summary>
        /// Keyboard no help.
        /// </summary>
        PIF_KB_NO_HELP = 2048,

        /// <summary>
        /// Keyboard stabilized.
        /// </summary>
        PIF_KB_STABILISED = 4096,

        /// <summary>
        /// Custom view.
        /// </summary>
        PIF_CUSTOM_VIEW = 8192,
    }
}
