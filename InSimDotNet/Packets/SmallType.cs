namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_SMALL"/> SubT.
    /// </summary>
    public enum SmallType {
        /// <summary>
        /// Not used
        /// </summary>
        SMALL_NONE,

        /// <summary>
        /// Start sending positions
        /// </summary>
        SMALL_SSP,

        /// <summary>
        /// Start sending gauges
        /// </summary>
        SMALL_SSG,

        /// <summary>
        /// Vote action
        /// </summary>
        SMALL_VTA,

        /// <summary>
        /// Time stop
        /// </summary>
        SMALL_TMS,

        /// <summary>
        /// Time step
        /// </summary>
        SMALL_STP,

        /// <summary>
        /// Race time packet (reply to GTH)
        /// </summary>
        SMALL_RTP,

        /// <summary>
        /// Set node lap interval
        /// </summary>
        SMALL_NLI,

        /// <summary>
        /// Set allowed cars (use TINY_ALC to request)
        /// </summary>
        SMALL_ALC
    }
}
