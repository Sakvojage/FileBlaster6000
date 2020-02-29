using System.Collections.Concurrent;

namespace FileBlaster6000
{
    public struct CollectedDataStruct
    {
        public ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>> MinTxPowerData;
        public ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>> OkTxPowerData;
        public ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>> MaxTxPowerData;
        public ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<bool>>> PassData;

        public CollectedDataStruct(
            ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>> minTxPowerData,
            ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>> okTxPowerData,
            ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<float>>> maxTxPowerData,
            ConcurrentDictionary<string, ConcurrentDictionary<byte, ConcurrentBag<bool>>> passData
        )
        {
            MinTxPowerData = minTxPowerData;
            OkTxPowerData = okTxPowerData;
            MaxTxPowerData = maxTxPowerData;
            PassData = passData;
        }
    }
}