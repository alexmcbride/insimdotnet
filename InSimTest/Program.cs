using InSimDotNet;
using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InSimTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunAsync().Wait();
        }

        private async Task RunAsync()
        {
            using (var client = new InSimClient())
            {
                client.IS_VER += Client_IS_VER;
                client.IS_MSO += Client_IS_MSO;
                client.IS_NPL += async (o, e) => { await Client_IS_NPL(o, e); };
                client.IS_MAL += async (o,e) => { await Client_IS_MAL(o, e); };

                await client.InitializeAsync(new InSimSettings
                {
                    Host = "127.0.0.1",
                    Port = 29999
                });

                await client.SendAsync(new IS_MAL(
                    new List<string>
                    {
                        //"3118BF",
                        //"953A1B"
                    }));
                await client.SendAsync(TinyType.TINY_MAL);

                Console.ReadKey(true);
            }
        }
        
        private async Task Client_IS_NPL(object o, PacketEventArgs<IS_NPL> e)
        {
            var insim = (InSimClient)o;
            var npl = e.Packet;
            await insim.SendAsync($"^7Player is joining with ^3{npl}");
        }

        private async Task Client_IS_MAL(object sender, PacketEventArgs<IS_MAL> e)
        {
            var insim = (InSimClient)sender;
            var mal = e.Packet;

            if (mal.NumM == 0) await insim.SendAsync("^7Host allows ^3ALL ^7mods");
            else
            {
                foreach (var skinID in mal.SkinIDs) 
                {
                    await insim.SendAsync($"^7Host allows: ^3{skinID.StringForm}");
                }
            }
        }

        private void Client_IS_MSO(object sender, PacketEventArgs<IS_MSO> e)
        {
            Console.WriteLine(e.Packet.Msg);
        }

        private void Client_IS_VER(object sender, PacketEventArgs<IS_VER> e)
        { 
            Console.WriteLine("InSim connected! " + e.Packet.InSimVer);
        }
    }
}
