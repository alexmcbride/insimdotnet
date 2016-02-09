using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Experimental - don't use.
    /// </summary>
    public class BatchHelper {
        private InSim insim;
        private List<ISendable> packets = new List<ISendable>();
        private bool isSending;

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

            packets.Add(packet);

            BatchSendAsync();
        }

        public void Send(params ISendable[] packets) {
            if (!insim.IsConnected) {
                throw new InSimException(StringResources.InSimNotConnectedMessage);
            }

            this.packets.AddRange(packets);

            BatchSendAsync();
        }

        private async void BatchSendAsync() {
            if (isSending) {
                Debug.WriteLine("already sending");
                return;
            }

            isSending = true;

            while (true) {
                var packets = GetPacketBatch();

                insim.Send(packets.ToArray());

                // wait and see if any more packets added.
                await Task.Delay(BatchDelay);

                Debug.WriteLine("just waited "+ BatchDelay + " ms");

                // if not then bye bye
                if (this.packets.Count == 0) {
                    break;
                }
            }

            isSending = false;
        }

        private IList<ISendable> GetPacketBatch() {
            var batch = new List<ISendable>();

            // get batch of packets up to max byte size.
            int size = 0;
            for (int i = 0; i < packets.Count; i++) {
                if (size + packets[i].Size < BatchSize) {
                    batch.Add(packets[i]);
                    size += packets[i].Size;
                    break;
                }
            }

            // remove the packets for that last batch.
            packets.RemoveRange(0, batch.Count);

            return batch;
        }
    }
}
