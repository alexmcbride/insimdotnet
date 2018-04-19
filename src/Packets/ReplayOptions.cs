using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the replay options.
    /// </summary>
    [Flags]
    public enum ReplayOptions {
        /// <summary>
        /// Loop replay.
        /// </summary>
        RIPOPT_LOOP = 1,

        /// <summary>
        /// Download missing skins.
        /// </summary>
        RIPOPT_SKINS = 2,

        /// <summary>
        /// Have LFS calculate full physics during replay seek.
        /// </summary>
        RIPOPT_FULL_PHYS = 4,
    }
}
