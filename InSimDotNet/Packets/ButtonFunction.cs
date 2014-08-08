namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_BFN"/> SubT.
    /// </summary>
    public enum ButtonFunction {
        /// <summary>
        /// Delete one button (must set ClickID).
        /// </summary>
        BFN_DEL_BTN,

        /// <summary>
        /// Clear all buttons made by this InSim instance.
        /// </summary>
        BFN_CLEAR,

        /// <summary>
        ///  User cleared this InSim instance's buttons.
        /// </summary>
        BFN_USER_CLEAR,

        /// <summary>
        /// SHIFT+B or SHIFT+I - request for buttons.
        /// </summary>
        BFN_REQUEST,
    }
}
