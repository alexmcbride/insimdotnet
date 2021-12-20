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
        FRONT_FEMALE = 0x01,

        /// <summary>
        /// Passenger in front.
        /// </summary>
        FRONT_OCCUPIED = 0x02,

        /// <summary>
        /// Rear-left passenger is female.
        /// </summary>
        REAR_LEFT_FEMALE = 0x04,

        /// <summary>
        /// Passenger in rear-left.
        /// </summary>
        REAR_LEFT_OCCUPIED = 0x08,

        /// <summary>
        /// Rear-middle passenger is female.
        /// </summary>
        REAR_MIDDLE_FEMALE = 0x10,

        /// <summary>
        /// Passenger in rear-middle.
        /// </summary>
        REAR_MIDDLE_OCCUPIED = 0x20,

        /// <summary>
        /// Rear-right passenger is female.
        /// </summary>
        REAR_RIGHT_FEMALE = 0x40,

        /// <summary>
        /// Passenger in rear-right.
        /// </summary>
        REAR_RIGHT_OCCUPIED = 0x80,
    }
}
