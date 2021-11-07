using InSimDotNet.Packets;
using System;

namespace InSimDotNet {
    /// <summary>
    /// Provides initialization settings for the <see cref="InSimClient"/> connection with LFS.
    /// </summary>
    public class InSimSettings {
        /// <summary>
        /// Gets or set the address of the remote host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port of the remote host.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the UDP port to use for <see cref="IS_MCI"/> and <see cref="IS_NLP"/> packet updates. If set a 
        /// separate UDP connection will be opened on that port.
        /// </summary>
        public int UdpPort { get; set; }

        /// <summary>
        /// Gets or sets the InSim initialization flags.
        /// </summary>
        public InSimFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the hidden host command prefix.
        /// </summary>
        public char Prefix { get; set; }

        /// <summary>
        /// Gets or sets the number of milliseconds between <see cref="IS_MCI"/> or <see cref="IS_NLP"/> packets.
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or sets the LFS game admin password.
        /// </summary>
        public string Admin { get; set; }

        /// <summary>
        /// Gets or sets a short name for the program.
        /// </summary>
        public string IName { get; set; }

        /// <summary>
        /// Gets or sets if the host is an InSim Relay host. If true all other settings are ignored.
        /// </summary>
        public bool IsRelayHost { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="InSimSettings"/> class.
        /// </summary>
        public InSimSettings() {
            Host = "127.0.0.1";
            Port = 29999;
            Prefix = Char.MinValue;
            Admin = String.Empty;
            IName = "InSim.NET";
        }
    }
}
