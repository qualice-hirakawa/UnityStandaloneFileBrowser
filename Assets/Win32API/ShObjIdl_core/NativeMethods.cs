using System;
using System.Runtime.InteropServices;

namespace Win32API.ShObjIdl_core
{
    public static class NativeMethods
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void SHCreateItemFromParsingName(
            [In][MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            [In] IntPtr pbc,
            [In][MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out][MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppv);

        public static IShellItem SHCreateItemFromParsingName(in string pszPath)
        {
            var path = System.IO.Path.GetFullPath(pszPath);
            SHCreateItemFromParsingName(path, IntPtr.Zero, typeof(IShellItem).GUID, out var item);
            return item;
        }

        public static IShellItem SHCreateItemFromParsingName(in string pszPath, IntPtr pbc)
        {
            var path = System.IO.Path.GetFullPath(pszPath);
            SHCreateItemFromParsingName(path, pbc, typeof(IShellItem).GUID, out var item);
            return item;
        }
    }
}
