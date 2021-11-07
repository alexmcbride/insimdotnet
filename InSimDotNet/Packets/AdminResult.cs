namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents <see cref="IS_ACR"/> Result.
    /// </summary>
    public enum AdminResult {
        /// <summary>
        /// No admin command processed.
        /// </summary>
        None = 0,

        /// <summary>
        /// Admin command has been processed.
        /// </summary>
        Processed = 1,

        /// <summary>
        /// Admin command was rejected.
        /// </summary>
        Rejected = 2,

        /// <summary>
        /// The admin command was not recognised.
        /// </summary>
        Unknown = 3
    }
}
