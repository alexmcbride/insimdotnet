using System;
using System.Collections.Generic;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// HANDICAPS
    /// </summary>
    /// <remarks>
    /// Player handicaps
    ///
    /// Set handicaps per player.  These handicaps will remain until the player spectates or rejoins 
    ///  after returning from pits or garage (an IS_NPL will be sent in that case).
    /// 
    /// </remarks>
    public class IS_PLH : IPacket, ISendable
    {
        /// <summary>
        /// Maximum number of players that you can set handicap to.
        /// </summary>
        public const int PLH_MAX_PLAYERS = 48;
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
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets the number of mods in the packet.
        /// </summary>
        public byte NumP { get; private set; }

        /// <summary>
        /// Gets a collection with <see cref="PlayerHCap"/> the player handicaps information.
        /// </summary>
        public IList<PlayerHCap> HCaps { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_PLH"/> class.
        /// </summary>
        public IS_PLH()
        {
            Size = 4; //  4 + NumP * 4
            Type = PacketType.ISP_PLH;
            HCaps = new List<PlayerHCap>(PLH_MAX_PLAYERS);
        }

        /// <summary>
        /// Creates a new IS_PLH packet.
        /// </summary>
        /// <param name="hCaps">A collection of CarSkins.</param>
        public IS_PLH(IEnumerable<PlayerHCap> hCaps)
            : this()
        {
            HCaps = new List<PlayerHCap>(hCaps);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IS_PLH"/> class from a byte array.
        /// </summary>
        /// <param name="buffer">The byte array to initialize from.</param>
        public IS_PLH(byte[] buffer)
            : this()
        {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumP = reader.ReadByte();

            List<PlayerHCap> info = new List<PlayerHCap>(NumP);
            for (int i = 0; i < NumP; i++)
            {
                info.Add(new PlayerHCap(reader));
            }
            HCaps = info.ToArray();
        }

        /// <summary>
        /// Gets the packet data.
        /// </summary>
        /// <returns>An array contaning the packet data.</returns>
        public byte[] GetBuffer()
        {
            if (HCaps.Count > PLH_MAX_PLAYERS)
            {
                throw new InvalidOperationException(StringResources.IsPLHInfoErrorMessage);
            }

            NumP = (byte)HCaps.Count;
            Size = (4 + (NumP * 4));
            PacketWriter writer = new PacketWriter(Size);
            writer.WriteSize(Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write(NumP);

            foreach (var info in HCaps)
            {
                info.GetBuffer(writer);
            }
            return writer.GetBuffer();
        }
    }
}
