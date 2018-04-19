using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InSimDotNet.Packets {
    /// <summary>
    /// New player packet.
    /// </summary>
    /// <remarks>
    /// Sent when a player joins the race (or leaving pits if PLID already exists).
    /// </remarks>
    public class IS_NPL : IPacket {
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
        /// Gets the unique ID of the player.
        /// </summary>
        public byte PLID { get; private set; }

        /// <summary>
        /// Gets the unique ID of the connection.
        /// </summary>
        public byte UCID { get; private set; }

        /// <summary>
        /// Gets the player type.
        /// </summary>
        public PlayerTypes PType { get; private set; }

        /// <summary>
        /// Gets the player flags.
        /// </summary>
        public PlayerFlags Flags { get; private set; }

        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string PName { get; private set; }

        /// <summary>
        /// Gets the number plate of the player.
        /// </summary>
        public string Plate { get; private set; }

        /// <summary>
        /// Gets the car name of the player.
        /// </summary>
        public string CName { get; private set; }

        /// <summary>
        /// Gets the skin name of the players car.
        /// </summary>
        public string SName { get; private set; }

        /// <summary>
        /// Gets the tyre compounds on the players car.
        /// </summary>
        public Tyres Tyres { get; private set; }

        /// <summary>
        /// Gets the added mass of the car.
        /// </summary>
        public byte H_Mass { get; private set; }

        /// <summary>
        /// Gets the intake restriction of the car.
        /// </summary>
        public byte H_TRes { get; private set; }

        /// <summary>
        /// Gets the driver model.
        /// </summary>
        public byte Model { get; private set; }

        /// <summary>
        /// Gets the passenger flags.
        /// </summary>
        public PassengerFlags Pass { get; private set; }

        /// <summary>
        /// Gets the setup flags.
        /// </summary>
        public SetupFlags SetF { get; private set; }

        /// <summary>
        /// Gets the number of player in the race. ZERO if this is a join request.
        /// </summary>
        public byte NumP { get; private set; }

        /// <summary>
        /// Creates a new new player packet.
        /// </summary>
        public IS_NPL() {
            Size = 76;
            Type = PacketType.ISP_NPL;
            PName = String.Empty;
            Plate = String.Empty;
            CName = String.Empty;
            SName = String.Empty;
        }

        /// <summary>
        /// Creates a new new player packet.
        /// </summary>
        /// <param name="buffer">A buffer contaning the packet data.</param>
        public IS_NPL(byte[] buffer)
            : this() {
            PacketReader reader = new PacketReader(buffer);
            Size = reader.ReadByte();
            Type = (PacketType)reader.ReadByte();
            ReqI = reader.ReadByte();
            PLID = reader.ReadByte();
            UCID = reader.ReadByte();
            PType = (PlayerTypes)reader.ReadByte();
            Flags = (PlayerFlags)reader.ReadUInt16();
            PName = reader.ReadString(24);
            Plate = reader.ReadString(8);
            CName = reader.ReadString(4);
            SName = reader.ReadString(16);
            Tyres = new Tyres(
                (TyreCompound)reader.ReadByte(),
                (TyreCompound)reader.ReadByte(),
                (TyreCompound)reader.ReadByte(),
                (TyreCompound)reader.ReadByte());
            H_Mass = reader.ReadByte();
            H_TRes = reader.ReadByte();
            Model = reader.ReadByte();
            Pass = (PassengerFlags)reader.ReadByte();
            reader.Skip(1);
            SetF = (SetupFlags)reader.ReadByte();
            NumP = reader.ReadByte();
        }
    }
}
