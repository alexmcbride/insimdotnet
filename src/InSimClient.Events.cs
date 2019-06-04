using InSimDotNet.Packets;
using System;

namespace InSimDotNet
{
    /// <summary>
    /// Manages connecting to LFS using the InSim protocol.
    /// </summary>
    public partial class InSimClient : IDisposable
    {
        public event EventHandler<PacketEventArgs<IS_VER>> IS_VER;
        public event EventHandler<PacketEventArgs<IS_TINY>> IS_TINY;
        public event EventHandler<PacketEventArgs<IS_SMALL>> IS_SMALL;
        public event EventHandler<PacketEventArgs<IS_STA>> IS_STA;
        public event EventHandler<PacketEventArgs<IS_CPP>> IS_CPP;
        public event EventHandler<PacketEventArgs<IS_ISM>> IS_ISM;
        public event EventHandler<PacketEventArgs<IS_MSO>> IS_MSO;
        public event EventHandler<PacketEventArgs<IS_III>> IS_III;
        public event EventHandler<PacketEventArgs<IS_VTN>> IS_VTN;
        public event EventHandler<PacketEventArgs<IS_RST>> IS_RST;
        public event EventHandler<PacketEventArgs<IS_NCN>> IS_NCN;
        public event EventHandler<PacketEventArgs<IS_CNL>> IS_CNL;
        public event EventHandler<PacketEventArgs<IS_CPR>> IS_CPR;
        public event EventHandler<PacketEventArgs<IS_NPL>> IS_NPL;
        public event EventHandler<PacketEventArgs<IS_PLP>> IS_PLP;
        public event EventHandler<PacketEventArgs<IS_PLL>> IS_PLL;
        public event EventHandler<PacketEventArgs<IS_LAP>> IS_LAP;
        public event EventHandler<PacketEventArgs<IS_SPX>> IS_SPX;
        public event EventHandler<PacketEventArgs<IS_PIT>> IS_PIT;
        public event EventHandler<PacketEventArgs<IS_PSF>> IS_PSF;
        public event EventHandler<PacketEventArgs<IS_PLA>> IS_PLA;
        public event EventHandler<PacketEventArgs<IS_CCH>> IS_CCH;
        public event EventHandler<PacketEventArgs<IS_PEN>> IS_PEN;
        public event EventHandler<PacketEventArgs<IS_TOC>> IS_TOC;
        public event EventHandler<PacketEventArgs<IS_FLG>> IS_FLG;
        public event EventHandler<PacketEventArgs<IS_PFL>> IS_PFL;
        public event EventHandler<PacketEventArgs<IS_FIN>> IS_FIN;
        public event EventHandler<PacketEventArgs<IS_RES>> IS_RES;
        public event EventHandler<PacketEventArgs<IS_REO>> IS_REO;
        public event EventHandler<PacketEventArgs<IS_NLP>> IS_NLP;
        public event EventHandler<PacketEventArgs<IS_MCI>> IS_MCI;
        public event EventHandler<PacketEventArgs<IS_CRS>> IS_CRS;
        public event EventHandler<PacketEventArgs<IS_BFN>> IS_BFN;
        public event EventHandler<PacketEventArgs<IS_AXI>> IS_AXI;
        public event EventHandler<PacketEventArgs<IS_AXO>> IS_AXO;
        public event EventHandler<PacketEventArgs<IS_BTC>> IS_BTC;
        public event EventHandler<PacketEventArgs<IS_BTT>> IS_BTT;
        public event EventHandler<PacketEventArgs<IS_RIP>> IS_RIP;
        public event EventHandler<PacketEventArgs<IS_SSH>> IS_SSH;
        public event EventHandler<PacketEventArgs<IS_CON>> IS_CON;
        public event EventHandler<PacketEventArgs<IS_OBH>> IS_OBH;
        public event EventHandler<PacketEventArgs<IS_HLV>> IS_HLV;
        public event EventHandler<PacketEventArgs<IS_AXM>> IS_AXM;
        public event EventHandler<PacketEventArgs<IS_ACR>> IS_ACR;
        public event EventHandler<PacketEventArgs<IS_NCI>> IS_NCI;
        public event EventHandler<PacketEventArgs<IS_UCO>> IS_UCO;
        public event EventHandler<PacketEventArgs<IS_SLC>> IS_SLC;
        public event EventHandler<PacketEventArgs<IS_CSC>> IS_CSC;
        public event EventHandler<PacketEventArgs<IS_CIM>> IS_CIM;

