using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HypoxicTimer
{
    class Utils
    {
        public static String ToString(int abyte)
        {
            return abyte.ToString("x2") + ", " + abyte.ToString("d3") + ", " + Convert.ToString(abyte, 2).PadLeft(8, '0');
        }

        public static string TimeSpanToString(TimeSpan ts)
        {
            string hr = ts.ToString();
            if (hr.IndexOf('.') > 0)
                hr = hr.Substring(0, hr.IndexOf('.'));
            return hr.Trim();
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.Dll")]
        static extern int PostMessage(IntPtr hWnd, UInt32 msg, int wParam, int lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        public static DialogResult ShowAutoClosingMessageBox(string message, string originalcaption, int time)
        {
            var timer = new System.Timers.Timer(time) { AutoReset = false };
            string caption = originalcaption + " (timeout in " + (time / 1000) + " seconds)";
            timer.Elapsed += delegate
            {
                IntPtr hWnd = FindWindowByCaption(IntPtr.Zero, caption);
                if (hWnd.ToInt32() != 0) PostMessage(hWnd, WM_CLOSE, 0, 0);
            };
            timer.Enabled = true;
            DialogResult dr = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return DialogResult.OK;
        }
        
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
