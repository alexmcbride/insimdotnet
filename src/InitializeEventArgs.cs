using System;

namespace InSimDotNet {
    /// <summary>
    /// Provides data for the <see cref="InSimClient"/> initialized event.
    /// </summary>
    public class InitializeEventArgs : EventArgs {
        /// <summary>
        /// Gets the settings used to initialize the connection with LFS.
        /// </summary>
        public InSimSettings Settings { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="InitializeEventArgs"/> object.
        /// </summary>
        /// <param name="settings">The InSim settings used to initialize the connection with LFS.</param>
        public InitializeEventArgs(InSimSettings settings) {
            this.Settings = settings;
        }
    }
}
