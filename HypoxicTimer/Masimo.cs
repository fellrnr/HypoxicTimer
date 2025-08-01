using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HypoxicTimer
{
    public class Masimo
    {
        private DateTime timestamp;

        public DateTime Timestamp
        {
            get { return timestamp; }
        }
        private List<RealTimePacket> packets = new List<RealTimePacket>();

        public List<RealTimePacket> Packets
        {
            get { return packets; }
        }
        const string PulseRateKey = "Pulse Rate";
        const string SpO2Key = "SpO2";
        const string TimestampKey = "Timestamp";

        public static Masimo ReadFile(string FileName)
        {
            Masimo that = new Masimo();
            List<Dictionary<string, string>> csv = Csv.ReadCSVFile(FileName);

            bool first = true;
            foreach (Dictionary<string, string> row in csv)
            {
                //Session, Index, Timestamp, Date, Time, SpO2Key, Pulse Rate, PVI
                long ts = long.Parse(row[TimestampKey]);
                DateTime aDateTime = Utils.UnixTimeStampToDateTime(ts);

                if (first)
                {
                    that.timestamp = aDateTime;
                    first = false;
                }
                else if (that.timestamp.Date != aDateTime.Date)
                {
                    that.timestamp = aDateTime;
                }
                if (row.ContainsKey(PulseRateKey) && row.ContainsKey(SpO2Key))
                {
                    int hr = int.Parse(row[PulseRateKey]);
                    int spo2 = int.Parse(row[SpO2Key]);
                    RealTimePacket aRealPacket = new RealTimePacket();
                    aRealPacket.HeartRate = hr;
                    aRealPacket.SpO2 = spo2;
                    that.packets.Add(aRealPacket);
                }
            }
            return that;
        }
    }
}
