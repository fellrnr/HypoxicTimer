using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Drawing;
using System.Media;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace HypoxicTimer
{
        [Serializable]
        public class Packet
        {
            static SerialPort aSerialPort = null;
            enum SupportedOximter { CMS50E, CMS60C };
            static SupportedOximter OximeterType;
            static byte[] cms60cHandshake = { 0x7D, 0x81, 0xA1 };
            static public Packet ReadPacket(string ComPort, string OximeterTypeString)
            {
                if (OximeterTypeString == "CMS 60C")
                    OximeterType = SupportedOximter.CMS60C;
                else
                    OximeterType = SupportedOximter.CMS50E;

                if (aSerialPort == null)
                {
                    int baudrate = OximeterType == SupportedOximter.CMS60C ? 115200 : 19200;
                    aSerialPort = new SerialPort(ComPort, baudrate);
                    aSerialPort.ReadTimeout = 100;
                    try
                    {
                        Logger.Log("Open Port for " + OximeterType, 1);
                        aSerialPort.Open();
                        System.GC.SuppressFinalize(aSerialPort.BaseStream);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        MessageBox.Show("Can't open the Oximeter - is another application using it?");
                        aSerialPort = null;
                        throw e;
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show("Can't open the Oximeter - is it plugged in?\r\n" + e);
                        aSerialPort = null;
                        throw e;
                    }
                    if (OximeterType == SupportedOximter.CMS60C)
                    {
                        Write8Bytes(cms60cHandshake);
                    }
                }


                RealTimePacket aPacket = null;
                for (int i = 0; i < 10 && aPacket == null; i++)
                {
                    if (OximeterType == SupportedOximter.CMS60C)
                    {
                        aPacket = GetPacketFromCms60C();
                    }
                    else
                    {
                        aPacket = GetPacketFromCms50E();
                    }
                }

                if (aPacket == null)
                {
                    Logger.Log("Failure", 2);
                }
                return aPacket;
            }

            private static RealTimePacket GetPacketFromCms50E()
            {
                int[] buffer = null;
                int? headN = ReadByte();
                if (headN == null)
                {
                    Logger.Log("Got Null", 2);
                }

                int head = (int)headN;
                if (((head & 0x80) != 0x80))
                {
                    Logger.Log("Header byte no good " + (head & 0xb0).ToString("x") + " & 0x80 " + ((head & 0xf0)).ToString("x"), 2);
                    return null;
                }
                buffer = ReadBytes(4);
                if (buffer == null)
                {
                    Logger.Log("Could not read 4 byte packet", 2);
                    //no read
                    return null;
                }
                for (int j = 0; j < 4; j++)
                {
                    if ((buffer[j] & 0x80) == 0x80)
                    {
                        return null;
                    }
                }
                RealTimePacket aPacket = new RealTimePacket();
                aPacket.TimeStamp = DateTime.Now;
                aPacket.Pulse1 = buffer[0];
                aPacket.Pulse2 = buffer[1];
                aPacket.Pulse = (aPacket.Pulse2 << 8) | aPacket.Pulse1;
                aPacket.HeartRate = buffer[2];
                aPacket.SpO2 = buffer[3];
                //aPacket.isBeat = (head & 0x40) == 0x40;
                aPacket.isValid = true; //assume for now
                return aPacket;
            }

            private static RealTimePacket GetPacketFromCms60C()
            {
                int[] buffer = ReadBytes(9);
                if (buffer == null)
                {
                    if (OximeterType == SupportedOximter.CMS60C)
                    {
                        Write8Bytes(cms60cHandshake);
                    }
                    Logger.Log("Could not read 9 byte packet", 5);
                    return null;
                }

                if(buffer[0] != 0x01)
                {
                    Logger.Log("Header byte no good " + buffer[0].ToString("x2"), 5);
                    return null;
                }

                if (buffer[1] != 0xe0)
                {
                    Logger.Log("Finger Out " + buffer[1].ToString("x2"), 5);
                    return null;
                }


                RealTimePacket aPacket = new RealTimePacket();
                aPacket.TimeStamp = DateTime.Now;
                aPacket.Pulse1 = buffer[3];
                aPacket.Pulse2 = buffer[4];
                aPacket.Pulse = ((aPacket.Pulse2 & 0x0f) << 8) + aPacket.Pulse1;
                aPacket.HeartRate = buffer[5] & 0x7f;
                aPacket.SpO2 = buffer[6] & 0x7f;
                aPacket.isBeat = (buffer[2] & 0x40) == 0x40;
                aPacket.isValid = true; //assume for now
                return aPacket;
            }

            static public int? ReadByte()
            {
                try
                {
                    int result = aSerialPort.ReadByte();
                    Logger.Log("Read " + result.ToString("x2") + " " + result.ToString("d3") + " " + Convert.ToString(result, 2).PadLeft(8, '0'), 6);
                    return result;
                }
                catch (Exception e)
                {
                    Logger.Log("Exception reading " + e, 1);
                    return null;
                }
            }

            static private int[] ReadBytes(int size)
            {
                int[] result = new int[size];
                for (int i = 0; i < size; i++)
                {
                    int? abyte = ReadByte();
                    if (abyte == null)
                        return null;
                    result[i] = (int)abyte;
                }
                return result;
            }

            private static void Write8Bytes(byte[] buffer)
            {
                byte[] eightbytes = new byte[8];
                int i = 0;
                for (; i < buffer.Length; i++)
                {
                    eightbytes[i] = buffer[i];
                }
                for (; i < 8; i++)
                {
                    eightbytes[i] = 0x80;
                }
                try
                {
                    aSerialPort.Write(eightbytes, 0, eightbytes.Length);
                }
                catch (Exception e)
                {
                    Logger.Log("Exception writing " + e, 1);
                }

            }


            static public void Debug()
            {
                for (int j = 0; j < 30; j++)
                {
                    int? head = Packet.ReadByte();
                    if (head == null)
                        Logger.Log("Got [" + j.ToString("d2") + "] NULL ", 4);
                    else
                        Logger.Log("Got [" + j.ToString("d2") + "] " + Utils.ToString((int)head), 4);
                }
                Logger.Log("Done", 4);
            }
        }
        [Serializable]
        public class RealTimePacket : Packet
        {
            public DateTime TimeStamp;
            public int Pulse1;
            public int Pulse2;
            public int Pulse;
            public int HeartRate;
            public int SpO2;
            public bool isBeat;
            public bool isValid;
            public override String ToString()
            {
                return "DateTime " + TimeStamp + ", Pulse1 " + Pulse1 + ", Pulse2 " + Pulse2 + ", Pulse " + Pulse + ", HeartRate " + HeartRate + ", SpO2 " + SpO2 + ", isBeat " + isBeat + ", isValid " + isValid;
            }
        }
        [Serializable]
        public class RecordedTimePacket : Packet
        {
            public int Hours;
            public int Minutes;
            public int Seconds;
        }
        [Serializable]
        public class RecordedDataPacket : Packet
        {
            public int HeartRate;
            public int SpO2;
            public bool isValid;
            public int header;
        }
}