        private static PacketType GetPacketType(byte[] buffer)
        {
            if (buffer.Length >= 4)
            {
                return (PacketType)buffer[1];
            }
            else
            {
                return PacketType.ISP_NONE;
            }
        }

        private void HandlePacketEvent(byte[] packet)
        {
            var packetType = GetPacketType(packet);
            switch(packetType)
            {
                case PacketType.ISP_VER:
                    OnIS_VER(new PacketEventArgs<IS_VER>(new IS_VER(packet)));
                    break;
                case PacketType.ISP_TINY:
                    OnIS_TINY(new PacketEventArgs<IS_TINY>(new IS_TINY(packet)));
                    break;
                case PacketType.ISP_SMALL:
                    OnIS_SMALL(new PacketEventArgs<IS_SMALL>(new IS_SMALL(packet)));
                    break;
                case PacketType.ISP_STA:
                    OnIS_STA(new PacketEventArgs<IS_STA>(new IS_STA(packet)));
                    break;
                case PacketType.ISP_CPP:
                    OnIS_CPP(new PacketEventArgs<IS_CPP>(new IS_CPP(packet)));
                    break;
                case PacketType.ISP_ISM:
                    OnIS_ISM(new PacketEventArgs<IS_ISM>(new IS_ISM(packet)));
                    break;
                case PacketType.ISP_MSO:
                    OnIS_MSO(new PacketEventArgs<IS_MSO>(new IS_MSO(packet)));
                    break;
                case PacketType.ISP_III:
                    OnIS_III(new PacketEventArgs<IS_III>(new IS_III(packet)));
                    break;
                case PacketType.ISP_VTN:
                    OnIS_VTN(new PacketEventArgs<IS_VTN>(new IS_VTN(packet)));
                    break;
                case PacketType.ISP_RST:
                    OnIS_RST(new PacketEventArgs<IS_RST>(new IS_RST(packet)));
                    break;
                case PacketType.ISP_NCN:
                    OnIS_NCN(new PacketEventArgs<IS_NCN>(new IS_NCN(packet)));
                    break;
                case PacketType.ISP_CNL:
                    OnIS_CNL(new PacketEventArgs<IS_CNL>(new IS_CNL(packet)));
                    break;
                case PacketType.ISP_CPR:
                    OnIS_CPR(new PacketEventArgs<IS_CPR>(new IS_CPR(packet)));
                    break;
                case PacketType.ISP_NPL:
                    OnIS_NPL(new PacketEventArgs<IS_NPL>(new IS_NPL(packet)));
                    break;
                case PacketType.ISP_PLP:
                    OnIS_PLP(new PacketEventArgs<IS_PLP>(new IS_PLP(packet)));
                    break;
                case PacketType.ISP_PLL:
                    OnIS_PLL(new PacketEventArgs<IS_PLL>(new IS_PLL(packet)));
                    break;
                case PacketType.ISP_LAP:
                    OnIS_LAP(new PacketEventArgs<IS_LAP>(new IS_LAP(packet)));
                    break;
                case PacketType.ISP_SPX:
                    OnIS_SPX(new PacketEventArgs<IS_SPX>(new IS_SPX(packet)));
                    break;
                case PacketType.ISP_PIT:
                    OnIS_PIT(new PacketEventArgs<IS_PIT>(new IS_PIT(packet)));
                    break;
                case PacketType.ISP_PSF:
                    OnIS_PSF(new PacketEventArgs<IS_PSF>(new IS_PSF(packet)));
                    break;
                case PacketType.ISP_PLA:
                    OnIS_PLA(new PacketEventArgs<IS_PLA>(new IS_PLA(packet)));
                    break;
                case PacketType.ISP_CCH:
                    OnIS_CCH(new PacketEventArgs<IS_CCH>(new IS_CCH(packet)));
                    break;
                case PacketType.ISP_PEN:
                    OnIS_PEN(new PacketEventArgs<IS_PEN>(new IS_PEN(packet)));
                    break;
                case PacketType.ISP_TOC:
                    OnIS_TOC(new PacketEventArgs<IS_TOC>(new IS_TOC(packet)));
                    break;
                case PacketType.ISP_FLG:
                    OnIS_FLG(new PacketEventArgs<IS_FLG>(new IS_FLG(packet)));
                    break;
                case PacketType.ISP_PFL:
                    OnIS_PFL(new PacketEventArgs<IS_PFL>(new IS_PFL(packet)));
                    break;
                case PacketType.ISP_FIN:
                    OnIS_FIN(new PacketEventArgs<IS_FIN>(new IS_FIN(packet)));
                    break;
                case PacketType.ISP_RES:
                    OnIS_RES(new PacketEventArgs<IS_RES>(new IS_RES(packet)));
                    break;
                case PacketType.ISP_REO:
                    OnIS_REO(new PacketEventArgs<IS_REO>(new IS_REO(packet)));
                    break;
                case PacketType.ISP_NLP:
                    OnIS_NLP(new PacketEventArgs<IS_NLP>(new IS_NLP(packet)));
                    break;
                case PacketType.ISP_MCI:
                    OnIS_MCI(new PacketEventArgs<IS_MCI>(new IS_MCI(packet)));
                    break;
                case PacketType.ISP_CRS:
                    OnIS_CRS(new PacketEventArgs<IS_CRS>(new IS_CRS(packet)));
                    break;
                case PacketType.ISP_BFN:
                    OnIS_BFN(new PacketEventArgs<IS_BFN>(new IS_BFN(packet)));
                    break;
                case PacketType.ISP_AXI:
                    OnIS_AXI(new PacketEventArgs<IS_AXI>(new IS_AXI(packet)));
                    break;
                case PacketType.ISP_AXO:
                    OnIS_AXO(new PacketEventArgs<IS_AXO>(new IS_AXO(packet)));
                    break;
                case PacketType.ISP_BTC:
                    OnIS_BTC(new PacketEventArgs<IS_BTC>(new IS_BTC(packet)));
                    break;
                case PacketType.ISP_BTT:
                    OnIS_BTT(new PacketEventArgs<IS_BTT>(new IS_BTT(packet)));
                    break;
                case PacketType.ISP_RIP:
                    OnIS_RIP(new PacketEventArgs<IS_RIP>(new IS_RIP(packet)));
                    break;
                case PacketType.ISP_SSH:
                    OnIS_SSH(new PacketEventArgs<IS_SSH>(new IS_SSH(packet)));
                    break;
                case PacketType.ISP_CON:
                    OnIS_CON(new PacketEventArgs<IS_CON>(new IS_CON(packet)));
                    break;
                case PacketType.ISP_OBH:
                    OnIS_OBH(new PacketEventArgs<IS_OBH>(new IS_OBH(packet)));
                    break;
                case PacketType.ISP_HLV:
                    OnIS_HLV(new PacketEventArgs<IS_HLV>(new IS_HLV(packet)));
                    break;
                case PacketType.ISP_AXM:
                    OnIS_AXM(new PacketEventArgs<IS_AXM>(new IS_AXM(packet)));
                    break;
                case PacketType.ISP_ACR:
                    OnIS_ACR(new PacketEventArgs<IS_ACR>(new IS_ACR(packet)));
                    break;
                case PacketType.ISP_NCI:
                    OnIS_NCI(new PacketEventArgs<IS_NCI>(new IS_NCI(packet)));
                    break;
                case PacketType.ISP_UCO:
                    OnIS_UCO(new PacketEventArgs<IS_UCO>(new IS_UCO(packet)));
                    break;
                case PacketType.ISP_SLC:
                    OnIS_SLC(new PacketEventArgs<IS_SLC>(new IS_SLC(packet)));
                    break;
                case PacketType.ISP_CSC:
                    OnIS_CSC(new PacketEventArgs<IS_CSC>(new IS_CSC(packet)));
                    break;
                case PacketType.ISP_CIM:
                    OnIS_CIM(new PacketEventArgs<IS_CIM>(new IS_CIM(packet)));
                    break;
            }
        }

