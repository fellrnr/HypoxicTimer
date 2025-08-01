using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO; //add reference to Microsoft.VisualBasic

namespace HypoxicTimer
{
    class Csv
    {
        public static List<Dictionary<string, string>> ReadCSVFile(string filename)
        {
            List<Dictionary<string, string>> retval = new List<Dictionary<string, string>>();

            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                string[] headers = parser.ReadFields();
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    Dictionary<string, string> rec = new Dictionary<string, string>();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (i < headers.Length)
                        {
                            string f = fields[i].Trim();
                            string h = headers[i].Trim();
                            rec.Add(h, f);
                        }
                    }
                    retval.Add(rec);
                }
            }

            return retval;
        }

    }
}
