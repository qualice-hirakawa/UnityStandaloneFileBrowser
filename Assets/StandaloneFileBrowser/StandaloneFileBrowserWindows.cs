#if UNITY_STANDALONE_WIN

using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Win32API.ShObjIdl_core;
using ShObjIdl_core.WrappedFileDialog;

namespace SFB
{
    // For fullscreen support
    // - WindowWrapper class and GetActiveWindow() are required for modal file dialog.
    // - "PlayerSettings/Visible In Background" should be enabled, otherwise when file dialog opened app window minimizes automatically.

    public class WindowWrapper : IWin32Window
    {
        private IntPtr _hwnd;
        public WindowWrapper(IntPtr handle) { _hwnd = handle; }
        public IntPtr Handle { get { return _hwnd; } }
    }

    public class StandaloneFileBrowserWindows : IStandaloneFileBrowser
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            using (var fd = new FileOpenDialog())
            {
                fd.SetTitle(title);
                if (extensions != null)
                {
                    var specs = new COMDLG_FILTERSPEC[extensions.Length];
                    for (int i = 0; i < extensions.Length; i++)
                    {
                        specs[i].pszName = extensions[i].Name;
                        var size = extensions[i].Extensions.Sum((element) => element.Length) + extensions[i].Extensions.Length * 3;
                        var builder = new StringBuilder(size);
                        foreach (var ext in extensions[i].Extensions)
                        {
                            builder.Append("*.").Append(ext).Append(';');
                        }
                        builder.Remove(builder.Length - 1, 1);
                        specs[i].pszSpec = builder.ToString();
                    }
                    fd.SetFileTypes(specs);
                }
                var fos = FOS.NOCHANGEDIR;
                if (multiselect)
                {
                    fos |= FOS.ALLOWMULTISELECT;
                }
                fd.SetOptions(fos);
                return fd.Show();
            }
            //if (!string.IsNullOrEmpty(directory))
            //{
            //    fd.FileName = GetDirectoryPath(directory);
            //}
            //var res = fd.ShowDialog(new WindowWrapper(GetActiveWindow()));
            //var filenames = res == DialogResult.OK ? fd.FileNames : new string[0];
            //fd.Dispose();
            //return filenames;
        }

        public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFilePanel(title, directory, extensions, multiselect));
        }

        public string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            //var fd = new VistaFolderBrowserDialog();
            //fd.Description = title;
            //if (!string.IsNullOrEmpty(directory))
            //{
            //    fd.SelectedPath = GetDirectoryPath(directory);
            //}
            //var res = fd.ShowDialog(new WindowWrapper(GetActiveWindow()));
            //var filenames = res == DialogResult.OK ? new[] { fd.SelectedPath } : new string[0];
            //fd.Dispose();
            //return filenames;
            return new string[0];
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFolderPanel(title, directory, multiselect));
        }

        public string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            //var fd = new VistaSaveFileDialog();
            //fd.Title = title;

            //var finalFilename = "";

            //if (!string.IsNullOrEmpty(directory))
            //{
            //    finalFilename = GetDirectoryPath(directory);
            //}

            //if (!string.IsNullOrEmpty(defaultName))
            //{
            //    finalFilename += defaultName;
            //}

            //fd.FileName = finalFilename;
            //if (extensions != null)
            //{
            //    fd.Filter = GetFilterFromFileExtensionList(extensions);
            //    fd.FilterIndex = 1;
            //    fd.DefaultExt = extensions[0].Extensions[0];
            //    fd.AddExtension = true;
            //}
            //else
            //{
            //    fd.DefaultExt = string.Empty;
            //    fd.Filter = string.Empty;
            //    fd.AddExtension = false;
            //}
            //var res = fd.ShowDialog(new WindowWrapper(GetActiveWindow()));
            //var filename = res == DialogResult.OK ? fd.FileName : "";
            //fd.Dispose();
            //return filename;
            return string.Empty;
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
        {
            cb.Invoke(SaveFilePanel(title, directory, defaultName, extensions));
        }

        // .NET Framework FileDialog Filter format
        // https://msdn.microsoft.com/en-us/library/microsoft.win32.filedialog.filter
        private static string GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
        {
            var filterString = "";
            foreach (var filter in extensions)
            {
                filterString += filter.Name + "(";

                foreach (var ext in filter.Extensions)
                {
                    filterString += "*." + ext + ",";
                }

                filterString = filterString.Remove(filterString.Length - 1);
                filterString += ") |";

                foreach (var ext in filter.Extensions)
                {
                    filterString += "*." + ext + "; ";
                }

                filterString += "|";
            }
            filterString = filterString.Remove(filterString.Length - 1);
            return filterString;
        }

        private static string GetDirectoryPath(string directory)
        {
            var directoryPath = Path.GetFullPath(directory);
            if (!directoryPath.EndsWith("\\"))
            {
                directoryPath += "\\";
            }
            if (Path.GetPathRoot(directoryPath) == directoryPath)
            {
                return directory;
            }
            return Path.GetDirectoryName(directoryPath) + Path.DirectorySeparatorChar;
        }
    }
}

#endif