namespace InSimDotNet.Packets {
    /// <summary>
    /// Enumeration for the <see cref="IS_CNL"/> disconnect reason.
    /// </summary>
    public enum LeaveReason {
        /// <summary>
        /// Disconnect.
        /// </summary>
        LEAVR_DISCO,

        /// <summary>
        /// Timed out.
        /// </summary>
        LEAVR_TIMEOUT,

        /// <summary>
        /// Lost connection.
        /// </summary>
        LEAVR_LOSTCONN,

        /// <summary>
        /// Kicked.
        /// </summary>
        LEAVR_KICKED,

        /// <summary>
        /// Banned.
        /// </summary>
        LEAVR_BANNED,

        /// <summary>
        /// Out-of-sync (OOS) or cheat protection.
        /// </summary>
        LEAVR_SECURITY,

        /// <summary>
        /// Cheat protection wrong.
        /// </summary>
        LEAVR_CPW,

        /// <summary>
        /// Out of sync with host.
        /// </summary>
        LEAVR_OOS,

        /// <summary>
        /// Join OOS (initial sync failed).
        /// </summary>
        LEAVR_JOOS,

        /// <summary>
        /// Invalid packet.
        /// </summary>
        LEAVR_HACK,
    }
}
