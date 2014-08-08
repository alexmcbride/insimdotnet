using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using InSimDotNet.Packets;

namespace InSimDotNet {
    /// <summary>
    /// Provides initialization settings for the <see cref="InSim"/> connection with LFS.
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Saves an instance of <see cref="InSimSettings"/> to an XML file.
        /// </summary>
        /// <param name="settings">The instance of <see cref="InSimSettings"/> to save.</param>
        /// <param name="path">The full path to where the XML file will be written.</param>
        public static void SaveToXml(InSimSettings settings, string path) {
            if (settings == null) {
                throw new ArgumentNullException("settings");
            }

            XElement xSettings = new XElement("Settings");
            xSettings.Add(new XElement("Host", settings.Host));
            xSettings.Add(new XElement("Port", settings.Port));
            xSettings.Add(new XElement("Admin", settings.Admin));
            xSettings.Add(new XElement("UdpPort", settings.UdpPort));
            xSettings.Add(new XElement("Flags", settings.Flags));

            // There are a bunch of chars XML cannot encode, so we use this little hack to make 
            // sure none of them get saved. Maybe should just throw an error?
            xSettings.Add(new XElement("Prefix", IsLegalXmlChar(settings.Prefix) ? settings.Prefix.ToString() : null));

            xSettings.Add(new XElement("Interval", settings.Interval));
            xSettings.Add(new XElement("IName", settings.IName));
            xSettings.Add(new XElement("IsRelayHost", settings.IsRelayHost));
            xSettings.Save(path);
        }

        /// <summary>
        /// Loads an instance of <see cref="InSimSettings"/> from an XML file.
        /// </summary>
        /// <param name="path">The full path to the XML file on disk.</param>
        /// <returns>A populated <see cref="InSimSettings"/> object.</returns>
        public static InSimSettings LoadFromXml(string path) {
            XElement xSettings = XElement.Load(path);

            return new InSimSettings {
                Host = xSettings.Element("Host").Value,
                Port = XmlConvert.ToInt32(xSettings.Element("Port").Value),
                Admin = xSettings.Element("Admin").Value,
                UdpPort = XmlConvert.ToInt32(xSettings.Element("UdpPort").Value),
                Flags = (InSimFlags)XmlConvert.ToInt32(xSettings.Element("Flags").Value),
                Prefix = xSettings.Element("Prefix").Value.SingleOrDefault(),
                Interval = XmlConvert.ToInt32(xSettings.Element("Interval").Value),
                IName = xSettings.Element("IName").Value,
                IsRelayHost = XmlConvert.ToBoolean(xSettings.Element("IsRelayHost").Value),
            };
        }

        private static bool IsLegalXmlChar(int character) {
            // Makes sure the char is legal XML, a bit convoluted but works in 
            // .NET 3.0. For .NET 4.0 can use bool XmlConvert.IsXmlChar(char).
            return character == 0x9 || character == 0xA || character == 0xD ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF);
        }
    }
}
