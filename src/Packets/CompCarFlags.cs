using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the <see cref="CompCar"/> Info.
    /// </summary>
    [Flags]
    public enum CompCarFlags {
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

        /// <summary>
        /// This is the first <see cref="CompCar"/> in this set of <see cref="IS_MCI"/> packets
        /// </summary>
        CCI_FIRST = 64,

        /// <summary>
        /// This is the last <see cref="CompCar"/> in this set of <see cref="IS_MCI"/> packets
        /// </summary>
        CCI_LAST = 128,
    }
}
