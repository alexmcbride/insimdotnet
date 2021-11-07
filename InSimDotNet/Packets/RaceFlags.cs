using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the host race flags.
    /// </summary>
    [Flags]
    public enum RaceFlags {
        /// <summary>
        /// Voting.
        /// </summary>
        HOSTF_CAN_VOTE = 1,

        /// <summary>
        /// Track select.
        /// </summary>
        HOSTF_CAN_SELECT = 2,

        /// <summary>
        /// Mid-race join.
        /// </summary>
        HOSTF_MID_RACE = 32,

        /// <summary>
        /// Mandatory pit-stop.
        /// </summary>
        HOSTF_MUST_PIT = 64,

        /// <summary>
        /// Can reset car.
        /// </summary>
        HOSTF_CAN_RESET = 128,

        /// <summary>
        /// Forced cockpit view.
        /// </summary>
        HOSTF_FCV = 256,

        /// <summary>
        /// Cruise mode.
        /// </summary>
        HOSTF_CRUISE = 512,
    }
}
