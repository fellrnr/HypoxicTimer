using System;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace HypoxicTimer
{
    class Oximeter
    {
        private static Oximeter singleton = null;
        public delegate void RecievePacketDelegate(RealTimePacket aRealTimePacket);
        private RecievePacketDelegate aRecievePacketDelegate;
        public RecievePacketDelegate RecievePacket { set { aRecievePacketDelegate = value; } }
        public static Oximeter Instance()
        {
            if (singleton == null)
                singleton = new Oximeter();
            return singleton;
        }

        private Thread aThread = null;
        private bool running = false;
        private string ComPort;
        private string OximeterType;
        public void Start(string ComPort, string OximeterType)
        {
            this.ComPort = ComPort;
            this.OximeterType = OximeterType;
            running = true;
            aThread = new Thread(new ParameterizedThreadStart(Worker));
            aThread.Start(this);
        }

        public void Stop()
        {
            running = false;
            //if(aThread != null)
            //    aThread.Join();
        }

        //private List<RealTimePacket> AllPackets = new List<RealTimePacket>();
        private Object LastRealTimePacketMonitor = new Object();
        private RealTimePacket LastRealTimePacket = null;
        private List<RealTimePacket> PerSecondPackets = new List<RealTimePacket>();
        private int PerTicketPacketOffset = 0;
        private bool replay = false;
        public bool IsReplay { get { return replay; } }
        public bool IsRunning { get { return running; } }
        private List<RealTimePacket> OutstandingPackets = new List<RealTimePacket>();
        public static void Worker(object data) 
        { 
            Oximeter that = (Oximeter)data;
            that.Worker();

        }

        public const string PACKET_FOLDER = "HypoxicTimer";
        public void SaveData(string SaveTo, string BackupTo)
        {
            if (replay || PerSecondPackets.Count == 0)
                return; //don't save the replays! 
            try
            {
                Monitor.Enter(PerSecondPackets);
                XmlSerializer serializer = new XmlSerializer(PerSecondPackets.GetType());
                if (!Directory.Exists(SaveTo))
                    Directory.CreateDirectory(SaveTo);
                String datestamp = DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm");
                string filename = "Packets_" + datestamp + ".xml";
                using (Stream stream = new FileStream(SaveTo + "\\" + filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    serializer.Serialize(stream, PerSecondPackets);
                }
                if (!string.IsNullOrEmpty(BackupTo))
                {
                    File.Copy(SaveTo + "\\" + filename, BackupTo + "\\" + filename);
                }
            }
            finally
            {
                Monitor.Exit(PerSecondPackets);
            }
        }

        public void LoadData(string name)
        {
            Stream xml = this.GetType().Assembly.GetManifestResourceStream("HypoxicTimer.Resources." + name);
            LoadData(xml);
        }

        public void LoadData(Stream xml)
        {
            List<RealTimePacket> tmp = PerSecondPackets;
            Monitor.Enter(tmp);
            Monitor.Enter(LastRealTimePacketMonitor);
            try
            {
                XmlSerializer serializer = new XmlSerializer(PerSecondPackets.GetType());
                PerSecondPackets = Oximeter.LoadDataFromStream(xml);
                PerTicketPacketOffset = 0;
                replay = true;
                if (PerSecondPackets.Count > 0)
                    LastRealTimePacket = PerSecondPackets[0];
            }
            finally
            {
                Monitor.Exit(tmp);
                Monitor.Exit(LastRealTimePacketMonitor);
            }
        }

        public static List<RealTimePacket> LoadDataFromStream(Stream xml)
        {
            List<RealTimePacket> tmp = new List<RealTimePacket>(); ;
            XmlSerializer serializer = new XmlSerializer(tmp.GetType());
            tmp = (List<RealTimePacket>)serializer.Deserialize(xml);
            return tmp;
        }

        public List<RealTimePacket> GetOutstandingRealTimePackets()
        {
            List<RealTimePacket> tmp = OutstandingPackets;
            Monitor.Enter(tmp);
            try
            {
                OutstandingPackets = new List<RealTimePacket>();
                return tmp;
            }
            finally
            {
                Monitor.Exit(tmp);
            }
        }

        public RealTimePacket GetLastRealTimePacket()
        {
            Monitor.Enter(LastRealTimePacketMonitor);
            try
            {
                return LastRealTimePacket;
            }
            finally
            {
                Monitor.Exit(LastRealTimePacketMonitor);
            }
        }

        public RealTimePacket GetNextRealTimePacket()
        {
            Monitor.Enter(PerSecondPackets);
            Monitor.Enter(LastRealTimePacketMonitor);
            try
            {
                if (replay)
                {
                    if (PerTicketPacketOffset < PerSecondPackets.Count)
                    {
                        LastRealTimePacket = PerSecondPackets[PerTicketPacketOffset++];
                        return LastRealTimePacket;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    PerSecondPackets.Add(LastRealTimePacket);
                    return LastRealTimePacket;
                }
            }
            finally
            {
                Monitor.Exit(PerSecondPackets);
                Monitor.Exit(LastRealTimePacketMonitor);
            }
        }

    
        public void Worker() 
        {
            //Rate is ~60/second
            while (running)
            {
                Packet aPacket;
                try
                {
                    aPacket = Packet.ReadPacket(ComPort, OximeterType);
                }
                catch (Exception)
                {
                    running = false;
                    return;
                }
                if (aPacket != null && aPacket is RealTimePacket)
                {
                    RealTimePacket aRealPacket = (RealTimePacket)aPacket;
                    Logger.Log(aRealPacket.ToString(), 2);
                    if (aRecievePacketDelegate != null)
                        aRecievePacketDelegate(aRealPacket);
                    Monitor.Enter(LastRealTimePacketMonitor);
                    List<RealTimePacket> tmp = OutstandingPackets;
                    Monitor.Enter(tmp);
                    try
                    {
                        LastRealTimePacket = aRealPacket;
                        OutstandingPackets.Add(aRealPacket);
                    }
                    finally
                    {
                        Monitor.Exit(LastRealTimePacketMonitor);
                        Monitor.Exit(tmp);
                    }
                }
            }
            Logger.Log("Oximeter thread finished", 1);

        }

    }
}
