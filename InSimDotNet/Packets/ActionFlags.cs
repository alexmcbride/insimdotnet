namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_AXM"/> PMOAction flags.
    /// </summary>
    public enum ActionFlags {
        /// <summary>
        /// Sent by the layout loading system only.
        /// </summary>
        PMO_LOADING_FILE,

        /// <summary>
        /// Adding objects (from InSim or editor).
        /// </summary>
        PMO_ADD_OBJECTS,

        /// <summary>
        /// Delete objects (from InSim or editor).
        /// </summary>
        PMO_DEL_OBJECTS,

        /// <summary>
        /// Clear all objects (NumO must be zero).
        /// </summary>
        PMO_CLEAR_ALL
    }
}
