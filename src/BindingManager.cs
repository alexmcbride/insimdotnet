using System.Collections.Generic;
using System.Linq;
using InSimDotNet.Packets;

namespace InSimDotNet {
    /// <summary>
    /// References a method to be called when a packet is received.
    /// </summary>
    /// <typeparam name="TPacket">The type of packet.</typeparam>
    /// <param name="insim">A reference to the InSim object that received the packet.</param>
    /// <param name="packet">A decoded packet object.</param>
    public delegate void PacketHandler<TPacket>(InSim insim, TPacket packet) where TPacket : IPacket;

    /// <summary>
    /// References a method to be called when a packet is received.
    /// </summary>
    /// <param name="insim">A reference to the InSim object that received the packet.</param>
    /// <param name="packet">A decoded packet object.</param>
    public delegate void PacketHandler(InSim insim, IPacket packet);

    internal sealed class BindingManager {
        private readonly Dictionary<PacketType, List<IPacketBinding>> packetBindings = new Dictionary<PacketType, List<IPacketBinding>>();

        public void Bind<TPacket>(PacketType packetType, PacketHandler<TPacket> callback)
            where TPacket : IPacket {
            BindInternal(packetType, new PacketBinding<TPacket>(callback));
        }

        public void Bind(PacketType packetType, PacketHandler callback) {
            BindInternal(packetType, new PacketBinding(callback));
        }

        private void BindInternal(PacketType packetType, IPacketBinding binding) {
            if (packetType != PacketType.ISP_NONE) {
                List<IPacketBinding> bindings;
                if (packetBindings.TryGetValue(packetType, out bindings)) {
                    bindings.Add(binding);
                }
                else {
                    packetBindings.Add(packetType, new List<IPacketBinding> { binding });
                }
            }
        }

        public bool IsBound(PacketType packetType) {
            return packetBindings.ContainsKey(packetType);
        }

        public bool IsBound<TPacket>(PacketType packetType, PacketHandler<TPacket> callback)
            where TPacket : IPacket {
            return IsBoundInternal(packetType, new PacketBinding<TPacket>(callback));
        }

        public bool IsBound(PacketType packetType, PacketHandler callback) {
            return IsBoundInternal(packetType, new PacketBinding(callback));
        }

        private bool IsBoundInternal(PacketType packetType, IPacketBinding binding) {
            if (packetType != PacketType.ISP_NONE) {
                List<IPacketBinding> bindings;
                if (packetBindings.TryGetValue(packetType, out bindings)) {
                    return bindings.Any(b => b.Equals(binding));
                }
            }

            return false;
        }

        public void Unbind<TPacket>(PacketType packetType, PacketHandler<TPacket> callback)
            where TPacket : IPacket {
            UnbindInternal(packetType, new PacketBinding<TPacket>(callback));
        }

        public void Unbind(PacketType packetType, PacketHandler callback) {
            UnbindInternal(packetType, new PacketBinding(callback));
        }

        private void UnbindInternal(PacketType packetType, IPacketBinding binding) {
            if (packetType != PacketType.ISP_NONE) {
                List<IPacketBinding> bindings;
                if (packetBindings.TryGetValue(packetType, out bindings)) {
                    bindings.Remove(bindings.Single(b => b.Equals(binding)));

                    // Delete dict key if no bindings left.
                    if (!bindings.Any()) {
                        packetBindings.Remove(packetType);
                    }
                }
            }
        }

        public void ExecuteCallbacks(InSim insim, IPacket packet) {
            List<IPacketBinding> bindings;
            if (packetBindings.TryGetValue(packet.Type, out bindings)) {
                for (int i = 0; i < bindings.Count; i++) {
                    bindings[i].ExecuteCallback(insim, packet);
                }
            }
        }

        private interface IPacketBinding {
            void ExecuteCallback(InSim insim, IPacket packet);
        }

        private class PacketBinding : IPacketBinding {
            private PacketHandler callback;

            public PacketBinding(PacketHandler callback) {
                this.callback = callback;
            }

            public void ExecuteCallback(InSim insim, IPacket packet) {
                callback(insim, packet);
            }

            public override bool Equals(object obj) {
                PacketBinding binding = obj as PacketBinding;
                return binding != null && binding.callback == callback;
            }

            public override int GetHashCode() {
                return callback.GetHashCode();
            }
        }

        private class PacketBinding<TPacket> : IPacketBinding
            where TPacket : IPacket {
            private PacketHandler<TPacket> callback;

            public PacketBinding(PacketHandler<TPacket> callback) {
                this.callback = callback;
            }

            public void ExecuteCallback(InSim insim, IPacket packet) {
                callback(insim, (TPacket)packet);
            }

            public override bool Equals(object obj) {
                PacketBinding<TPacket> binding = obj as PacketBinding<TPacket>;
                return binding != null && binding.callback == callback;
            }

            public override int GetHashCode() {
                return callback.GetHashCode();
            }
        }
    }
}
