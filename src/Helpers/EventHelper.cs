using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace InSimDotNet.Helpers
{
    /// <summary>
    /// Class to manage handling events for multiple InSim objects.
    /// </summary>
    internal class EventHelper
    {
        private class Connection
        {
            public InSim InSim { get; private set; }
            public InSimSettings Settings { get; private set; }

            public Connection(InSim insim, InSimSettings settings)
            {
                InSim = insim;
                Settings = settings;
            }
        }

        private List<Connection> connections;

        /// <summary>
        /// Gets all the current InSim connections.
        /// </summary>
        public ReadOnlyCollection<InSim> Connections
        {
            get { return new ReadOnlyCollection<InSim>(connections.Select(c => c.InSim).ToList()); }
        }

        /// <summary>
        /// Gets if at least one InSim object is connected.
        /// </summary>
        public bool IsConnected
        {
            get { return connections.Select(c => c.InSim.IsConnected).Any(); }
        }

        /// <summary>
        /// Attaches event to all InSim objects.
        /// </summary>
        public event EventHandler<InSimErrorEventArgs> InSimError
        {
            add
            {
                foreach (var conn in connections)
                {
                    conn.InSim.InSimError += value;
                }
            }

            remove
            {
                foreach (var conn in connections)
                {
                    conn.InSim.InSimError -= value;
                }
            }
        }

        /// <summary>
        /// Attaches event to all InSim objects.
        /// </summary>
        public event EventHandler<PacketEventArgs> PacketReceived
        {
            add
            {
                foreach (var conn in connections)
                {
                    conn.InSim.PacketReceived += value;
                }
            }

            remove
            {
                foreach (var conn in connections)
                {
                    conn.InSim.PacketReceived -= value;
                }
            }
        }

        /// <summary>
        /// Attaches event to all InSim objects.
        /// </summary>
        public event EventHandler<InitializeEventArgs> Initialized
        {
            add
            {
                foreach (var conn in connections)
                {
                    conn.InSim.Initialized += value;
                }
            }

            remove
            {
                foreach (var conn in connections)
                {
                    conn.InSim.Initialized -= value;
                }
            }
        }

        /// <summary>
        /// Attaches event to all InSim objects.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected
        {
            add
            {
                foreach (var conn in connections)
                {
                    conn.InSim.Disconnected += value;
                }
            }

            remove
            {
                foreach (var conn in connections)
                {
                    conn.InSim.Disconnected -= value;
                }
            }
        }

        /// <summary>
        /// Creates a new EventHelper
        /// </summary>
        public EventHelper()
        {
            connections = new List<Connection>();
        }

        /// <summary>
        /// Adds a new InSim object to the EventHelper.
        /// </summary>
        /// <param name="settings">The settings for this InSim object.</param>
        /// <returns>The new InSim object</returns>
        public InSim AddInSim(InSimSettings settings)
        {
            var insim = new InSim();
            connections.Add(new Connection(insim, settings));
            return insim;
        }

        /// <summary>
        /// Initializes all of the added InSim objects.
        /// </summary>
        public async Task InitializeAsync()
        {
            foreach (var conn in connections)
            {
                if (!conn.InSim.IsConnected)
                {
                    await conn.InSim.InitializeAsync(conn.Settings);
                }
            }
        }

        /// <summary>
        /// Disconnects all of the InSim objects.
        /// </summary>
        public void Disconnect()
        {
            foreach (var conn in connections)
            {
                if (conn.InSim.IsConnected)
                {
                    conn.InSim.Disconnect();
                }
            }
        }

        /// <summary>
        /// Send a packet to all InSim objects.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        public void Send(ISendable packet)
        {
            foreach (var conn in connections)
            {
                if (conn.InSim.IsConnected)
                {
                    conn.InSim.Send(packet);
                }
            }
        }

        /// <summary>
        /// Sends multiple packets to all InSim objects.
        /// </summary>
        /// <param name="packets">An array of packets to send.</param>
        public void Send(params ISendable[] packets)
        {
            foreach (var conn in connections)
            {
                if (conn.InSim.IsConnected)
                {
                    conn.InSim.Send(packets);
                }
            }
        }

        /// <summary>
        /// Binds a packet handler to all InSim instances.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="callback">The method to call when the packet is received.</param>
        public void Bind<TPacket>(PacketHandler<TPacket> callback) where TPacket : IPacket
        {
            foreach (var conn in connections)
            {
                conn.InSim.Bind<TPacket>(callback);
            }
        }

        /// <summary>
        /// Unbinds a packet handler from all InSim instances.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="callback">The method to unbind.</param>
        public void Unbind<TPacket>(PacketHandler<TPacket> callback) where TPacket : IPacket
        {
            foreach (var conn in connections)
            {
                conn.InSim.Unbind<TPacket>(callback);
            }
        }

        /// <summary>
        /// Determins if a packet handler has been bound to at least one InSim instance.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="callback">The handler to check for.</param>
        /// <returns>True if the handler has been bound.</returns>
        public bool IsBoundAll<TPacket>(PacketHandler<TPacket> callback) where TPacket : IPacket
        {
            // just check and see if one of them in bound. if one is they all are?
            var conn = connections.FirstOrDefault();
            if (conn != null)
            {
                return conn.InSim.IsBound<TPacket>(callback);
            }
            return false;
        }
    }
}
