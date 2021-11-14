using System;
using System.Linq;

namespace InSimDotNet.Packets
{
    /// <summary>
    /// Class for the <see cref="IS_MAL"/> Info collection.
    /// </summary>
    public class SkinID
    {
        public string StringForm { get; set; }
        public uint CompressedForm { get; set; }
        public SkinID(PacketReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            
            CompressedForm = reader.ReadUInt32();
            StringForm = ((byte)(CompressedForm >> 24)).ToString("X2")
                       + ((byte)(CompressedForm >> 16)).ToString("X2")
                       + ((byte)(CompressedForm >> 8)).ToString("X2");
        }

        public SkinID(string stringForm)
        {
            CompressedForm = Convert.ToUInt32(stringForm, 16); 
            StringForm = stringForm.ToUpper();
        }

        public SkinID(uint compressed)
        {
            CompressedForm = compressed;
            StringForm = ((byte)(CompressedForm >> 24)).ToString("X2")
                       + ((byte)(CompressedForm >> 16)).ToString("X2")
                       + ((byte)(CompressedForm >> 8)).ToString("X2");
        }
    }
}