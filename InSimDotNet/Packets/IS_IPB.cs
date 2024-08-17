using System;
using System.Collections.Generic;
using System.Net;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// IP Ban list information
    /// </summary>
    /// <remarks>
    /// IP Bans
    ///
    /// You can set up to 120 IP addresses that are not allowed to join a host
    /// </remarks>
    public class IS_IPB : IPacket, ISendable
    {
        /// <summary>
        /// Maximum number of bans allowed in the packet
        /// </summary>
        public const int IPB_MAX_BANS = 120;
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets the request ID - 0 unless this is a reply to a TINY_IPB request
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets the number of bans in the packet.
        /// </summary>
        public byte NumB { get; private set; }

        /// <summary>
        /// No description provided by lfs.
        /// </summary>
        public byte Sp0 { get; private set; }
        /// <summary>
        /// No description provided by lfs.
        /// </summary>
        public byte Sp1 { get; private set; }
        /// <summary>
        /// No description provided by lfs.
        /// </summary>
        public byte Sp2 { get; private set; }
        /// <summary>
        /// No description provided by lfs.
        /// </summary>
        public byte Sp3 { get; private set; }

        /// <summary>
        /// Gets a collection with the carSkins information as string.
        /// </summary>
        public IList<IPAddress> BanIps { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_IPB"/> class.
        /// </summary>
        public IS_IPB()
        {
            Size = 8;
            Type = PacketType.ISP_IPB;
            BanIps = new List<IPAddress>(IPB_MAX_BANS);
        }

        /// <summary>
        /// Creates a new IS_IPB packet.
        /// </summary>
        /// <param name="banIps">A collection of CarSkins.</param>
        public IS_IPB(IEnumerable<IPAddress> banIps)
            : this()
        {
            BanIps = new List<IPAddress>(banIps);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_IPB"/> class from a byte array.
        /// </summary>
        /// <param name="buffer">The byte array to initialize from.</param>
        public IS_IPB(byte[] buffer)
            : this()
        {
            var reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumB = reader.ReadByte();
            Sp0 = reader.ReadByte();
            Sp1 = reader.ReadByte();
            Sp2 = reader.ReadByte();
            Sp3 = reader.ReadByte();

            var info = new List<IPAddress>(NumB);
            for (var i = 0; i < NumB; i++)
            {
                info.Add(new IPAddress(reader.ReadUInt32()));
            }
            BanIps = info;
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>An array contaning the packet data.</returns>
        public byte[] GetBuffer()
        {
            if (BanIps.Count > IPB_MAX_BANS)
            {
                throw new InvalidOperationException(StringResources.IsIPBInfoErrorMessage);
            }

            NumB = (byte)BanIps.Count;
            Size = (8 + (NumB * 4));
            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write(NumB);
            writer.Skip(1);
            writer.Skip(1);
            writer.Skip(1);
            writer.Skip(1);

            foreach (var address in BanIps)
            {
                byte[] bytes = address.GetAddressBytes();
                writer.Write(BitConverter.ToUInt32(bytes, 0));
            }
            return writer.GetBuffer();
        }
    }
}
