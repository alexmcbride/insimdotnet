using System;
using System.Diagnostics.CodeAnalysis;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Passenger flags.
    /// </summary>
    [Flags]
    public enum PassengerFlags {
        /// <summary>
        /// Front passenger is female.
        /// </summary>
        FRONT_FEMALE,

        /// <summary>
        /// Passenger in front.
        /// </summary>
        FRONT_OCCUPIED,

        /// <summary>
        /// Rear-left passenger is female.
        /// </summary>
        REAR_LEFT_FEMALE,

        /// <summary>
        /// Passenger in rear-left.
        /// </summary>
        REAR_LEFT_OCCUPIED,

        /// <summary>
        /// Rear-middle passenger is female.
        /// </summary>
        REAR_MIDDLE_FEMALE,

        /// <summary>
        /// Passenger in rear-middle.
        /// </summary>
        REAR_MIDDLE_OCCUPIED,

        /// <summary>
        /// Rear-right passenger is female.
        /// </summary>
        REAR_RIGHT_FEMALE,

        /// <summary>
        /// Passenger in rear-right.
        /// </summary>
        REAR_RIGHT_OCCUPIED,
    }
}
