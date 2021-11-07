namespace InSimDotNet.Packets {
    /// <summary>
    /// UCOAction enum
    /// </summary>
    public enum UCOAction {
        /// <summary>
        /// Entered a circle
        /// </summary>
        UCO_CIRCLE_ENTER,

        /// <summary>
        /// Left a circle
        /// </summary>
        UCO_CIRCLE_LEAVE,

        /// <summary>
        /// Crossed checkpoint in forward direction
        /// </summary>
        UCO_CP_FWD,

        /// <summary>
        /// Crossed checkpoint in reverse direction
        /// </summary>
        UCO_CP_REV,
    }
}
