namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the camera view identifiers.
    /// </summary>
    public enum ViewIndentifier {
        /// <summary>
        /// Arcade.
        /// </summary>
        VIEW_FOLLOW,

        /// <summary>
        /// Helicopter.
        /// </summary>
        VIEW_HELI,

        /// <summary>
        /// TV camera.
        /// </summary>
        VIEW_CAM,

        /// <summary>
        /// Driver.
        /// </summary>
        VIEW_DRIVER,

        /// <summary>
        /// Custom
        /// </summary>
        VIEW_CUSTOM,

        /// <summary>
        /// Max
        /// </summary>
        VIEW_MAX,

        /// <summary>
        /// Viewing another player
        /// </summary>
        VIEW_ANOTHER = 255
    }
}
