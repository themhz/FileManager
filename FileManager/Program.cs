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
       
        static void Main(string[] args)
        {
            

            
        }

        static void exportMySqlDatabase()
        {
            var db = new DataBaseExporter();
        }

        static void backupToGoogleDrive()
        {

            var uploader = new GoogleDriveUploader();

            // Upload a file to Google Drive.
            ZipManager zipManager = new ZipManager();
            Console.WriteLine("Starting Zip");
            zipManager.ZipDirectory(@"D:\ProjNet2022\applications\Building.EnergyProject\EnergyBuilding.UI\data\reports\EnergyReport\", @"D:\uploads\test.zip");
            Console.WriteLine("Zip Completed");
            var FileManager = new FileManager();
            Console.WriteLine("Starting upload");
            foreach (var file in FileManager.ListFiles(@"D:\uploads"))
            {
                Console.WriteLine("Uploading " + file);
                string fileName = file.Split(@"\")[file.Split(@"\").Length - 1];
                uploader.UploadFile(file, fileName, "1BngNlDqGa-rz3DBsd_MJYpONMRBd4XIp");
                Console.WriteLine("finished ");
            }
            Console.WriteLine("All uploading completed ");
            Console.ReadLine();
        }
          
    }
}
