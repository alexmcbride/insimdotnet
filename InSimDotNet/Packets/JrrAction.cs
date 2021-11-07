namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents JRR packet actions.
    /// </summary>
    public enum JrrAction {
        /// <summary>
        /// Reject new player.
        /// </summary>
        JRR_REJECT = 0,

        /// <summary>
        /// Allow new player.
        /// </summary>
        JRR_SPAWN = 1,

        //JRR_2,
        //JRR_3,

        /// <summary>
        /// Reset?
        /// </summary>
        JRR_RESET = 4,

        /// <summary>
        /// No repair?
        /// </summary>
        JRR_RESET_NO_REPAIR = 5,

        //JRR_6,
        //JRR_7,
    }
}
