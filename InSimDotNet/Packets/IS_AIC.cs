using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InSimDotNet.Packets {
    /// <summary>
    /// AI Control - variable size
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class IS_AIC : IPacket, ISendable
    {
        private const int AIC_MAX_INPUTS = 20; // NOTE: Increase if CS_NUM is increased

        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet. ISP_AIC
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Optional - returned in any immediate response e.g. reply to CS_SEND_AI_INFO
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Unique ID of AI driver to control
        /// </summary>
        public byte PLID { get; set; }

        /// <summary>
        /// Gets a collection of up to sixteen <see cref="AIInputVal"/> packets.
        /// </summary>
        /// <remarks>
        /// If there are more than sixteen cars in the race then more than one 
        /// <see cref="IS_MCI"/> packet is sent.
        /// </remarks>
        public IList<AIInputVal> Inputs { get; private set; }

        /// <summary>
        /// Creates a new ai control packet.
        /// </summary>
        public IS_AIC() {
            Size = 4; // 4 + 4 * (number of inputs)
            Type = PacketType.ISP_AIC;
            Inputs = new List<AIInputVal>(AIC_MAX_INPUTS);
        }

        /// <summary>
        /// Creates a new ai control packet.
        /// </summary>
        /// <param name="inputs"></param>
        public IS_AIC(IList<AIInputVal> inputs)
            : this()
        {
            Inputs = new List<AIInputVal>(inputs);
            Size = 4 + (4 * inputs.Count);
        }

        /// <summary>
        /// Creates a new multi car information packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_AIC(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();

            var info = new List<AIInputVal>();
            Inputs = new ReadOnlyCollection<AIInputVal>(info);
        }


        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public byte[] GetBuffer()
        {
            if (Inputs.Count > AIC_MAX_INPUTS)
            {
                throw new InvalidOperationException(string.Format(StringResources.IsAicPlidErrorMessage, AIC_MAX_INPUTS));
            }

            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write(PLID);

            foreach (AIInputVal info in Inputs)
            {
                info.GetBuffer(writer);
            }

            return writer.GetBuffer();
        }
    }
}
