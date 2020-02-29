using System;
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
            
            Console.WriteLine("FileBlaster6000 is starting. Reading files..");
            
            DateTime start = DateTime.Now;
            var fileListProvider = new FileListProvider(args[0]); // Source path
            var reportProvider = new ReportProvider(args[1]); // target dir

            Parallel.ForEach(fileListProvider.FilePathList, filePath =>
            {
                //
            });
            
            Console.WriteLine((DateTime.Now - start) + " ### Files parsed, calculating...");
            
            //
            
            Console.WriteLine((DateTime.Now - start) + " ### ..finished!");
            Console.ReadLine();
        }
    }
}