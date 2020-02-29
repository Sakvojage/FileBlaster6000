using System;
using System.IO;

namespace FileBlaster6000
{
    public class ReportProvider
    {
        private readonly string _path;

        public ReportProvider(string path)
        {
            _path = path;
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Output directory \"{path}\" does not exist.");
                Environment.Exit(1);
            }
        } 
    }
}