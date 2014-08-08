namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the screenshot options.
    /// </summary>
    public enum ScreenshotError {
        /// <summary>
        /// OK - completed instruction.
        /// </summary>
        SSH_OK,

        /// <summary>
        /// Can't save a screenshot - dedicated host.
        /// </summary>
        SSH_DEDICATED,

        /// <summary>
        /// <see cref="IS_SSH"/> corrupted (e.g. BMP does not end with zero).
        /// </summary>
        SSH_CORRUPTED,

        /// <summary>
        /// Could not save the screenshot.
        /// </summary>
        SSH_NO_SAVE,
    }
}
