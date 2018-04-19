using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InSimDotNet.Packets {
    /// <summary>
    /// Multi car information packet.
    /// </summary>
    /// <remarks>
    /// Contains information about cars currently on-track. To enable these 
    /// packets see the Flags and Interval properties when initializing InSim.
    /// </remarks>
    public class IS_MCI : IPacket {
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
        /// Gets the number of cars in the packet.
        /// </summary>
        public byte NumC { get; private set; }

        /// <summary>
        /// Gets a collection of up to eight <see cref="CompCar"/> packets.
        /// </summary>
        /// <remarks>
        /// If there are more than eight cars in the race then more than one 
        /// <see cref="IS_MCI"/> packet is sent.
        /// </remarks>
        public ReadOnlyCollection<CompCar> Info { get; private set; }

        /// <summary>
        /// Creates a new multi car information packet.
        /// </summary>
        public IS_MCI() {
            Size = 28;
            Type = PacketType.ISP_MCI;
        }

        /// <summary>
        /// Creates a new multi car information packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_MCI(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            NumC = reader.ReadByte();

            List<CompCar> info = new List<CompCar>(NumC);
            for (int i = 0; i < NumC; i++) {
                info.Add(new CompCar(reader));
            }
            Info = new ReadOnlyCollection<CompCar>(info);
        }
    }
}
