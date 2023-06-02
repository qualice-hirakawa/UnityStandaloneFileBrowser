using System;
using Win32API.ShObjIdl_core;

namespace WrappedFileDialog
{
    public class FileOpenDialog : FileDialog<FileOpenDialogInternal>
    {
        private IFileOpenDialog dialog;

        public FileOpenDialog()
        {
            if (comobj is IFileOpenDialog dialog)
            {
                this.dialog = dialog;
            }
            else
            {
                Dispose();
                throw new PlatformNotSupportedException();
            }
        }

        public string[] Show() => Show(IntPtr.Zero);

        public string[] Show(in IntPtr parent)
        {
            try
            {
                dialog.Show(parent);
                dialog.GetResults(out var items);
                items.GetCount(out var count);
                var result = new string[count];
                for (uint i = 0; i < count; i++)
                {
                    items.GetItemAt(i, out var item);
                    item.GetDisplayName(SIGDN.FILESYSPATH, out result[i]);
                }
                return result;
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == Win32API.HRESULT_FROM_WIN32.ERROR_CANCELLED)
                {
                    return new string[0];
                }
                throw ex;
            }
        }

        public void SetFileTypes(in COMDLG_FILTERSPEC[] rgFilterSpec)
        {
            if (rgFilterSpec is null)
            {
                throw new ArgumentNullException(nameof(rgFilterSpec));
            }
            dialog.SetFileTypes((uint)(rgFilterSpec.Length), rgFilterSpec);
        }

        public void SetFileTypeIndex(in uint iFileType) => dialog.SetFileTypeIndex(iFileType);

        public uint GetFileTypeIndex()
        {
            dialog.GetFileTypeIndex(out var piFileType);
            return piFileType;
        }

        public void Advise(in IFileDialogEvents pfde, out uint pdwCookie) => dialog.Advise(pfde, out pdwCookie);

        public void Unadvise(in uint dwCookie) => dialog.Unadvise(dwCookie);

        public void SetOptions(in FOS fos) => dialog.SetOptions(fos);

        public FOS GetOptions()
        {
            dialog.GetOptions(out var pfos);
            return pfos;
        }

        public void SetDefaultFolder(in string path) => dialog.SetDefaultFolder(NativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero));

        public void SetFolder(in string path) => dialog.SetFolder(NativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero));

        public string GetFolder()
        {
            dialog.GetFolder(out var ppsi);
            ppsi.GetDisplayName(SIGDN.FILESYSPATH, out var ppszName);
            return ppszName;
        }

        public void GetCurrentSelection(out IShellItem ppsi) => dialog.GetCurrentSelection(out ppsi);

        public void SetFileName(in string pszName) => dialog.SetFileName(pszName);

        public string GetFileName()
        {
            dialog.GetFileName(out var pszName);
            return pszName;
        }

        public void SetTitle(in string psz0Title) => dialog.SetTitle(psz0Title);

        public void SetOkButtonLabel(in string pszText) => dialog.SetOkButtonLabel(pszText);

        public void SetFileNameLabel(in string pszLabel) => dialog.SetFileNameLabel(pszLabel);

        public void GetResult(out IShellItem ppsi) => dialog.GetResult(out ppsi);

        public string GetResult()
        {
            dialog.GetResult(out var ppsi);
            ppsi.GetDisplayName(SIGDN.FILESYSPATH, out var result);
            return result;
        }

        public void AddPlace(in IShellItem psi, in FDAP fdap) => dialog.AddPlace(psi, fdap);

        public void AddPlace(in string path, in FDAP fdap) => dialog.AddPlace(NativeMethods.SHCreateItemFromParsingName(System.IO.Path.GetFullPath(path)), fdap);

        public void SetDefaultExtension(in string pszDefaultExtension) => dialog.SetDefaultExtension(pszDefaultExtension);

        public void Close(in uint hr) => dialog.Close(hr);

        public void SetClientGuid(ref Guid guid) => dialog.SetClientGuid(ref guid);

        public void ClearClientData() => dialog.ClearClientData();

        public void GetResults(out IShellItemArray ppenum) => dialog.GetResults(out ppenum);

        public string[] GetResults()
        {
            dialog.GetResults(out var ppenum);
            ppenum.GetCount(out var count);
            var results = new string[count];
            for (uint i = 0; i < count; i++)
            {
                ppenum.GetItemAt(i, out var ppsi);
                ppsi.GetDisplayName(SIGDN.FILESYSPATH, out var ppszName);
                results[i] = ppszName;
            }
            return results;
        }

        public void GetSelectedItems(out IShellItemArray ppsai) => dialog.GetSelectedItems(out ppsai);

        public string[] GetSelectedItems()
        {
            dialog.GetResults(out var ppenum);
            ppenum.GetCount(out var count);
            var results = new string[count];
            for (uint i = 0; i < count; i++)
            {
                ppenum.GetItemAt(i, out var ppsi);
                ppsi.GetDisplayName(SIGDN.FILESYSPATH, out var ppszName);
                results[i] = ppszName;
            }
            return results;
        }
    }
}
