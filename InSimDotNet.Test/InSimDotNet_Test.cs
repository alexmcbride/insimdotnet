using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InSimDotNet.Packets;

namespace InSimDotNet.Test
{
    class InSimDotNet_Test
    {
        public static InSim inSim;
        static void Main(string[] args)
        {
            inSim = new InSim();

            inSim.Bind<IS_VER>((insim, ver) =>
            {
                insim.Send($"InSim version: {ver.InSimVer}, LFS Version: {ver.Version}, Product: {ver.Product}");
            });

            inSim.Bind<IS_NPL>((insim, npl) =>
            {
                insim.Send($"Joining with {npl.CName}");
            });
            
            inSim.Initialize(new InSimSettings
            {
                Admin = "adminpass",
                Host = "127.0.0.1",
                Port = 29999
            });
            
            inSim.Send(new IS_TINY {ReqI = 4, SubT = TinyType.TINY_VER});
            inSim.Send("Hello, test complete!");

            while (inSim.IsConnected)
            {
                Thread.Sleep(200);
            }
        }
    }
}
