using InSimDotNet;
using InSimDotNet.Packets;
using System;
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
                client.IS_MSO += Client_IS_MSO; ;

                await client.InitializeAsync(new InSimSettings
                {
                    Host = "127.0.0.1",
                    Port = 29999,

                });

                Console.ReadKey(true);
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
