namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the vote actions.
    /// </summary>
    public enum VoteAction {
        /// <summary>
        /// No vote.
        /// </summary>
        VOTE_NONE,

        /// <summary>
        /// End race.
        /// </summary>
        VOTE_END,

        /// <summary>
        /// Restart session.
        /// </summary>
        VOTE_RESTART,

        /// <summary>
        /// Qualify.
        /// </summary>
        VOTE_QUALIFY
    }
}
