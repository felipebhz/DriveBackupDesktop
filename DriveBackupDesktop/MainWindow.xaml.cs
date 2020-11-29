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

        public MainWindow()
        {
            InitializeComponent();
            DateTodayLabel.Content = Common.getDateToday("dd/MM/yyyy");
        }

        private void Start_Backup_Button_Click(object sender, RoutedEventArgs e)
        {
            var backup = new DriveApi();
            backup.StartBackup();
        }

        private void Choose_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            FileManager FileManager = new FileManager();
            FileManager.ShowDirSelectDialog();
            SelectedFolderLabel.Content = FileManager.GetUserSelectedPath();
        }

        private void Debug_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        


    }
}
