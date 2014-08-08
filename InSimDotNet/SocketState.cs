using System.Net.Sockets;

namespace InSimDotNet {
    internal sealed class SocketState {
        public Socket Socket { get; private set; }
        public byte[] Buffer { get; private set; }
        public int Offset { get; set; }

        public SocketState(Socket socket, byte[] buffer) {
            Socket = socket;
            Buffer = buffer;
            Offset = 0;
        }
    }
}
