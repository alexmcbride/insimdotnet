using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Reorder packet.
    /// </summary>
    /// <remarks>
    /// Sent when race starts or is restarted. Used to reorder grid.
    /// </remarks>
    public class IS_REO : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the packet request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets the number of players in the race. This gets filled in automatically 
        /// when the packet is sent.
        /// </summary>
        public byte NumP { get; private set; }

        /// <summary>
        /// Gets a collection player IDs in order, used to set the grid.
        /// </summary>
        public IList<byte> PLID { get; private set; }

        /// <summary>
        /// Creates a new reorder packet.
        /// </summary>
        public IS_REO() {
            Size = 36;
            Type = PacketType.ISP_REO;
            PLID = new List<byte>(32);
        }

        /// <summary>
        /// Creates a new reorder packet.
        /// </summary>
        /// <param name="plid">A collection of PLID bytes.</param>
        public IS_REO(IEnumerable<byte> plid)
            : this() {
            PLID = new List<byte>(plid);
        }

        /// <summary>
        /// Creates a new reorder packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_REO(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumP = reader.ReadByte();
            PLID = new List<byte>(reader.ReadBytes(NumP));
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            if (PLID.Count > 32) {
                throw new InvalidOperationException(StringResources.IsReoPlidErrorMessage);
            }

            PacketWriter writer = new PacketWriter(Size);
            writer.Write((byte)Size);
            writer.Write((byte)Type);
            writer.Write((byte)ReqI);
            writer.Write((byte)PLID.Count);
            writer.Write(PLID.ToArray());
            return writer.GetBuffer();
        }
    }
}
