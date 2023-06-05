#if UNITY_STANDALONE_WIN

using System;
using System.Text;
using System.Linq;
using Win32API.ShObjIdl_core;
using WrappedFileDialog;

namespace SFB
{
    public class StandaloneFileBrowserWindows : IStandaloneFileBrowser
    {
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
                if (!string.IsNullOrEmpty(directory))
                {
                    fd.SetFolder(directory);
                }
                return fd.Show();
            }
        }

        public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFilePanel(title, directory, extensions, multiselect));
        }

        public string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            using (var fd = new FileOpenDialog())
            {
                fd.SetTitle(title);
                var fos = FOS.NOCHANGEDIR | FOS.PICKFOLDERS;
                if (multiselect)
                {
                    fos |= FOS.ALLOWMULTISELECT;
                }
                fd.SetOptions(fos);
                if (!string.IsNullOrEmpty(directory))
                {
                    fd.SetFolder(directory);
                }
                return fd.Show();
            }
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            cb.Invoke(OpenFolderPanel(title, directory, multiselect));
        }

        public string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            using (var fd = new FileSaveDialog())
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
                    if (extensions.Length > 0 && extensions[0].Extensions.Length > 0)
                    {
                        fd.SetDefaultExtension(extensions[0].Extensions[0]);
                    }
                }
                var fos = FOS.NOCHANGEDIR;
                fd.SetOptions(fos);
                if (!string.IsNullOrEmpty(directory))
                {
                    fd.SetFolder(directory);
                }
                if (!string.IsNullOrEmpty(defaultName))
                {
                    fd.SetFileName(defaultName);
                }
                return fd.Show();
            }
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
        {
            cb.Invoke(SaveFilePanel(title, directory, defaultName, extensions));
        }
    }
}

#endif