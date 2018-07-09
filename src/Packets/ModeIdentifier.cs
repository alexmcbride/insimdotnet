namespace InSimDotNet.Packets {
    /// <summary>
    /// Modes to represent the IS_CIM Mode attribute.
    /// </summary>
    public enum ModeIdentifier {
        /// <summary>
        /// Not in a special mode.
        /// </summary>
        CIM_NORMAL,

        /// <summary>
        /// In the options menu.
        /// </summary>
        CIM_OPTIONS,

        /// <summary>
        /// In the host's options menu.
        /// </summary>
        CIM_HOST_OPTIONS,

        /// <summary>
        /// In the garage.
        /// </summary>
        CIM_GARAGE,

        /// <summary>
        /// In the car selection menu.
        /// </summary>
        CIM_CAR_SELECT,

        /// <summary>
        /// In the track selection menu.
        /// </summary>
        CIM_TRACK_SELECT,

        /// <summary>
        /// In the free view mode (Shift + U).
        /// </summary>
        CIM_SHIFTU
    }
}
