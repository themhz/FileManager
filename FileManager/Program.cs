using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Http;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Program
    {
        private const string PathToServiceAccountKeyFile = @"C:\Users\themis\Downloads\canvas-hybrid-375716-701dab79677d.json";
        //private const string ServiceAccountEmail = "testserviceaccountname@youtubebot-339915.iam.gserviceaccount.com";
        private const string ServiceAccountEmail = "filemanagerservice@canvas-hybrid-375716.iam.gserviceaccount.com";
        private const string UploadFileName = "Test hello.txt";
        private const string DirectoryId = "1BngNlDqGa-rz3DBsd_MJYpONMRBd4XIp";
        static void Main(string[] args)
        {
            UploadToGoogleTest();
        }

        static async void UploadToGoogleTest()
        {
            Console.WriteLine("Hello World!");

            var credential = GoogleCredential.FromFile(PathToServiceAccountKeyFile)
                .CreateScoped(DriveService.ScopeConstants.Drive);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "Test hello.txt",
                Parents = new List<String>() { DirectoryId }
            };

            string uploadedFileId;
            await using (var fsSource = new FileStream(UploadFileName, FileMode.Open, FileAccess.Read))
            {
                var request = service.Files.Create(fileMetadata, fsSource, "text/plain");
                
                request.Fields = "*";
                try
                {
                    var results = await request.UploadAsync(CancellationToken.None);
                    if (results.Status == Google.Apis.Upload.UploadStatus.Failed)
                    {
                        Console.WriteLine($"Error ulloading file:{results.Exception.Message}");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());   
                }
                
                

                uploadedFileId = request.ResponseBody?.Id;
            }
        }
    }
}
