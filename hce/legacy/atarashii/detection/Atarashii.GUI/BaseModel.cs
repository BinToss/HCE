using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Atarashii.GUI.Properties;
using Clipboard = System.Windows.Clipboard;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Atarashii.GUI
{
    public class BaseModel : INotifyPropertyChanged
    {
        public LogWindow LogWindow { get; } = new LogWindow();
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Opens up a file picking dialogue window.
        /// </summary>
        /// <param name="filter">
        ///     File filter to use in the dialogue window.
        /// </param>
        /// <returns>
        ///     File name chosen by the end-user.
        /// </returns>
        public static string PickFile(string filter = null)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter ?? string.Empty
            };

            return openFileDialog.ShowDialog() == true
                ? openFileDialog.FileName
                : string.Empty;
        }

        /// <summary>
        ///     Opens up a folder picking dialogue window.
        /// </summary>
        /// <returns>
        ///     Folder path chosen by the end-user.
        /// </returns>
        public static string PickFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowDialog();
                return dialog.SelectedPath;
            }
        }

        /// <summary>
        ///     Copies given data to the clipboard.
        /// </summary>
        public void CopyToClipboard(string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                Clipboard.SetText(data);
                LogWindow.Log("Copied data to the clipboard.");
            }
            else
            {
                LogWindow.Log("Refusing to copy empty data to the clipboard.");
            }
        }

        /// <summary>
        ///     Exits the application with exit code 0.
        /// </summary>
        public static void Exit()
        {
            Environment.Exit(0);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}