using System;
using System.Runtime.InteropServices;

namespace Win32API.ShObjIdl_core
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PROPERTYKEY
    {
        public Guid fmtid;
        public uint pid;
    }
}
