using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace DriveBackupDesktop
{
    class DriveApi
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        //static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Drive Backup Desktop";

        public void StartBackup()
        {
            UserCredential credential;
            FileManager FileManager = new FileManager();

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API googleDriveService.
            var googleDriveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            /*FilesResource.ListRequest listRequest = googleDriveService.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";*/

            // List files.
            //IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            Dictionary<string, string> newFolder = CreateFolder("NewFolder_" + Common.getDateToday("dd-MM-yyyy-HH:mm:ss"), googleDriveService);
            UploadBasic(@"D:\temp\001-small-dummy.zip", newFolder["id"], FileManager.GetFilePaths(), googleDriveService);

        }

        private static Dictionary<string, string> CreateFolder(string folderName, DriveService googleDriveService)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };
            var request = googleDriveService.Files.Create(fileMetadata);
            request.Fields = "id, name";
            var file = request.Execute();
            //Console.WriteLine("Folder ID: " + file.Id);
            Dictionary<string, string> createdFolder = new Dictionary<string, string>();
            createdFolder.Add("name", file.Name);
            createdFolder.Add("id", file.Id);
            return createdFolder;
        }

        private static void UploadBasic(string path, string remoteFolderId, HashSet<string> localFilesToUpload, DriveService googleDriveService)
        {
            foreach (string fileToUpload in localFilesToUpload)
            {
                var fileMetadata = new Google.Apis.Drive.v3.Data.File();
                fileMetadata.Parents = new[] { remoteFolderId };
                fileMetadata.Name = Path.GetFileName(fileToUpload);
                fileMetadata.MimeType = "application/zip";
                //System.Diagnostics.Debug.WriteLine(fileMetadata);
                FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(fileToUpload, System.IO.FileMode.Open))
                {
                    request = googleDriveService.Files.Create(fileMetadata, stream, "application/zip");
                    request.Fields = "id";
                    request.Upload();
                }

                var file = request.ResponseBody;
            }
        }

    }
}