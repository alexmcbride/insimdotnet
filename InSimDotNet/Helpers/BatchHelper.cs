using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InSimDotNet.Helpers {
    public class BatchHelper {
        private InSim insim;
        private List<ISendable> buffer = new List<ISendable>();
        private bool isAlreadySending;

        public int BatchSize { get; set; }
        public int BatchDelay { get; private set; }

        public bool IsConnected {
            get { return insim.IsConnected; }
        }

        public BatchHelper(InSim insim) {
            if (insim == null) {
                throw new ArgumentNullException("insim");
            }

            this.insim = insim;

            BatchSize = 2048; // bytes
            BatchDelay = 100; // milliseconds
        }

        public void Send(ISendable packet) {
            if (!insim.IsConnected) {
                throw new InSimException(StringResources.InSimNotConnectedMessage);
            }

            buffer.Add(packet);

            BatchSendAsync();
        }

        public void Send(params ISendable[] packets) {
            if (!insim.IsConnected) {
                throw new InSimException(StringResources.InSimNotConnectedMessage);
            }

            buffer.AddRange(packets);

            BatchSendAsync();
        }

        private async void BatchSendAsync() {
            if (isAlreadySending) {
                Debug.WriteLine("already sending");
                return;
            }

            isAlreadySending = true;

            while (true) {
                var packets = GetPacketBatch();

                insim.Send(packets.ToArray());

                // wait and see if any more packets added.
                await Task.Delay(BatchDelay);

                Debug.WriteLine("just waited "+ BatchDelay + " ms");

                // if not then bye bye
                if (buffer.Count == 0) {
                    break;
                }
            }

            isAlreadySending = false;
        }

        private IList<ISendable> GetPacketBatch() {
            var packets = new List<ISendable>();

            int size = 0;
            for (int i = 0; i < buffer.Count; i++) {
                if (size + buffer[i].Size < BatchSize) {
                    packets.Add(buffer[i]);
                    size += buffer[i].Size;
                    break;
                }
            }
            buffer.RemoveRange(0, packets.Count);

            return packets;
        }
    }
}
