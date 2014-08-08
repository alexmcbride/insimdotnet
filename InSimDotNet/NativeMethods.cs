using System;
using System.Runtime.InteropServices;

namespace InSimDotNet {
    internal static class NativeMethods {
        public const uint WC_NO_BEST_FIT_CHARS = 0x400;

        [DllImport("kernel32.dll")]
        public static extern int WideCharToMultiByte(
            uint CodePage, // windows codepage e.g. 1251
            uint dwFlags, // optional flags
            [MarshalAs(UnmanagedType.LPWStr)] string lpWideCharStr, // unicode string to convert
            int cchWideChar, // length of unicode string to convert
            [MarshalAs(UnmanagedType.LPArray)] byte[] lpMultiByteStr, // destination buffer
            int cbMultiByte, // length of destination buffer
            IntPtr lpDefaultChar, // optinal fallback char
            [MarshalAs(UnmanagedType.Bool)] out bool lpUsedDefaultChar); // optinal set if fallback char was used
    }
}
