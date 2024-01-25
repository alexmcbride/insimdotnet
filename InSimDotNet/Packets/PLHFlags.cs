using System;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Represents the <see cref="IS_PLH"/> flags.
    /// </summary>
    [Flags]
    public enum PLHFlags {
        /// <summary>
        /// Has/Set Mass handicap.
        /// </summary>
        SetMass = 0x01,
        /// <summary>
        /// Has/Set Intake restriction.
        /// </summary>
        SetTRes = 0x02,
        /// <summary>
        /// Silent.
        /// </summary>
        Silent = 0x80,
    }
}
