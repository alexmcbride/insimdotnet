using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace InSimDotNet.Helpers {
    /// <summary>
    /// Class to help sending large amounts of packets.
    /// </summary>
    /// <remarks>
    /// If you flood InSim with too many packets it will cause an error. This class helps that. It tracks 
    /// how many bytes you send to LFS and inserts a delay if you send too many over a specified period.
    /// </remarks>
    public class BatchHelper {
        private InSim insim;
        private int bytesSent;
        private bool isPaused;
        private List<ISendable> buffer = new List<ISendable>();

        /// <summary>
        /// Gets the underlying InSim object.
        /// </summary>
        public InSim InSim {
            get { return insim; }
        }

        /// <summary>
        /// Gets or sets the max number of bytes to send without delaying.
        /// </summary>
        public int BatchBytes { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds to delay sending for.
        /// </summary>
        public int BatchDelay { get; set; }

        /// <summary>
        /// Creates a new BatchHelper object.
        /// </summary>
        /// <param name="insim">The InSim object to use when sending packets.</param>
        public BatchHelper(InSim insim) {
            if (insim == null) {
                throw new ArgumentNullException("insim");
            }

            this.insim = insim;

            BatchBytes = 1024;
            BatchDelay = 100;

            insim.Initialized += insim_Initialized;

            if (insim.IsConnected) {
                RunAsync();
            }
        }

        /// <summary>
        /// Dispose the object.
        /// </summary>
        public void Dispose() {
            insim.Initialized -= insim_Initialized;
        }

        private void insim_Initialized(object sender, InitializeEventArgs e) {
            buffer.Clear();

            RunAsync();
        }

        private async void RunAsync() {
            if (!insim.IsConnected) {
                return;
            }

            await Task.Delay(BatchDelay);

            if (isPaused) {
                insim.Send(buffer.ToArray());
                buffer.Clear();

                bytesSent = 0;
                isPaused = false;
            }

            if (bytesSent > BatchBytes) {
                isPaused = true;

                Debug.WriteLine("batch sending delay kicked in");
            }

            RunAsync();
        }

        /// <summary>
        /// Sends a packet to LFS.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        public void Send(ISendable packet) {
            bytesSent += packet.Size;

            if (isPaused) {
                buffer.Add(packet);
            }
            else {
                insim.Send(packet);
            }
        }

        /// <summary>
        /// Sends an array of packets to LFS.
        /// </summary>
        /// <param name="packets">The array to send.</param>
        public void Send(params ISendable[] packets) {
            bytesSent += packets.Sum(p => p.Size);

            if (isPaused) {
                buffer.AddRange(packets);
            }
            else {
                insim.Send(packets);
            }
        }
    }
}
