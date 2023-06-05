using SFB;
using UnityEngine;

namespace Samples
{
    public class BasicSample : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Text results;

        private System.Text.StringBuilder builder = new System.Text.StringBuilder();

        public void OpenFile() => WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false));

        public void OpenFileAsync() => StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", "", false, (string[] paths) => { WriteResult(paths); });

        public void OpenFileMultiple() => WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", true));

        public void OpenFileExtension() => WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "txt", true));

        public void OpenFileDirectory() => WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", Application.dataPath, "", true));

        public void OpenFileFilter()
        {
            var extensions = new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
                new ExtensionFilter("Sound Files", "mp3", "wav" ),
                new ExtensionFilter("All Files", "*" ),
            };
            WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true));
        }

        public void OpenFolder() => WriteResult(StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true));

        public void OpenFolderAsync() => StandaloneFileBrowser.OpenFolderPanelAsync("Select Folder", "", true, (string[] paths) => { WriteResult(paths); });

        public void OpenFolderDirectory() => WriteResult(StandaloneFileBrowser.OpenFolderPanel("Select Folder", Application.dataPath, true));

        public void SaveFile() => WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "", ""));

        public void SaveFileAsync() => StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", "", (string path) => { WriteResult(path); });

        public void SaveFileDefaultName() => WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", ""));

        public void SaveFileDefaultNameExt() => WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", "dat"));

        public void SaveFileDirectory() => WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "", ""));

        public void SaveFileFilter()
        {
            var extensionList = new[] {
                new ExtensionFilter("Binary", "bin"),
                new ExtensionFilter("Text", "txt"),
            };
            WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", extensionList));
        }

        private void WriteResult(string path) => results.text = path;

        private void WriteResult(string[] paths)
        {
            if (paths.Length == 0)
            {
                return;
            }

            builder.Clear();
            foreach (var p in paths)
            {
                builder.AppendLine(p);
                results.text = builder.ToString();
            }
        }
    }
}
