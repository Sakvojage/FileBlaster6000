using System;
using System.Collections.Concurrent;
using System.IO;

namespace FileBlaster6000
{
    public class FileDataProvider
    {
         private readonly string[] _bands = {"GSM850", "GSM900", "DCS1800", "PCS1900"};
        private readonly CollectedDataStruct _collectedDataRef;

        public FileDataProvider(ref CollectedDataStruct collectedData)
        {
            _collectedDataRef = collectedData;
        }

        public void GetContents(string filePath)
        {
            try
            {
                using StreamReader reader = new StreamReader(filePath);
                string line;
                reader.BaseStream.Position = 4700;

                while ((line = reader.ReadLine()) != null)
                {
                    ParseAndSubmitData(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void ParseAndSubmitData(string line)
        {
            foreach (var band in _bands)
            {
                if (!line.StartsWith(band)) continue;

                var data = line.Split(',');
                var pcl = Convert.ToByte(data[1]);
                var power = float.Parse(data[2].Trim());
                var powerMin = float.Parse(data[4].Trim());
                var powerMax = float.Parse(data[5].Trim());
                var passed = data[6].Trim().Equals("PASS");

                CreateIndex(band, pcl);

                _collectedDataRef.PassData[band][pcl].Add(passed);

                if (power < powerMin)
                {
                    _collectedDataRef.MinTxPowerData[band][pcl].Add(power);
                }
                else if (power > powerMax)
                {
                    _collectedDataRef.MaxTxPowerData[band][pcl].Add(power);
                }
                else
                {
                    _collectedDataRef.OkTxPowerData[band][pcl].Add(power);
                }

                break;
            }
        }

        private void CreateIndex(string band, byte pcl)
        {
            if (!_collectedDataRef.MinTxPowerData.ContainsKey(band))
            {
                _collectedDataRef.MinTxPowerData.TryAdd(band, new ConcurrentDictionary<byte, ConcurrentBag<float>>());
            }

            if (!_collectedDataRef.MinTxPowerData[band].ContainsKey(pcl))
            {
                _collectedDataRef.MinTxPowerData[band].TryAdd(pcl, new ConcurrentBag<float>());
            }

            //

            if (!_collectedDataRef.OkTxPowerData.ContainsKey(band))
            {
                _collectedDataRef.OkTxPowerData.TryAdd(band, new ConcurrentDictionary<byte, ConcurrentBag<float>>());
            }

            if (!_collectedDataRef.OkTxPowerData[band].ContainsKey(pcl))
            {
                _collectedDataRef.OkTxPowerData[band].TryAdd(pcl, new ConcurrentBag<float>());
            }

            //

            if (!_collectedDataRef.MaxTxPowerData.ContainsKey(band))
            {
                _collectedDataRef.MaxTxPowerData.TryAdd(band, new ConcurrentDictionary<byte, ConcurrentBag<float>>());
            }

            if (!_collectedDataRef.MaxTxPowerData[band].ContainsKey(pcl))
            {
                _collectedDataRef.MaxTxPowerData[band].TryAdd(pcl, new ConcurrentBag<float>());
            }

            //

            if (!_collectedDataRef.PassData.ContainsKey(band))
            {
                _collectedDataRef.PassData.TryAdd(band, new ConcurrentDictionary<byte, ConcurrentBag<bool>>());
            }

            if (!_collectedDataRef.PassData[band].ContainsKey(pcl))
            {
                _collectedDataRef.PassData[band].TryAdd(pcl, new ConcurrentBag<bool>());
            }
        }
    }
}