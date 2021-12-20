using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Represents the pit work flags.
    /// </summary>
    [Flags]
    public enum PitWorkFlags {
        /// <summary>
        /// No work done.
        /// </summary>
        PSE_NOTHING = 0x1,

        /// <summary>
        /// Stopped at pit-box.
        /// </summary>
        PSE_STOP = 0x2,

        /// <summary>
        /// Front damage.
        /// </summary>
        PSE_FR_DAM = 0x4,

        /// <summary>
        /// Front wheels changed.
        /// </summary>
        PSE_FR_WHL = 0x8,

        /// <summary>
        /// Left-front damage.
        /// </summary>
        PSE_LE_FR_DAM = 0x10,

        /// <summary>
        /// Left-front wheels changed.
        /// </summary>
        PSE_LE_FR_WHL = 0x20,

        /// <summary>
        /// Right-front damaged.
        /// </summary>
        PSE_RI_FR_DAM = 0x40,

        /// <summary>
        /// Right-front wheel changed.
        /// </summary>
        PSE_RI_FR_WHL = 0x80,

        /// <summary>
        /// Rear damage.
        /// </summary>
        PSE_RE_DAM = 0x100,

        /// <summary>
        /// Rear wheels changed.
        /// </summary>
        PSE_RE_WHL = 0x200,

        /// <summary>
        /// Left-rear damage.
        /// </summary>
        PSE_LE_RE_DAM = 0x400,

        /// <summary>
        /// Left-rear wheel changed.
        /// </summary>
        PSE_LE_RE_WHL = 0x800,

        /// <summary>
        /// Right-rear damage.
        /// </summary>
        PSE_RI_RE_DAM = 0x1000,

        /// <summary>
        /// Right-rear wheel changed.
        /// </summary>
        PSE_RI_RE_WHL = 0x2000,

        /// <summary>
        /// Minor body damage.
        /// </summary>
        PSE_BODY_MINOR = 0x4000,

        /// <summary>
        /// Major body damage.
        /// </summary>
        PSE_BODY_MAJOR = 0x8000,

        /// <summary>
        /// Setup changed.
        /// </summary>
        PSE_SETUP = 0x10000,

        /// <summary>
        /// Refuelled.
        /// </summary>
        PSE_REFUEL = 0x20000,
    }
}
