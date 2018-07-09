namespace InSimDotNet.Packets {
    /// <summary>
    /// Submode identifiers to represent the IS_CIM SubMode attribute when Mode
    /// is set to CIM_GARAGE.
    /// </summary>
    public enum GarageSubMode {
        /// <summary>
        /// General information pane.
        /// </summary>
        GRG_INFO,

        /// <summary>
        /// Colours adjustment pane.
        /// </summary>
        GRG_COLOURS,

        /// <summary>
        /// Braking and traction control pane.
        /// </summary>
        GRG_BRAKE_TC,

        /// <summary>
        /// Suspension adjustment pane.
        /// </summary>
        GRG_SUSP,

        /// <summary>
        /// Steering adjustment pane.
        /// </summary>
        GRG_STEER,

        /// <summary>
        /// Drivetrain configuration pane (e.g. transmission, differentials).
        /// </summary>
        GRG_DRIVE,

        /// <summary>
        /// Tyres selection pane.
        /// </summary>
        GRG_TYRES,

        /// <summary>
        /// Aero dynamism of the car (if available).
        /// </summary>
        GRG_AERO,

        /// <summary>
        /// Undocumented.
        /// </summary>
        GRG_PASS
    }
}
