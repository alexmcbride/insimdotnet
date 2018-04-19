namespace InSimDotNet.Packets
{
    /// <summary>
    /// Represents the <see cref="IS_TTC"/> SubT.
    /// </summary>
    public enum TtcType
    {
        /// <summary>
        /// Not used
        /// </summary>
        TTC_NONE,

        /// <summary>
        /// Send IS_AXM for a layout editor selection
        /// </summary>
        TTC_SEL,
    }
}
