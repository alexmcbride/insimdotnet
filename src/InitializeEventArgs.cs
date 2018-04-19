using System;

namespace InSimDotNet {
    /// <summary>
    /// Provides data for the <see cref="InSim"/> initialized event.
    /// </summary>
    public class InitializeEventArgs : EventArgs {
        /// <summary>
        /// Gets the settings used to initialize the connection with LFS.
        /// </summary>
        public ReadOnlyInSimSettings Settings { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="InitializeEventArgs"/> object.
        /// </summary>
        /// <param name="settings">The InSim settings used to initialize the connection with LFS.</param>
        public InitializeEventArgs(ReadOnlyInSimSettings settings) {
            this.Settings = settings;
        }
    }
}
