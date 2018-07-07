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

        /// <summary>
        /// Send IS_AXM every time the selection changes
        /// </summary>
        TTC_SEL_START,

        /// <summary>
        /// Switch off IS_AXM requested by TTC_SEL_START
        /// </summary>
        TTC_SET_STOP
    }
}
