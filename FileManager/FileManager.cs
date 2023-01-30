using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager
{
    internal class FileManager
    {
        public List<string> ListFiles(string path)
        {
            List<string> ListOfFiles = new List<string>();
            try
            {
                var dir = new DirectoryInfo(path);
                var files = dir.GetFiles();
                
                foreach (var file in files)
                {
                    //Console.WriteLine(file.FullName);
                    ListOfFiles.Add(file.FullName);
                }
                var subdirectories = dir.GetDirectories();
                foreach (var subdirectory in subdirectories)
                {
                    ListFiles(subdirectory.FullName);
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return ListOfFiles;
        }
    }
}