        protected virtual void OnIS_VER(PacketEventArgs<IS_VER> e)
        {
            IS_VER?.Invoke(this, e);
        }

        protected virtual void OnIS_TINY(PacketEventArgs<IS_TINY> e)
        {
            IS_TINY?.Invoke(this, e);
        }

        protected virtual void OnIS_SMALL(PacketEventArgs<IS_SMALL> e)
        {
            IS_SMALL?.Invoke(this, e);
        }

        protected virtual void OnIS_STA(PacketEventArgs<IS_STA> e)
        {
            IS_STA?.Invoke(this, e);
        }

        protected virtual void OnIS_CPP(PacketEventArgs<IS_CPP> e)
        {
            IS_CPP?.Invoke(this, e);
        }

        protected virtual void OnIS_ISM(PacketEventArgs<IS_ISM> e)
        {
            IS_ISM?.Invoke(this, e);
        }

        protected virtual void OnIS_MSO(PacketEventArgs<IS_MSO> e)
        {
            IS_MSO?.Invoke(this, e);
        }

        protected virtual void OnIS_III(PacketEventArgs<IS_III> e)
        {
            IS_III?.Invoke(this, e);
        }

        protected virtual void OnIS_VTN(PacketEventArgs<IS_VTN> e)
        {
            IS_VTN?.Invoke(this, e);
        }

