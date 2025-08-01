using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace HypoxicTimer
{
    public class Diary
    {
        //Diary Entries
        public class DiaryEntry
        {
            public string PacketFile;
            public DateTime Timestamp;
            public int HypoxicTraningIndexLimited = 0;
            public int HypoxicTraningIndexFull = 0;
            public int?[] SecondsAtPercentage = null;
            public string comment = "";
            public void ParsePackets(List<RealTimePacket> packetlist)
            {
                int HypoxicTraningIndexLimitedSeconds = 0;
                int HypoxicTraningIndexFullSeconds = 0;
                SecondsAtPercentage = new int?[100 - 60]; //from 60 to 100
                for (int i = 0; i < SecondsAtPercentage.Length; i++)
                    SecondsAtPercentage[i] = 0;
                foreach (RealTimePacket aRealTimePacket in packetlist)
                {
                    if (aRealTimePacket.SpO2 < 90 && aRealTimePacket.SpO2 != 0)
                    {
                        int SpO2 = aRealTimePacket.SpO2 > 75 ? aRealTimePacket.SpO2 : 75;
                        HypoxicTraningIndexLimitedSeconds += (90 - SpO2);
                        HypoxicTraningIndexFullSeconds += (90 - aRealTimePacket.SpO2);
                    }
                    if (aRealTimePacket.SpO2 > 60 && aRealTimePacket.SpO2 < 100)
                    {
                        SecondsAtPercentage[aRealTimePacket.SpO2 - 60]++;
                    }
                }
                HypoxicTraningIndexLimited = (HypoxicTraningIndexLimitedSeconds / 60);
                HypoxicTraningIndexFull = (HypoxicTraningIndexFullSeconds / 60);
            }

            public void Debug()
            {
                Logger.Log("File " + PacketFile, 0);
                Logger.Log("Timestamp " + Timestamp, 0);
                Logger.Log("HypoxicTraningIndexLimited " + HypoxicTraningIndexLimited, 0);
                Logger.Log("HypoxicTraningIndexFull " + HypoxicTraningIndexFull, 0);
                if (SecondsAtPercentage != null)
                {
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    foreach (int? j in SecondsAtPercentage)
                    {
                        sb.Append("[" + (i + 60) + "] " + j + ", ");
                        i++;
                    }
                    Logger.Log("SecondsAtPercentage " + sb, 0);
                }
            }
        }
        //Data
        public SerializableDictionary<DateTime, DiaryEntry> Entries = new SerializableDictionary<DateTime, DiaryEntry>();

        public const string DiaryName = "HypoxicDiary.xml";
        private Diary()
        {
        }

        public void SaveXML(string SaveTo, string BackupTo)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());

            if (!Directory.Exists(SaveTo))
                Directory.CreateDirectory(SaveTo);
            String datestamp = DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm");

            using (Stream stream = new FileStream(SaveTo + "\\" + DiaryName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(stream, this);
            }
            if (!string.IsNullOrEmpty(BackupTo))
            {
                File.Copy(SaveTo + "\\" + DiaryName, BackupTo + "\\" + DiaryName);
            }
        }

        public static Diary GetDiary(string SaveTo)
        {
            if (!Directory.Exists(SaveTo))
                Directory.CreateDirectory(SaveTo);
            string DiaryPathName = SaveTo + "\\" + DiaryName;
            Logger.Log("Looking for " + DiaryPathName, 1);
            Diary aDiary = null;
            if (File.Exists(DiaryPathName))
            {
                Logger.Log("Opening " + DiaryPathName, 1);
                XmlSerializer aXmlSerializer = new XmlSerializer(typeof(Diary));
                using (Stream aStream = File.OpenRead(DiaryPathName))
                {
                    aDiary = (Diary)aXmlSerializer.Deserialize(aStream);
                }
            }
            else
            {
                aDiary = new Diary();
            }
            aDiary.FindNewPackets(SaveTo);
            aDiary.FindNewMasimo(SaveTo);
            return aDiary;
        }

        private const string packetfilter = "Packets*.xml";
        public void FindNewPackets(string SaveTo)
        {
            Logger.Log("Looking in " + SaveTo + " for " + packetfilter, 1);

            foreach (string aFilePath in Directory.GetFiles(SaveTo, packetfilter))
            {
                Logger.Log("Found " + aFilePath, 1);
                //Packets_2010_07_12_19_51
                FileInfo aFileInfo = new FileInfo(aFilePath);
                string aFileName = aFileInfo.Name;
                string year = aFileName.Substring(8, 4);
                string mon = aFileName.Substring(13, 2);
                string day = aFileName.Substring(16, 2);
                string hour = aFileName.Substring(19, 2);
                string min = aFileName.Substring(22, 2);
                DateTime timestamp;
                try
                {
                    timestamp = new DateTime(
                        int.Parse(year),
                        int.Parse(mon),
                        int.Parse(day),
                        0, //int.Parse(hour),
                        0, //int.Parse(min),
                        0);
                }
                catch (Exception e)
                {
                    Logger.Log("Could not parse " + aFilePath + " because " + e, 1);
                    return;
                }
                DiaryEntry aDiaryEntry = null;
                if (Entries.ContainsKey(timestamp))
                {
                    Logger.Log("All ready exists " + aFilePath, 1);
                    aDiaryEntry = Entries[timestamp];
                }
                else
                {
                    aDiaryEntry = new DiaryEntry();
                }
                aDiaryEntry.Timestamp = timestamp;
                aDiaryEntry.PacketFile = aFilePath;
                List<RealTimePacket> packetlist = Oximeter.LoadDataFromStream(new FileInfo(aFilePath).OpenRead());
                aDiaryEntry.ParsePackets(packetlist);

                Entries[timestamp] = aDiaryEntry;
                Logger.Log("Added " + aFilePath, 1);
            }

            AddEarlier();
        }

        private const string masimofilter = "*masimo*.csv";
        public void FindNewMasimo(string SaveTo)
        {
            Logger.Log("Looking in " + SaveTo + " for " + masimofilter, 1);

            foreach (string aFilePath in Directory.GetFiles(SaveTo, masimofilter))
            {
                Logger.Log("Found " + aFilePath, 1);
                Masimo aMasimo = Masimo.ReadFile(aFilePath);
                DiaryEntry aDiaryEntry = null;
                if (Entries.ContainsKey(aMasimo.Timestamp.Date))
                {
                    Logger.Log("All ready exists " + aFilePath, 1);
                    aDiaryEntry = Entries[aMasimo.Timestamp.Date];
                }
                else
                {
                    aDiaryEntry = new DiaryEntry();
                }
                aDiaryEntry.ParsePackets(aMasimo.Packets);

                Entries[aMasimo.Timestamp.Date] = aDiaryEntry;
                Logger.Log("Added " + aFilePath, 1);
            }

            AddEarlier();
        }

        public void AddMasimo(Masimo aMasimo)
        {
            DiaryEntry aDiaryEntry = null;
            if (Entries.ContainsKey(aMasimo.Timestamp.Date))
            {
                Logger.Log("All ready exists ", 1);
                aDiaryEntry = Entries[aMasimo.Timestamp.Date];
            }
            else
            {
                aDiaryEntry = new DiaryEntry();
            }
            aDiaryEntry.ParsePackets(aMasimo.Packets);

            Entries[aMasimo.Timestamp.Date] = aDiaryEntry;
            Logger.Log("Added Masimo", 1);

            AddEarlier();
        }

        private void AddEarlier()
        {
            if (Entries.Count > 0)
            {
                DateTime earliest = DateTime.Now;
                foreach (KeyValuePair<DateTime, DiaryEntry> aKeyValuePair in Entries)
                {
                    if (aKeyValuePair.Key.CompareTo(earliest) < 0)
                    {
                        earliest = aKeyValuePair.Key;
                    }
                }
                while (earliest.CompareTo(DateTime.Now.AddDays(-2)) < 0)
                {
                    earliest = earliest.AddDays(1);
                    if (!Entries.ContainsKey(earliest))
                    {
                        DiaryEntry aDiaryEntry = new DiaryEntry();
                        aDiaryEntry.Timestamp = earliest;
                        aDiaryEntry.PacketFile = null;
                        Entries.Add(earliest, aDiaryEntry);
                    }
                }
            }
        }


        public void Debug()
        {
            Logger.Log("Diary with " + Entries.Count + " entries", 1);
            foreach (KeyValuePair<DateTime, DiaryEntry> aKeyValuePair in Entries)
            {
                aKeyValuePair.Value.Debug();
            }
}

    }
}
