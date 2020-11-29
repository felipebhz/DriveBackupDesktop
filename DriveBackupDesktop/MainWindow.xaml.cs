using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using Microsoft.WindowsAPICodePack.Dialogs;
//using System.Windows.Forms;
//using Microsoft.Win32;

// Expose class to use here
using DriveBackupDesktop;


namespace DriveBackupDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var backup = new DriveApi();
            backup.StartBackup();
        }


        private void Choose_Folder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                String path = dialog.SelectedPath;
                UserSelectedPath(path);
                //System.Diagnostics.Debug.WriteLine(path);
            }

        }

        public String UserSelectedPath(string path)
        {
            String UserSelectedPath = path;
            System.Diagnostics.Debug.WriteLine(path);
            return UserSelectedPath;
        }








        /*
        private void Choose_Folder_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.ShowDialog();

        }
*/
        /*private void Choose_Folder_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                String filePath = fileDialog.FileName;
                System.Diagnostics.Debug.WriteLine(filePath);

            }

        }*/






    }
}
