using System;
/********
 * 创建人:jrb
 * 创建日期: 2019-08-27
 * 创建内容:日志处理帮助类
 ********/
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XmTest.Basic.Helpers
{
    /// <summary>
    /// 日志处理帮助类
    /// </summary>
    public class LogHelper
    {

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="title">标题</param>
        public static void Write(Exception ex, string title)
        {
            Write(ex, title, EnumHepler.LogLevel.Error);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="title"></param>
        /// <param name="level"></param>
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
    }



}
