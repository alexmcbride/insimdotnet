namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_SMALL"/> SubT.
    /// </summary>
    public enum SmallType {
        /// <summary>
        /// Not used
        /// </summary>
        SMALL_NONE = 0,

        /// <summary>
        /// Start sending positions
        /// </summary>
        SMALL_SSP = 1,

        /// <summary>
        /// Start sending gauges
        /// </summary>
        SMALL_SSG = 2,

        /// <summary>
        /// Vote action
        /// </summary>
        SMALL_VTA = 3,

        /// <summary>
        /// Time stop
        /// </summary>
        SMALL_TMS = 4,

        /// <summary>
        /// Time step
        /// </summary>
        SMALL_STP = 5,

        /// <summary>
        /// Race time packet (reply to GTH)
        /// </summary>
        SMALL_RTP = 6,

        /// <summary>
        /// Set node lap interval
        /// </summary>
        SMALL_NLI = 7,

        /// <summary>
        /// Set allowed cars (use TINY_ALC to request)
        /// </summary>
        SMALL_ALC = 8,

        /// <summary>
        /// Set local car switches (lights, horn, siren). Use <see cref="LocalCarSwitches"/> for options.
        /// </summary>
        SMALL_LCS = 9
    }
}
