using System;

namespace InSimDotNet {
    /// <summary>
    /// Provides data for the <see cref="InSim"/> InSimError event.
    /// </summary>
    public class InSimErrorEventArgs : EventArgs {
        /// <summary>
        /// Gets the <see cref="Exception"/> which has occurred.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="InSimErrorEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> which has occurred.</param>
        public InSimErrorEventArgs(Exception exception) {
            Exception = exception;
        }
    }
}
