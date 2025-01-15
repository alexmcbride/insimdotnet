using System;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Player handicaps - there is an array of these in IS_PLH.
    /// </summary>
    public class PlayerHCap
    {
        /// <summary>
        /// Player's unique ID
        /// </summary>
        public byte PLID { get; set; }

        /// <summary>
        /// Handicap flags
        /// </summary>
        public PlayerHCapFlag Flags { get; set; }

        /// <summary>
        /// Added mass (0kg to 200kg)
        /// </summary>
        public byte H_Mass { get; set; }

        /// <summary>
        /// Intake restriction (0 to  50)
        /// </summary>
        public byte H_TRes { get; set; }

        /// <summary>
        /// Creates a new <see cref="PlayerHCap"/> object.
        /// </summary>
        public PlayerHCap(){}

        /// <summary>
        /// Creates a new PlayerHCap object.
        /// </summary>
        /// <param name="reader">A <see cref="PacketReader"/> containing packet data</param>
        public PlayerHCap(PacketReader reader)
        {
            if(reader == null)
                throw new ArgumentNullException("reader");

            PLID = reader.ReadByte();
            Flags = (PlayerHCapFlag)reader.ReadByte();
            H_Mass = reader.ReadByte();
            H_TRes = reader.ReadByte();
        }

        /// <summary>
        /// Writes the <see cref="PlayerHCap"/> object to the specified <see cref="PacketWriter"/>.
        /// </summary>
        /// <param name="writer">The <see cref="PacketWriter"/> to write the data to.</param>
        public void GetBuffer(PacketWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(PLID);
            writer.Write((byte)Flags);
            writer.Write(H_Mass);
            writer.Write(H_TRes);
        }
    }
}
