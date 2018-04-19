using System;

namespace InSimDotNet {
    /// <summary>
    /// Generic exception thrown by InSim.NET.
    /// </summary>
    public class InSimException : Exception {
        /// <summary>
        /// Creates a new instance of the <see cref="InSimException"/> class.
        /// </summary>
        public InSimException() { }

        /// <summary>
        /// Creates a new instance of the <see cref="InSimException"/> class.
        /// </summary>
        public InSimException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of the <see cref="InSimException"/> class.
        /// </summary>
        public InSimException(string message, Exception inner) : base(message, inner) { }
    }
}