        protected virtual void OnIS_RST(PacketEventArgs<IS_RST> e)
        {
            IS_RST?.Invoke(this, e);
        }

        protected virtual void OnIS_NCN(PacketEventArgs<IS_NCN> e)
        {
            IS_NCN?.Invoke(this, e);
        }

        protected virtual void OnIS_CNL(PacketEventArgs<IS_CNL> e)
        {
            IS_CNL?.Invoke(this, e);
        }

        protected virtual void OnIS_CPR(PacketEventArgs<IS_CPR> e)
        {
            IS_CPR?.Invoke(this, e);
        }

        protected virtual void OnIS_NPL(PacketEventArgs<IS_NPL> e)
        {
            IS_NPL?.Invoke(this, e);
        }

        protected virtual void OnIS_PLP(PacketEventArgs<IS_PLP> e)
        {
            IS_PLP?.Invoke(this, e);
        }

        protected virtual void OnIS_PLL(PacketEventArgs<IS_PLL> e)
        {
            IS_PLL?.Invoke(this, e);
        }

        protected virtual void OnIS_LAP(PacketEventArgs<IS_LAP> e)
        {
            IS_LAP?.Invoke(this, e);
        }

        protected virtual void OnIS_SPX(PacketEventArgs<IS_SPX> e)
        {
            IS_SPX?.Invoke(this, e);
        }

        protected virtual void OnIS_PIT(PacketEventArgs<IS_PIT> e)
        {
            IS_PIT?.Invoke(this, e);
        }

        protected virtual void OnIS_PSF(PacketEventArgs<IS_PSF> e)
        {
            IS_PSF?.Invoke(this, e);
        }

        protected virtual void OnIS_PLA(PacketEventArgs<IS_PLA> e)
        {
            IS_PLA?.Invoke(this, e);
        }

        protected virtual void OnIS_CCH(PacketEventArgs<IS_CCH> e)
        {
            IS_CCH?.Invoke(this, e);
        }

        protected virtual void OnIS_PEN(PacketEventArgs<IS_PEN> e)
        {
            IS_PEN?.Invoke(this, e);
        }

