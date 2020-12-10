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
//using System.Windows.Forms;
//using Microsoft.Win32;

// Expose class to use here
//using DriveBackupDesktop;

namespace DriveBackupDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileManager FileManager = new FileManager();
        public MainWindow()
        {
            InitializeComponent();
            DateTodayLabel.Content = Common.getDateToday("dd/MM/yyyy");
            SelectedFolderLabel.Content = Properties.Settings.Default.ChosenFolderAutoBackup;
        }

        private void Start_Backup_Button_Click(object sender, RoutedEventArgs e)
        {
            var backup = new DriveApi();
            backup.StartBackup();
        }

        private void Choose_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            //FileManager.GetUserSelectedPath();
            FileManager.ShowDirSelectDialog();
            SelectedFolderLabel.Content = Properties.Settings.Default.ChosenFolderAutoBackup;
        }

        private void Debug_Button_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(Properties.Settings.Default.ChosenFolderAutoBackup);
        }




    }
}
