
namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="IS_FLG"/> Flag.
    /// </summary>
    public enum FlagType {
        /// <summary>
        /// No flag shown.
        /// </summary>
        None,

        /// <summary>
        /// Blue flag (car being lapped).
        /// </summary>
        Blue,

        /// <summary>
        /// Yellow flag (car is slow or stopped in dangerous place).
        /// </summary>
        Yellow,
    }
}
