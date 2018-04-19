using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="CarContact"/> Info.
    /// </summary>
    [Flags]
    public enum ContactFlags {
        /// <summary>
        /// This car is in the way of a driver who is a lap ahead
        /// </summary>
        CCI_BLUE = 1,

        /// <summary>
        /// This car is slow or stopped and in a dangerous place
        /// </summary>
        CCI_YELLOW = 2,

        /// <summary>
        /// This car is lagging (missing or delayed position packets)
        /// </summary>
        CCI_LAG = 32,
    }
}
