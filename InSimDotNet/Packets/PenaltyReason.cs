using System;
using System.Diagnostics.CodeAnalysis;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the penalty reason.
    /// </summary>
    public enum PenaltyReason {
        /// <summary>
        /// Unknown or cleared penalty
        /// </summary>
        PENR_UNKNOWN,

        /// <summary>
        /// Penalty given by admin
        /// </summary>
        PENR_ADMIN,

        /// <summary>
        /// Wrong way driving
        /// </summary>
        PENR_WRONG_WAY,

        /// <summary>
        /// Starting before green light
        /// </summary>
        PENR_FALSE_START,

        /// <summary>
        /// Speeding in pit lane
        /// </summary>
        PENR_SPEEDING,

        /// <summary>
        /// Stop-go pit stop too short
        /// </summary>
        PENR_STOP_SHORT,

        /// <summary>
        /// Compulsory stop is too late
        /// </summary>
        PENR_STOP_LATE,
    }
}
