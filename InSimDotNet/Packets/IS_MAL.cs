using System;
using System.Collections.Generic;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Mod list information
    /// </summary>
    /// <remarks>
    /// Allowed Mods
    ///
    /// You can set a list of up to 120 mods that are allowed to be used on a host
    /// Send zero to clear the list and allow all mods to be used
    /// </remarks>
    public class IS_MAL : IPacket, ISendable
    {
        public const int MAX_MAL_MODS = 120;
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID - 0 unless this is a reply to a TINY_MAL request
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets the number of mods in the packet.
        /// </summary>
        public byte NumM { get; private set; }

        /// <summary>
        /// Gets the unique ID of the connnection that updated the list
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// No description provided by lfs.
        /// </summary>
        public byte Flags { get; private set; }
        /// <summary>
        /// No description provided by lfs.
        /// </summary>
        public byte Sp2 { get; private set; }
        /// <summary>
        /// No description provided by lfs.
        /// </summary>
        public byte Sp3 { get; private set; }

        public IList<string> SkinIDs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_MAL"/> class.
        /// </summary>
        public IS_MAL()
        {
            Size = 8;
            Type = PacketType.ISP_MAL;
            SkinIDs = new List<string>(MAX_MAL_MODS);
        }

        /// <summary>
        /// Creates a new IS_MAL packet.
        /// </summary>
        /// <param name="plid">A collection of CarSkins.</param>
        public IS_MAL(IEnumerable<string> plid)
            : this()
        {
            SkinIDs = new List<string>(plid);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_MAL"/> class from a byte array.
        /// </summary>
        /// <param name="buffer">The byte array to initialize from.</param>
        public IS_MAL(byte[] buffer)
            : this()
        {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumM = reader.ReadByte();
            UCID = reader.ReadByte();
            Flags = reader.ReadByte();
            Sp2 = reader.ReadByte();
            Sp3 = reader.ReadByte();

            var info = new List<string>(NumM);
            for (int i = 0; i < NumM; i++)
            {
                info.Add(reader.ReadCNameString());
            }
            SkinIDs = info;
        }

        public byte[] GetBuffer()
        {
            if (SkinIDs.Count > MAX_MAL_MODS)
            {
                throw new InvalidOperationException(StringResources.IsMalInfoErrorMessage);
            }

            NumM = (byte)SkinIDs.Count;
            Size = (8 + (NumM * 4));
            Flags = 0; // zero (for now)
            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write(NumM);
            writer.Write(UCID);
            writer.Write(Flags);
            writer.Skip(1);
            writer.Skip(1);

            foreach (var info in SkinIDs)
            {
                writer.Write(Convert.ToUInt32(info, 16));
            }
            return writer.GetBuffer();
        }
    }
}
