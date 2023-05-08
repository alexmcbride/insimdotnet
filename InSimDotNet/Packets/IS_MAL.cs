using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Multi car information packet.
    /// </summary>
    /// <remarks>
    /// Contains information about cars currently on-track. To enable these 
    /// packets see the Flags and Interval properties when initializing InSim.
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
        /// Gets the number of cars in the packet.
        /// </summary>
        public byte NumM { get; private set; }

        public byte UCID { get; }

        public IS_MAL()
        {
            Size = 28;
            Type = PacketType.ISP_MAL;
        }

        /// <summary>
        /// Creates a new multi car information packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_MAL(byte[] buffer)
            : this()
        {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadSize();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumM = reader.ReadByte();
            UCID = reader.ReadByte();
        }
    }
}
