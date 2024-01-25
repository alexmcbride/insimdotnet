using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Class for the <see cref="IS_PLH"/> Car handicaps Info Collection
    /// </summary>
    public struct PlayerHCap {
        /// <summary>
        /// 
        /// </summary>
        public byte PLID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PLHFlags Flags { get; set; }

        /// <summary>
        /// Added mass restriction (0 to  200)
        /// </summary>
        public byte H_Mass { get; set; }
        
        /// <summary>
        /// Intake restriction (0 to  50)
        /// </summary>
        public byte H_TRes { get; set; }

        public PlayerHCap(PacketReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            PLID = reader.ReadByte();
            Flags = (PLHFlags)reader.ReadByte();
            H_Mass = reader.ReadByte();
            H_TRes = reader.ReadByte();
        }

        /// <summary>
        /// Writes the <see cref="PlayerHCap"/> object to the specified <see cref="PacketWriter"/>
        /// </summary>
        /// <param name="writer">The <see cref="PacketWriter"/> to write the data to.</param>
        public void GetBuffer(PacketWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.Write(PLID);
            writer.Write((byte)Flags);
            writer.Write(H_Mass);
            writer.Write(H_TRes);
        }
    }
}
