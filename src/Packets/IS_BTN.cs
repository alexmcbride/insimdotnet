using System;
using System.Globalization;

namespace InSimDotNet.Packets {
    /// <summary>
    /// InSim Button packet.
    /// </summary>
    /// <remarks>
    /// Used to send a button to InSim.
    /// </remarks>
    public class IS_BTN : IPacket, ISendable {
        /// <summary>
        /// Gets the size of the packet.
        /// </summary>
        public byte Size { get; private set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketType Type { get; private set; }

        /// <summary>
        /// Gets or sets the packet request ID (returned in <see cref="IS_BTC"/> and <see cref="IS_BTT"/> packets).
        /// </summary>
        public byte ReqI { get; set; }

        /// <summary>
        /// Gets or sets the connection to display the button to (0 = local / 255 = all).
        /// </summary>
        public byte UCID { get; set; }

        /// <summary>
        /// Gets or sets the unique button click ID.
        /// </summary>
        public byte ClickID { get; set; }

        /// <summary>
        /// Used internally by InSim.
        /// </summary>
        public byte Inst { get; set; }

        /// <summary>
        /// Gets or sets the button style flags.
        /// </summary>
        public ButtonStyles BStyle { get; set; }

        /// <summary>
        /// Gets or sets the max characters the user is allowed to type in. Setting this to non-zero turns
        /// the button into a text-entry prompt.
        /// </summary>
        public byte TypeIn { get; set; }

        /// <summary>
        /// Gets or sets the distance from the left of the screen the button will be displayed (0 to 200).
        /// </summary>
        public byte L { get; set; }

        /// <summary>
        /// Gets or sets the distance from the top of the screen the button will be displayed (0 to 200).
        /// </summary>
        public byte T { get; set; }

        /// <summary>
        /// Gets or sets the width of the button (0 to 200).
        /// </summary>
        public byte W { get; set; }

        /// <summary>
        /// Gets or sets the height of the button (0 to 200).
        /// </summary>
        public byte H { get; set; }

        /// <summary>
        /// Gets or sets the text of the button.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the caption for a type-in button. Only used if TypeIn is set to non-zero.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Creates a new button packet.
        /// </summary>
        public IS_BTN() {
            Size = 12;
            Type = PacketType.ISP_BTN;
            Text = String.Empty;
            Caption = String.Empty;
        }

        /// <summary>
        /// Returns the packet data.
        /// </summary>
        /// <returns>The packet data.</returns>
        public byte[] GetBuffer() {
            string text = Text;

            // Add button caption if set.
            if (!String.IsNullOrEmpty(Caption) && TypeIn > 0) {
                text = String.Format("{0}{1}{0}{2}", Char.MinValue, Caption, text);
            }

            // Need to decode string first so we know how big to make the packet.
            byte[] buffer = new byte[240];
            int length = LfsEncoding.Instance.GetBytes(text, buffer, 0, 240);

            // Get packet size.
            Size = (byte)(12 + Math.Min(length + (4 - (length % 4)), 240));

            PacketWriter writer = new PacketWriter(Size);
            writer.Write((byte)Size);
            writer.Write((byte)Type);
            writer.Write(ReqI);
            writer.Write(UCID);
            writer.Write(ClickID);
            writer.Write(Inst);
            writer.Write((byte)BStyle);
            writer.Write(TypeIn);
            writer.Write(L);
            writer.Write(T);
            writer.Write(W);
            writer.Write(H);
            writer.Write(buffer, length);
            return writer.GetBuffer();
        }
    }
}
