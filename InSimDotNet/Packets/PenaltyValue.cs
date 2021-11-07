namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the penalty values.
    /// </summary>
    public enum PenaltyValue {
        /// <summary>
        /// No penalty.
        /// </summary>
        PENALTY_NONE,

        /// <summary>
        /// Drive-through.
        /// </summary>
        PENALTY_DT,

        /// <summary>
        /// Drive-through valid.
        /// </summary>
        PENALTY_DT_VALID,

        /// <summary>
        /// Stop and go.
        /// </summary>
        PENALTY_SG,

        /// <summary>
        /// Stop and go valid.
        /// </summary>
        PENALTY_SG_VALID,

        /// <summary>
        /// 30 seconds.
        /// </summary>
        PENALTY_30,

        /// <summary>
        /// 45 seconds.
        /// </summary>
        PENALTY_45,
    }
}
