namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents <see cref="IS_MSO"/> user types.
    /// </summary>
    public enum UserType {
        /// <summary>
        /// System message.
        /// </summary>
        MSO_SYSTEM,

        /// <summary>
        /// Normal visible user message.
        /// </summary>
        MSO_USER,

        /// <summary>
        /// Hidden message starting with special prefix (see ISI prefix).
        /// </summary>
        MSO_PREFIX,

        /// <summary>
        /// Hidden message typed on local PC with /o command.
        /// </summary>
        MSO_O,
    }
}
