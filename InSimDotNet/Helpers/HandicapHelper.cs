using InSimDotNet.Packets;
using System.Collections.Generic;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Helper class for setting car handicaps.
    /// </summary>
    public static class HandicapHelper {
        private static readonly Dictionary<CarFlags, byte> CarMap = new Dictionary<CarFlags, byte>() {
            { CarFlags.XFG, 0 },
            { CarFlags.XRG, 1 },
            { CarFlags.XRT, 2 },
            { CarFlags.RB4, 3 },
            { CarFlags.FXO, 4 },
            { CarFlags.LX4, 5 },
            { CarFlags.LX6, 6 },
            { CarFlags.MRT, 7 },
            { CarFlags.UF1, 8 },
            { CarFlags.RAC, 9 },
            { CarFlags.FZ5, 10 },
            { CarFlags.FOX, 11 },
            { CarFlags.XFR, 12 },
            { CarFlags.UFR, 13 },
            { CarFlags.FO8, 14 },
            { CarFlags.FXR, 15 },
            { CarFlags.XRR, 16 },
            { CarFlags.FZR, 17 },
            { CarFlags.BF1, 18 },
            { CarFlags.FBM, 19 },
        };

        /// <summary>
        /// Sets the handicap for the specified cars.
        /// </summary>
        /// <param name="packet">The <see cref="IS_HCP"/> packet containing the handicaps.</param>
        /// <param name="cars">The cars to set the handicap for.</param>
        /// <param name="H_Mass">The added mass to set in kilograms (0 - 200).</param>
        /// <param name="H_TRes">The intake restriction to set (0 - 50).</param>
        public static void SetHandicap(IS_HCP packet, CarFlags cars, byte H_Mass = 0, byte H_TRes = 0) {
            foreach (KeyValuePair<CarFlags, byte> map in CarMap) {
                if (cars.HasFlag(CarFlags.All) || cars.HasFlag(map.Key)) {
                    packet.Info[map.Value].H_Mass = H_Mass;
                    packet.Info[map.Value].H_TRes = H_TRes;
                }
            }
        }

        /// <summary>
        /// Creates an IS_HCP packet and sets the various car handicaps.
        /// </summary>
        /// <param name="cars">The cars to set the handicap for.</param>
        /// <param name="H_Mass">The added mass to set in kilograms (0 - 200).</param>
        /// <param name="H_TRes">The intake restriction to set (0 - 50).</param>
        /// <returns>An IS_HCP packet.</returns>
        public static IS_HCP SetHandicap(CarFlags cars, byte H_Mass = 0, byte H_TRes = 0)
        {
            var packet = new IS_HCP();
            SetHandicap(packet, cars, H_Mass, H_TRes);
            return packet;
        }
    }
}
