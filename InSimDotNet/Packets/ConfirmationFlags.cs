using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents confirmation flags.
    /// </summary>
    [Flags]
    public enum ConfirmationFlags {
        /// <summary>
        /// Mentioned.
        /// </summary>
        CONF_MENTIONED = 1,

        /// <summary>
        /// Confirmed.
        /// </summary>
        CONF_CONFIRMED = 2,

        /// <summary>
        /// Has drive-through penalty
        /// </summary>
        CONF_PENALTY_DT = 4,

        /// <summary>
        /// Has stop-and-go penalty
        /// </summary>
        CONF_PENALTY_SG = 8,

        /// <summary>
        /// Has 30 second time penalty
        /// </summary>
        CONF_PENALTY_30 = 16,

        /// <summary>
        /// Has 45 second time penalty
        /// </summary>
        CONF_PENALTY_45 = 32,

        /// <summary>
        /// Did not complete a required pit stop
        /// </summary>
        CONF_DID_NOT_PIT = 64,

        /// <summary>
        /// Is disqualified
        /// </summary>
        CONF_DISQ = CONF_PENALTY_DT | CONF_PENALTY_SG | CONF_DID_NOT_PIT,

        /// <summary>
        /// Has time penalty
        /// </summary>
        CONF_TIME = CONF_PENALTY_30 | CONF_PENALTY_45,
    }
}
