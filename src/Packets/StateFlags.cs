using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the state flags.
    /// </summary>
    [Flags]
    public enum StateFlags {
        /// <summary>
        /// In game (or MPR).
        /// </summary>
        ISS_GAME = 1,

        /// <summary>
        /// In SPR.
        /// </summary>
        ISS_REPLAY = 2,

        /// <summary>
        /// Paused.
        /// </summary>
        ISS_PAUSED = 4,

        /// <summary>
        /// In Shift-U mode.
        /// </summary>
        ISS_SHIFTU = 8,

        /// <summary>
        /// In a dialog.
        /// </summary>
        ISS_DIALOG = 16,

        /// <summary>
        /// Following car.
        /// </summary>
        ISS_SHIFTU_FOLLOW = 32,

        /// <summary>
        /// Shift-U buttons hidden.
        /// </summary>
        ISS_SHIFTU_NO_OPT = 64,

        /// <summary>
        /// Showing 2D display.
        /// </summary>
        ISS_SHOW_2D = 128,

        /// <summary>
        /// Entry screen.
        /// </summary>
        ISS_FRONT_END = 256,

        /// <summary>
        /// Multiplayer mode.
        /// </summary>
        ISS_MULTI = 512,

        /// <summary>
        /// Multiplayer speedup option.
        /// </summary>
        ISS_MPSPEEDUP = 1024,

        /// <summary>
        /// LFS running in a window.
        /// </summary>
        ISS_WINDOWED = 2048,

        /// <summary>
        /// Sound if switched off.
        /// </summary>
        ISS_SOUND_MUTE = 4096,

        /// <summary>
        /// Override user view.
        /// </summary>
        ISS_VIEW_OVERRIDE = 8192,

        /// <summary>
        /// InSim buttons visible.
        /// </summary>
        ISS_VISIBLE = 16384,

        /// <summary>
        /// In a text entry dialog
        /// </summary>
        ISS_TEXT_ENTRY = 32768
    }
}
