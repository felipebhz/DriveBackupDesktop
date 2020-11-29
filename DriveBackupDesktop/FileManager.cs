using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DriveBackupDesktop
{
    class FileManager
    {
        public static HashSet<String> filePaths = new HashSet<string>();
        String UserSelectedFolder;

        public string ShowDirSelectDialog()
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return "User Canceled";
            }

            UserSelectedFolder = dialog.FileName;

            if (dialog.FileName != null)
            {
                FileManager.ProcessDirectory(UserSelectedFolder);
            }

            return UserSelectedFolder;
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            filePaths.Add(path);
            GetFilePaths();
        }

        public static HashSet<string> GetFilePaths()
        {
            System.IO.File.WriteAllLines(@"D:\WriteLines"+Common.getDateToday("MM-dd-yyyy")+".txt", filePaths);
            return filePaths;
            
        }

        public String GetUserSelectedPath()
        {
            if (UserSelectedFolder == null)
            {
                return "No folder selected";
            }
            return UserSelectedFolder;
        }

        //DirectoryInfo dir = new DirectoryInfo(string );

        //FileSystemInfo[] infos = dir.GetFileSystemInfos();


    }
}
