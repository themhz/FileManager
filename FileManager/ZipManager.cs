using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.IO;

namespace FileManager
{
    internal class ZipManager
    {        
        public void ZipDirectory(string sourceDirectory, string destinationZipFile)
        {
            try
            {
                ZipFile.CreateFromDirectory(sourceDirectory, destinationZipFile, CompressionLevel.Fastest, false);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
    }


}
