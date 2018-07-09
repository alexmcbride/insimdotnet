namespace InSimDotNet.Packets {
    /// <summary>
    /// Submode identifiers to represent the IS_CIM SubMode attribute when Mode
    /// is set to CIM_SHIFTU.
    /// </summary>
    public enum ShiftUSubMode {
        /// <summary>
        /// No buttons displayed.
        /// </summary>
        FVM_PLAIN,

        /// <summary>
        /// Buttons displayed (not editing)
        /// </summary>
        FVM_BUTTONS,

        /// <summary>
        /// Edit mode.
        /// </summary>
        FVM_EDIT
    }
}
