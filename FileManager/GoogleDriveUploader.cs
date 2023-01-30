using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    public class GoogleDriveUploader
    {
        private DriveService _service;

        public GoogleDriveUploader()
        {
            UserCredential credential;
            using (var stream = new FileStream(@"C:\Users\themis\Downloads\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = @"C:\Users\themis\Downloads\token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Drive API .NET Quickstart",
            });
        }

        public void UploadFile(string filePath, string fileName, string folderId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName,       
                Parents = new string[] { folderId }
            };

            using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open))
            {
                var request = _service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Upload();
            }
        }
    }
}
