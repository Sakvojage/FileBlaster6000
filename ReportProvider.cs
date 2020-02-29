using System;
using System.IO;
using System.Linq;

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
        
        public void GenerateReportFile(ref CollectedDataStruct collectedDataRef)
        {
            DateTime time = DateTime.Now;
            string FormattedFilename =
                $"report-{time.Year}-{time.Month}-{time.Day}_{time.Hour}{time.Minute}{time.Second}.csv";

            using (var file = new StreamWriter(_path + FormattedFilename))
            {
                file.WriteLine("BAND,PCL,AvgLow TxPower,AvgOK TxPower,AvgHi TxPower,PASS,FAIL");

                foreach (var band in collectedDataRef.PassData)
                {
                    foreach (var pcl in band.Value)
                    {
                        file.Write(band.Key + "," + pcl.Key + ",");
                        file.Write(collectedDataRef.MinTxPowerData[band.Key][pcl.Key].Average() + ",");
                        file.Write(collectedDataRef.OkTxPowerData[band.Key][pcl.Key].Average() + ",");
                        file.Write(collectedDataRef.MaxTxPowerData[band.Key][pcl.Key].Average() + ",");
                        file.WriteLine(pcl.Value.Count(c => c) + "," + pcl.Value.Count(c => !c));
                    }
                }
            }
        }
    }
}