using System;
using System.Runtime.Serialization;

namespace InSimDotNet {
    /// <summary>
    /// Generic exception thrown by InSim.NET.
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Creates a new instance of the <see cref="InSimException"/> class..
        /// </summary>
        protected InSimException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
