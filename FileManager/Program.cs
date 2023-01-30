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
            var uploader = new GoogleDriveUploader();

            //// Upload a file to Google Drive.
            

            //Console.WriteLine("File uploaded successfully!");
            //Console.ReadLine();

            var FileManager = new FileManager();
            foreach(var file in FileManager.ListFiles(@"C:\Users\themis\Downloads"))
            {
                //Console.WriteLine(file.Split(@"\")[file.Split(@"\").Length-1]);
                string fileName = file.Split(@"\")[file.Split(@"\").Length - 1];
                uploader.UploadFile(file, fileName, "1BngNlDqGa-rz3DBsd_MJYpONMRBd4XIp");
            }
            Console.ReadLine();
        }
          
    }
}
