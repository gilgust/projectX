using System.Windows;
using Microsoft.Win32;

namespace projectX.services
{
    class DefaultDialogService :IDialogService
    {

        public string FilePath { get; set; }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public bool OpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != true) return false;
            FilePath = dialog.FileName;
            return true;
        }

        public bool SaveFileDialog()
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() != true) return false;
            FilePath = dialog.FileName;
            return true;
        }
    }
}
