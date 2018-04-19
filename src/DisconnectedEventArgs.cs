using System;

namespace InSimDotNet {
    /// <summary>
    /// Provides data for the <see cref="InSim"/> Disconnected event.
    /// </summary>
    public class DisconnectedEventArgs : EventArgs {
        /// <summary>
        /// Gets the reason for the disconnection.
        /// </summary>
        public DisconnectReason Reason { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="DisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="reason">The reason for the disconnection.</param>
        public DisconnectedEventArgs(DisconnectReason reason) {
            Reason = reason;
        }
    }
}
