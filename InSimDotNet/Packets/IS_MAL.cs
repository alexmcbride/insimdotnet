using System;
using System.Collections.ObjectModel;
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
    public class IS_MAL : IPacket
    {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        public byte ReqI { get; private set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_MAL"/> class.
        /// </summary>
        public IS_MAL()
        {
            Size = 28;
            Type = PacketType.ISP_MAL;
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
        }
    }
}
