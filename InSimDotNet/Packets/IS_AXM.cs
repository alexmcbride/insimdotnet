using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// AutoX Multiple objects - variable size.
    /// </summary>
    /// <remarks>
    /// Set the ISF_AXM_LOAD flag in the IS_ISI for info about objects when a layout is loaded.
    /// Set the ISF_AXM_EDIT flag in the IS_ISI for info about objects edited by user or InSim.
    /// </remarks>
    public class IS_AXM : IPacket, ISendable {
        private const int MAX_AXM_OBJ = 60;
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the packet request ID.
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets the number of objects in the packet. This value is filled in automatically when sending objects.
        /// </summary>
        public byte NumO { get; private set; }

        /// <summary>
        /// Gets or sets the unique ID of the connection that sent the packet.
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets the object action flags.
        /// </summary>
        public ActionFlags PMOAction { get; set; }

        /// <summary>
        /// Gets the PMOFlags of the packet.
        /// </summary>
        public PMOFlags PMOFlags { get; set; }

        /// <summary>
        /// Gets a collection of <see cref="ObjectInfo"/> sub-packets.
        /// </summary>
        public IList<ObjectInfo> Info { get; private set; }

        /// <summary>
        /// Creates a new <see cref="IS_AXM"/> object.
        /// </summary>
        public IS_AXM() {
            Size = 8;
            Type = PacketType.ISP_AXM;
            Info = new List<ObjectInfo>(MAX_AXM_OBJ);
        }

        /// <summary>
        /// Creates a new <see cref="IS_AXM"/> object.
        /// </summary>
        /// <param name="info">A collection of <see cref="ObjectInfo"/> sub-packets.</param>
        public IS_AXM(IEnumerable<ObjectInfo> info)
            : this() {
            Info = new List<ObjectInfo>(info);
        }

        /// <summary>
        /// Creates a new <see cref="IS_AXM"/> object.
        /// </summary>
        /// <param name="buffer">The packet data.</param>
        public IS_AXM(byte[] buffer) {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumO = reader.ReadByte();
            UCID = reader.ReadByte();
            PMOAction = (ActionFlags)reader.ReadByte();
            PMOFlags = (PMOFlags)reader.ReadByte();
            reader.Skip(1);

            Info = new List<ObjectInfo>(NumO);
            for (int i = 0; i < NumO; i++) {
                Info.Add(new ObjectInfo(reader));
            }
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>An array containing the packet data.</returns>
        public byte[] GetBuffer() {
            if (Info.Count > MAX_AXM_OBJ) {
                throw new InvalidOperationException("IS_AXM too many objects set");
            }

            Size = 8 + (Info.Count * 8);
            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)Info.Count);
            writer.Write(UCID);
            writer.Write((byte)PMOAction);
            writer.Write((byte)PMOFlags);
            writer.Skip(1);

            foreach (ObjectInfo info in Info) {
                info.GetBuffer(writer);
            }

            return writer.GetBuffer();
        }
    }
}
