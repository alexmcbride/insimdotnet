using System;
using InSimDotNet.Packets;

namespace InSimDotNet {
    /// <summary>
    /// Statis class for building packets.
    /// </summary>
    public static class PacketFactory {
        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        /// <param name="buffer">Buffer containing the packet data.</param>
        /// <returns>The packet type.</returns>
        public static PacketType GetPacketType(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            if (buffer.Length > 1) {
                return (PacketType)buffer[1];
            }

            return PacketType.ISP_NONE;
        }

        /// <summary>
        /// Builds a packet.
        /// </summary>
        /// <param name="buffer">Buffer containing the packet data.</param>
        /// <returns>The built packet object.</returns>
        public static IPacket BuildPacket(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            PacketType packetType = GetPacketType(buffer);
            switch (packetType) {
                case PacketType.ISP_AXI:
                    return new IS_AXI(buffer);
                case PacketType.ISP_AXO:
                    return new IS_AXO(buffer);
                case PacketType.ISP_BFN:
                    return new IS_BFN(buffer);
                case PacketType.ISP_BTT:
                    return new IS_BTT(buffer);
                case PacketType.ISP_CCH:
                    return new IS_CCH(buffer);
                case PacketType.ISP_BTC:
                    return new IS_BTC(buffer);
                case PacketType.ISP_CNL:
                    return new IS_CNL(buffer);
                case PacketType.ISP_CPP:
                    return new IS_CPP(buffer);
                case PacketType.ISP_CPR:
                    return new IS_CPR(buffer);
                case PacketType.ISP_CRS:
                    return new IS_CRS(buffer);
                case PacketType.ISP_FIN:
                    return new IS_FIN(buffer);
                case PacketType.ISP_FLG:
                    return new IS_FLG(buffer);
                case PacketType.ISP_III:
                    return new IS_III(buffer);
                case PacketType.ISP_ISM:
                    return new IS_ISM(buffer);
                case PacketType.ISP_LAP:
                    return new IS_LAP(buffer);
                case PacketType.ISP_MCI:
                    return new IS_MCI(buffer);
                case PacketType.ISP_MSO:
                    return new IS_MSO(buffer);
                case PacketType.ISP_NCN:
                    return new IS_NCN(buffer);
                case PacketType.ISP_NLP:
                    return new IS_NLP(buffer);
                case PacketType.ISP_NPL:
                    return new IS_NPL(buffer);
                case PacketType.ISP_PEN:
                    return new IS_PEN(buffer);
                case PacketType.ISP_PFL:
                    return new IS_PFL(buffer);
                case PacketType.ISP_PIT:
                    return new IS_PIT(buffer);
                case PacketType.ISP_PLA:
                    return new IS_PLA(buffer);
                case PacketType.ISP_PLL:
                    return new IS_PLL(buffer);
                case PacketType.ISP_PLP:
                    return new IS_PLP(buffer);
                case PacketType.ISP_PSF:
                    return new IS_PSF(buffer);
                case PacketType.ISP_REO:
                    return new IS_REO(buffer);
                case PacketType.ISP_RES:
                    return new IS_RES(buffer);
                case PacketType.ISP_RIP:
                    return new IS_RIP(buffer);
                case PacketType.ISP_RST:
                    return new IS_RST(buffer);
                case PacketType.ISP_SMALL:
                    return new IS_SMALL(buffer);
                case PacketType.ISP_SPX:
                    return new IS_SPX(buffer);
                case PacketType.ISP_SSH:
                    return new IS_SSH(buffer);
                case PacketType.ISP_STA:
                    return new IS_STA(buffer);
                case PacketType.ISP_TINY:
                    return new IS_TINY(buffer);
                case PacketType.ISP_TOC:
                    return new IS_TOC(buffer);
                case PacketType.ISP_VER:
                    return new IS_VER(buffer);
                case PacketType.ISP_VTN:
                    return new IS_VTN(buffer);
                case PacketType.ISP_CON:
                    return new IS_CON(buffer);
                case PacketType.ISP_OBH:
                    return new IS_OBH(buffer);
                case PacketType.ISP_HLV:
                    return new IS_HLV(buffer);
                case PacketType.ISP_AXM:
                    return new IS_AXM(buffer);
                case PacketType.ISP_ACR:
                    return new IS_ACR(buffer);
                case PacketType.ISP_NCI:
                    return new IS_NCI(buffer);
                case PacketType.ISP_UCO:
                    return new IS_UCO(buffer);
                case PacketType.ISP_SLC:
                    return new IS_SLC(buffer);
                case PacketType.ISP_CSC:
                    return new IS_CSC(buffer);
                case PacketType.ISP_CIM:
                    return new IS_CIM(buffer);
                case PacketType.IRP_ARP:
                    return new IR_ARP(buffer);
                case PacketType.IRP_HOS:
                    return new IR_HOS(buffer);
                case PacketType.IRP_ERR:
                    return new IR_ERR(buffer);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Looks up a packet type from a string.
        /// </summary>
        /// <param name="type">The type of packet as a string (e.g. "ISP_TINY")</param>
        /// <returns>The packet type.</returns>
        public static PacketType PacketLookup(string type) {
            PacketType packetType;
            if (Enum.TryParse(type, true, out packetType)) {
                return packetType;
            }

            return PacketType.ISP_NONE;
        }

        /// <summary>
        /// Looks up the packet type for the specified object type.
        /// </summary>
        /// <param name="type">The type to lookup.</param>
        /// <returns>The packet type.</returns>
        public static PacketType PacketLookup(Type type) {
            if (type == typeof(IS_ISI))
                return PacketType.ISP_ISI;
            if (type == typeof(IS_VER))
                return PacketType.ISP_VER;
            if (type == typeof(IS_TINY))
                return PacketType.ISP_TINY;
            if (type == typeof(IS_SMALL))
                return PacketType.ISP_SMALL;
            if (type == typeof(IS_STA))
                return PacketType.ISP_STA;
            if (type == typeof(IS_SCH))
                return PacketType.ISP_SCH;
            if (type == typeof(IS_SFP))
                return PacketType.ISP_SFP;
            if (type == typeof(IS_SCC))
                return PacketType.ISP_SCC;
            if (type == typeof(IS_CPP))
                return PacketType.ISP_CPP;
            if (type == typeof(IS_ISM))
                return PacketType.ISP_ISM;
            if (type == typeof(IS_MSO))
                return PacketType.ISP_MSO;
            if (type == typeof(IS_III))
                return PacketType.ISP_III;
            if (type == typeof(IS_MST))
                return PacketType.ISP_MST;
            if (type == typeof(IS_MTC))
                return PacketType.ISP_MTC;
            if (type == typeof(IS_MOD))
                return PacketType.ISP_MOD;
            if (type == typeof(IS_VTN))
                return PacketType.ISP_VTN;
            if (type == typeof(IS_RST))
                return PacketType.ISP_RST;
            if (type == typeof(IS_NCN))
                return PacketType.ISP_NCN;
            if (type == typeof(IS_CNL))
                return PacketType.ISP_CNL;
            if (type == typeof(IS_CPR))
                return PacketType.ISP_CPR;
            if (type == typeof(IS_NPL))
                return PacketType.ISP_NPL;
            if (type == typeof(IS_PLP))
                return PacketType.ISP_PLP;
            if (type == typeof(IS_PLL))
                return PacketType.ISP_PLL;
            if (type == typeof(IS_LAP))
                return PacketType.ISP_LAP;
            if (type == typeof(IS_SPX))
                return PacketType.ISP_SPX;
            if (type == typeof(IS_PIT))
                return PacketType.ISP_PIT;
            if (type == typeof(IS_PSF))
                return PacketType.ISP_PSF;
            if (type == typeof(IS_PLA))
                return PacketType.ISP_PLA;
            if (type == typeof(IS_CCH))
                return PacketType.ISP_CCH;
            if (type == typeof(IS_PEN))
                return PacketType.ISP_PEN;
            if (type == typeof(IS_TOC))
                return PacketType.ISP_TOC;
            if (type == typeof(IS_FLG))
                return PacketType.ISP_FLG;
            if (type == typeof(IS_PFL))
                return PacketType.ISP_PFL;
            if (type == typeof(IS_FIN))
                return PacketType.ISP_FIN;
            if (type == typeof(IS_RES))
                return PacketType.ISP_RES;
            if (type == typeof(IS_REO))
                return PacketType.ISP_REO;
            if (type == typeof(IS_NLP))
                return PacketType.ISP_NLP;
            if (type == typeof(IS_MCI))
                return PacketType.ISP_MCI;
            if (type == typeof(IS_MSX))
                return PacketType.ISP_MSX;
            if (type == typeof(IS_MSL))
                return PacketType.ISP_MSL;
            if (type == typeof(IS_CRS))
                return PacketType.ISP_CRS;
            if (type == typeof(IS_BFN))
                return PacketType.ISP_BFN;
            if (type == typeof(IS_AXI))
                return PacketType.ISP_AXI;
            if (type == typeof(IS_AXO))
                return PacketType.ISP_AXO;
            if (type == typeof(IS_BTN))
                return PacketType.ISP_BTN;
            if (type == typeof(IS_BTC))
                return PacketType.ISP_BTC;
            if (type == typeof(IS_BTT))
                return PacketType.ISP_BTT;
            if (type == typeof(IS_RIP))
                return PacketType.ISP_RIP;
            if (type == typeof(IS_SSH))
                return PacketType.ISP_SSH;
            if (type == typeof(IS_CON))
                return PacketType.ISP_CON;
            if (type == typeof(IS_OBH))
                return PacketType.ISP_OBH;
            if (type == typeof(IS_HLV))
                return PacketType.ISP_HLV;
            if (type == typeof(IS_PLC))
                return PacketType.ISP_PLC;
            if (type == typeof(IS_AXM))
                return PacketType.ISP_AXM;
            if (type == typeof(IS_ACR))
                return PacketType.ISP_ACR;
            if (type == typeof(IS_NCI))
                return PacketType.ISP_NCI;
            if (type == typeof(IS_JRR))
                return PacketType.ISP_JRR;
            if (type == typeof(IS_UCO))
                return PacketType.ISP_UCO;
            if (type == typeof(IS_OCO))
                return PacketType.ISP_OCO;
            if (type == typeof(IS_TTC))
                return PacketType.ISP_TTC;
            if (type == typeof(IS_SLC))
                return PacketType.ISP_SLC;
            if (type == typeof(IS_CSC))
                return PacketType.ISP_CSC;
            if (type == typeof(IS_CIM))
                return PacketType.ISP_CIM;
            if (type == typeof(IR_ARP))
                return PacketType.IRP_ARP;
            if (type == typeof(IR_ARQ))
                return PacketType.IRP_ARQ;
            if (type == typeof(IR_ERR))
                return PacketType.IRP_ERR;
            if (type == typeof(IR_HLR))
                return PacketType.IRP_HLR;
            if (type == typeof(IR_HOS))
                return PacketType.IRP_HOS;
            if (type == typeof(IR_SEL))
                return PacketType.IRP_SEL;
            return PacketType.ISP_NONE;
        }
    }
}
