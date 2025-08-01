using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HypoxicTimer
{
    class Logger
    {
        static TextWriter logger = null;
        public static void init(string SaveTo)
        {
            if (logger == null)
            {
                try
                {
                    String datestamp = "";
                    //datestamp = "_" + DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm");
                    string path = SaveTo + "\\" + Oximeter.PACKET_FOLDER;
                    if (!Directory.Exists(SaveTo))
                        Directory.CreateDirectory(SaveTo);
                    logger = new StreamWriter(SaveTo + "\\HypoxiaDebug" + datestamp + ".txt");
                }
                catch (Exception)
                {
                }
            }
        }

        static int debugLevel;

        public static int DebugLevel
        {
            get { return Logger.debugLevel; }
            set { Logger.debugLevel = value; }
        }

        public static void Log(string message, int level)
        {
            if (level > DebugLevel)
                return;
            try
            {
                if (logger != null)
                {
                    logger.WriteLine(message);
                    logger.Flush();
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
