using System;
using System.Runtime.InteropServices;

namespace Win32API.ShObjIdl_core
{
    [ComImport, Guid("42f85136-db7e-439c-85f1-e4075d135fc8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileDialog : IModalWindow { }
}
