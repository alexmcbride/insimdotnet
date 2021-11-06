
namespace InSimDotNet.Packets {
    /// <summary>
    /// Car handicaps - there is an array of these in IS_HCP.
    /// </summary>
    public struct CarHCP {
        /// <summary>
        /// Added mass (0kg to 200kg)
        /// </summary>
        public byte H_Mass { get; set; }

        /// <summary>
        /// Intake restriction (0 to  50)
        /// </summary>
        public byte H_TRes { get; set; }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>An array containing the packet data.</returns>
        public void GetBuffer(PacketWriter writer) {
            writer.Write(H_Mass);
            writer.Write(H_TRes);
        }
    }
}
