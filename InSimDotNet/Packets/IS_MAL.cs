using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Allowed Mods - Set/Clear allowed mods (by skinID) in the server
    /// </summary>
    /// <remarks>
    ///  You can set a list of up to 120 mods that are allowed to be used on a host
    ///  Send zero to clear the list and allow all mods to be used
    /// </remarks>
    public class IS_MAL : IPacket, ISendable
    {
        public static int MAX_MAL_MODS = 120;
        /// <summary>
        /// Gets the packet size.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the packet type.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        /// <remarks>
        ///  0 unless this is a reply to a TINY_MAL request
        /// </remarks>
        public byte ReqI { get; set; }

        /// <summary>
        /// Number of mods in this packet [0-120]. This value is filled in automatically when sending objects.
        /// </summary>
        public byte NumM { get; private set; }

        /// <summary>
        /// Unique id of the connection that updated the list
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Zero (for now)
        /// </summary>
        public byte Flags { get; set; }

        /// <summary>
        /// Gets a collection of <see cref="NodeLap"/> packets.
        /// </summary>
        public IList<SkinID> SkinIDs { get; private set; }

        /// <summary>
        /// Creates a new <see cref="IS_MAL"/> object.
        /// </summary>
        public IS_MAL()
        {
            Size = 8;
            Type = PacketType.ISP_MAL;
            SkinIDs = new List<SkinID>(MAX_MAL_MODS);
        }

        /// <summary>
        /// Creates a new <see cref="IS_MAL"/> object.
        /// </summary>
        /// <param name="info">A collection of <see cref="ObjectInfo"/> sub-packets.</param>
        public IS_MAL(IEnumerable<SkinID> skinIDs)
            : this()
        {
            SkinIDs = new List<SkinID>(skinIDs);
        }

        /// <summary>
        /// Creates a new <see cref="IS_MAL"/> object.
        /// </summary>
        /// <param name="info">A collection of <see cref="ObjectInfo"/> sub-packets.</param>
        public IS_MAL(IEnumerable<string> skinIDs)
            : this()
        {
            SkinIDs = skinIDs.Select(x => new SkinID(x)).ToList();
        }

        /// <summary>
        /// Creates a new <see cref="IS_MAL"/> object.
        /// </summary>
        /// <param name="buffer">The packet data.</param>
        public IS_MAL(byte[] buffer)
        {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumM = reader.ReadByte();
            UCID = reader.ReadByte();
            Flags = reader.ReadByte();
            reader.Skip(1);
            reader.Skip(1);

            SkinIDs = new List<SkinID>(NumM);
            for (int i = 0; i < NumM; i++)
            {
                SkinIDs.Add(new SkinID(reader));
            }
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>An array containing the packet data.</returns>
        public byte[] GetBuffer()
        {
            if (SkinIDs.Count > MAX_MAL_MODS)
            {
                throw new InvalidOperationException("IS_MAL too many objects set");
            }

            Size = (8 + (SkinIDs.Count * 4));
            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write((byte)SkinIDs.Count);
            writer.Write(UCID);
            writer.Write(Flags);
            writer.Skip(1);
            writer.Skip(1);

            foreach (var skinID in SkinIDs)
            {
                writer.Write(skinID.CompressedForm);
            }

            return writer.GetBuffer();
        }
    }
}
