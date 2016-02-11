namespace InSimDotNet.Packets
{
    /// <summary>
    /// Represents the <see cref="IS_TTC"/> SubT.
    /// </summary>
    public enum TtcType
    {
        /// <summary>
        /// not used
        /// </summary>
        TTC_NONE,

        /// <summary>
        /// send IS_AXM for a layout editor selection
        /// </summary>
        TTC_SEL,
    }
}
