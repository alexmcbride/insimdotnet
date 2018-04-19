using System;

namespace InSimDotNet.Packets {
    /// <summary>
    /// New connection packet.
    /// </summary>
    /// <remarks>
    /// Sent when a connection joins a host.
    /// </remarks>
    public class IS_NCN : IPacket {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID.
        /// </summary>
        public byte ReqI { get; private set; }

        /// <summary>
        /// Gets the unique ID of the connection.
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets the LFS username of the connection.
        /// </summary>
        public string UName { get; set; }

        /// <summary>
        /// Gets the current player name of the connection.
        /// </summary>
        public string PName { get; private set; }

        /// <summary>
        /// Gets if the connection if an admin.
        /// </summary>
        public bool Admin { get; private set; }

        /// <summary>
        /// Gets the total number of connections on the host.
        /// </summary>
        public byte Total { get; private set; }

        ///// <summary>
        ///// Gets the connection flags (bit 2 : remote).
        ///// </summary>
        //public byte Flags { get; private set; }

        /// <summary>
        /// Gets if the connection is remote or local.
        /// </summary>
        public bool Remote { get; private set; }

        /// <summary>
        /// Creates a new new connection packet.
        /// </summary>
        public IS_NCN() {
            Size = 56;
            Type = PacketType.ISP_NCN;
            UName = String.Empty;
            PName = String.Empty;
        }

        /// <summary>
        /// Creates a new new connection packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_NCN(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            UCID = reader.ReadByte();
            UName = reader.ReadString(24);
            PName = reader.ReadString(24);
            Admin = reader.ReadBoolean();
            Total = reader.ReadByte();
            Remote = (reader.ReadByte() & 4) > 0; // bit 2: remote
        }
    }
}
