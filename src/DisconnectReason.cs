namespace InSimDotNet {
    /// <summary>
    /// Specifies the disconnect reason for the <see cref="DisconnectedEventArgs"/> class.
    /// </summary>
    public enum DisconnectReason {
        /// <summary>
        /// The connection with LFS was been lost (disconnected etc..).
        /// </summary>
        Lost,

        /// <summary>
        /// The connection was closed by request.
        /// </summary>
        Request,
    }
}
