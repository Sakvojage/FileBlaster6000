using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FileBlaster6000
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine($"Arguments missing - /path/to/data and /path/to/output are required.");
                Environment.Exit(1);
            }

            CollectedDataStruct collectedData = new CollectedDataStruct(
                new ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>>(),
                new ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>>(),
                new ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>>(),
                new ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<bool>>>()
            );

            Console.WriteLine("FileBlaster6000 is starting. Reading files..");

            DateTime start = DateTime.Now;
            var fileListProvider = new FileListProvider(args[0]); // Source path
            var reportProvider = new ReportProvider(args[1]); // target dir

            Parallel.ForEach(fileListProvider.FilePathList, filePath =>
            {
                FileDataProvider fileDataProvider = new FileDataProvider(ref collectedData);
                fileDataProvider.GetContents(filePath);
            });

            Console.WriteLine((DateTime.Now - start) + " ### Files parsed, calculating...");

            //

            Console.WriteLine((DateTime.Now - start) + " ### ..finished!");
            Console.ReadLine();
        }
    }
}