        protected virtual void OnIS_TOC(PacketEventArgs<IS_TOC> e)
        {
            IS_TOC?.Invoke(this, e);
        }

        protected virtual void OnIS_FLG(PacketEventArgs<IS_FLG> e)
        {
            IS_FLG?.Invoke(this, e);
        }

        protected virtual void OnIS_PFL(PacketEventArgs<IS_PFL> e)
        {
            IS_PFL?.Invoke(this, e);
        }

        protected virtual void OnIS_FIN(PacketEventArgs<IS_FIN> e)
        {
            IS_FIN?.Invoke(this, e);
        }

        protected virtual void OnIS_RES(PacketEventArgs<IS_RES> e)
        {
            IS_RES?.Invoke(this, e);
        }

        protected virtual void OnIS_REO(PacketEventArgs<IS_REO> e)
        {
            IS_REO?.Invoke(this, e);
        }

        protected virtual void OnIS_NLP(PacketEventArgs<IS_NLP> e)
        {
            IS_NLP?.Invoke(this, e);
        }

        protected virtual void OnIS_MCI(PacketEventArgs<IS_MCI> e)
        {
            IS_MCI?.Invoke(this, e);
        }

        protected virtual void OnIS_CRS(PacketEventArgs<IS_CRS> e)
        {
            IS_CRS?.Invoke(this, e);
        }

        protected virtual void OnIS_BFN(PacketEventArgs<IS_BFN> e)
        {
            IS_BFN?.Invoke(this, e);
        }

        protected virtual void OnIS_AXI(PacketEventArgs<IS_AXI> e)
        {
            IS_AXI?.Invoke(this, e);
        }

        protected virtual void OnIS_AXO(PacketEventArgs<IS_AXO> e)
        {
            IS_AXO?.Invoke(this, e);
        }

        protected virtual void OnIS_BTC(PacketEventArgs<IS_BTC> e)
        {
            IS_BTC?.Invoke(this, e);
        }

        protected virtual void OnIS_BTT(PacketEventArgs<IS_BTT> e)
        {
            IS_BTT?.Invoke(this, e);
        }

        protected virtual void OnIS_RIP(PacketEventArgs<IS_RIP> e)
        {
            IS_RIP?.Invoke(this, e);
        }

        protected virtual void OnIS_SSH(PacketEventArgs<IS_SSH> e)
        {
            IS_SSH?.Invoke(this, e);
        }

        protected virtual void OnIS_CON(PacketEventArgs<IS_CON> e)
        {
            IS_CON?.Invoke(this, e);
        }

        protected virtual void OnIS_OBH(PacketEventArgs<IS_OBH> e)
        {
            IS_OBH?.Invoke(this, e);
        }

        protected virtual void OnIS_HLV(PacketEventArgs<IS_HLV> e)
        {
            IS_HLV?.Invoke(this, e);
        }

        protected virtual void OnIS_AXM(PacketEventArgs<IS_AXM> e)
        {
            IS_AXM?.Invoke(this, e);
        }

        protected virtual void OnIS_ACR(PacketEventArgs<IS_ACR> e)
        {
            IS_ACR?.Invoke(this, e);
        }

        protected virtual void OnIS_NCI(PacketEventArgs<IS_NCI> e)
        {
            IS_NCI?.Invoke(this, e);
        }

        protected virtual void OnIS_UCO(PacketEventArgs<IS_UCO> e)
        {
            IS_UCO?.Invoke(this, e);
        }

        protected virtual void OnIS_SLC(PacketEventArgs<IS_SLC> e)
        {
            IS_SLC?.Invoke(this, e);
        }

        protected virtual void OnIS_CSC(PacketEventArgs<IS_CSC> e)
        {
            IS_CSC?.Invoke(this, e);
        }

        protected virtual void OnIS_CIM(PacketEventArgs<IS_CIM> e)
        {
            IS_CIM?.Invoke(this, e);
        }
    }
}
