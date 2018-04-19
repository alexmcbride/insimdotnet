using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="HInfo"/> Flags.
    /// </summary>
    [Flags]
    public enum HostFlags {
        /// <summary>
        /// Host requires a spectator password
        /// </summary>
        HOS_SPECPASS = 1,

        /// <summary>
        /// Host is licensed (S1, S2 etc..)
        /// </summary>
        HOS_LICENSED = 2,

        /// <summary>
        /// Host is S1
        /// </summary>
        HOS_S1 = 4,

        /// <summary>
        /// Host is S2
        /// </summary>
        HOS_S2 = 8,

        /// <summary>
        /// First <see cref="HInfo"/> in this set of <see cref="IR_HOS"/> packets.
        /// </summary>
        HOS_FIRST = 64,

        /// <summary>
        /// Last <see cref="HInfo"/> in this set of <see cref="IR_HOS"/> packets.
        /// </summary>
        HOS_LAST = 128,
    }
}
