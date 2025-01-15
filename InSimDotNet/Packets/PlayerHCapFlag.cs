using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InSimDotNet.Packets
{
    [Flags]
    public enum PlayerHCapFlag
    {
        /// <summary>
        /// Set mass
        /// </summary>
        H_Mass = 0x1,

        /// <summary>
        /// Set intake restriction
        /// </summary>
        H_TRes = 0x2,

        /// <summary>
        /// Add restrictions silently
        /// </summary>
        Silent = 0x80
    }
}
