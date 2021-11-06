namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the replay errors.
    /// </summary>
    public enum ReplayError {
        /// <summary>
        /// OK - completed instruction.
        /// </summary>
        RIP_OK,

        /// <summary>
        /// OK - already at the destination.
        /// </summary>
        RIP_ALREADY,

        /// <summary>
        /// Can't run a replay - dedicated host.
        /// </summary>
        RIP_DEDICATED,

        /// <summary>
        /// Can't start a replay - not in a suitable mode.
        /// </summary>
        RIP_WRONG_MODE,

        /// <summary>
        /// RName is zero but no replay is currently loaded.
        /// </summary>
        RIP_NOT_REPLAY,

        /// <summary>
        /// IS_RIP corrupted (e.g. RName does not end with zero).
        /// </summary>
        RIP_CORRUPTED,

        /// <summary>
        /// The replay file was not found.
        /// </summary>
        RIP_NOT_FOUND,

        /// <summary>
        /// Obsolete / future / corrupted.
        /// </summary>
        RIP_UNLOADABLE,

        /// <summary>
        /// Destination is beyond replay length.
        /// </summary>
        RIP_DEST_OOB,

        /// <summary>
        /// Unknown error found starting replay.
        /// </summary>
        RIP_UNKNOWN,

        /// <summary>
        /// Replay search was terminated by user.
        /// </summary>
        RIP_USER,

        /// <summary>
        /// Can't reach destination - SPR is out of sync.
        /// </summary>
        RIP_OOS,
    }
}
