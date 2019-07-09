using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XmTest.Basic.Helpers
{
    public class Log
    {

        public static void Write(Exception ex, string title, EnumHepler.LogLevel level = EnumHepler.LogLevel.Normal)
        {
            string logdir = AppDomain.CurrentDomain.BaseDirectory + "/Log/" + DateTime.Now.ToString("yyyyMM") + "/" + level.ToString();
            if (!Directory.Exists(logdir))
                Directory.CreateDirectory(logdir);
            string logFile = string.Format("{0}/{1}.txt", logdir, DateTime.Now.ToString("yyyyMMdd"));
            using (StreamWriter sw = new StreamWriter(logFile, true))
            {
                sw.WriteLine(string.Format("-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-{0}~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                sw.WriteLine("       title:" + title);
                sw.WriteLine("       msg:");
                sw.WriteLine("           " + ex.ToString());
                sw.WriteLine("-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~--~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~ end");
                sw.Close();
            }
        }

        public static void Write(Exception ex)
        {
            Write(ex, "", EnumHepler.LogLevel.Error);
        }
    }



}
