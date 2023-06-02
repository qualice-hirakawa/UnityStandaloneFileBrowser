using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Win32API.ShObjIdl_core
{
    [ComImport, Guid("973510db-7d7f-452b-8975-74a85828d354"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileDialogEvents
    {
        // NOTE: some of these callbacks are cancelable - returning S_FALSE means that
        // the dialog should not proceed (e.g. with closing, changing folder); to
        // support this, we need to use the PreserveSig attribute to enable us to return
        // the proper HRESULT
        [MethodImpl(MethodImplOptions.InternalCall | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Runtime)]
        uint OnFileOk([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Runtime)]
        uint OnFolderChanging(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psiFolder);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnFolderChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnSelectionChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnShareViolation(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            out FDE_SHAREVIOLATION_RESPONSE pResponse);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnTypeChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnOverwrite(
            [In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd,
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            out FDE_OVERWRITE_RESPONSE pResponse);
    }
}

