namespace InSimDotNet.Packets {
    /// <summary>
    /// Submode identifiers to represent the IS_CIM SubMode attribute when Mode
    /// is set to CIM_NORMAL.
    /// </summary>
    public enum NormalSubMode {
        /// <summary>
        /// Not in a specific view.
        /// </summary>
        NRM_NORMAL,
        
        /// <summary>
        /// User is viewing the car's wheel temperature (F9).
        /// </summary>
        NRM_WHEEL_TEMPS,

        /// <summary>
        /// User is viewing the car's wheel damage (F10).
        /// </summary>
        NRM_WHEEL_DAMAGE,

        /// <summary>
        /// User is viewing the setting pane for the car (F11).
        /// </summary>
        NRM_LIVE_SETTINGS,

        /// <summary>
        /// User is viewing the pit instructions pane (F12).
        /// </summary>
        NRM_PIT_INSTRUCTIONS
    }
}
