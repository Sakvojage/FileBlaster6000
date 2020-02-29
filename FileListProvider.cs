using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace FileBlaster6000
{
    public class FileListProvider
    {
        public readonly ConcurrentBag<string> FilePathList = new ConcurrentBag<string>();

        public FileListProvider(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                Parallel.ForEach(files, file => { FilePathList.Add(file); });
            }
            else
            {
                Console.WriteLine($"Directory \"{path}\" does not exist");
                Environment.Exit(1);
            }
        }
    }
}