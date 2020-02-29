using System;
using System.Threading.Tasks;

namespace FileBlaster6000
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FileBlaster6000");

            var fileListProvider = new FileListProvider(args[0]); // Source path

            Parallel.ForEach(fileListProvider.FilePathList, filePath =>
            {
                //
            });
        }
    }
}