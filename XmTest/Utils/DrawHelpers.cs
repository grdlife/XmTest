using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using AtjuboSaaS.Common.ViewModel.TaskOverPrintVM;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
//using GenerateOPT.Models;
using XmTest.temp;
namespace XmTest.Utils
{
    public class DrawHelpers
    {
          /// <summary>
        /// 套版图
        /// </summary>
        /// <param name="str">套版数据（JSON）</param>
        /// <returns></returns>
        public static List<string> Drawing(List<ProClass> modelList)
        {
            var data = new List<string>();
            foreach (var item in modelList)
            {
                ProXY yuanpian = new ProXY();//原片
                var GlassList = item.ProxyList;
                yuanpian.Height = item.Height;
                yuanpian.Width = item.Width;
                Bitmap bMap = new Bitmap((int.Parse(yuanpian.Width.ToString()) / 5), (int.Parse(yuanpian.Height.ToString()) / 5));//画图初始化     + GlassList.Count * x       + GlassList.Count * x
                Graphics g = Graphics.FromImage(bMap);
                g.SmoothingMode = SmoothingMode.AntiAlias;//消除绘制图形的锯齿
                Rectangle rect1 = new Rectangle(new Point(0, 0), new Size((int.Parse(yuanpian.Width.ToString()) / 5) + 20, (int.Parse(yuanpian.Height.ToString()) / 5) + 20));//设置绘制区域
                SolidBrush br11 = new SolidBrush(Color.DeepSkyBlue);
                g.FillRectangle(br11, rect1);
                g.Clear(Color.FromArgb(174, 223, 225));//以白色清空panel1控件的背景
                Pen myPen = new Pen(Color.Black, 1);//设置画笔的颜色
                Font font = new System.Drawing.Font("Arial", 10, (System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Italic));
                Font font2 = new System.Drawing.Font("Arial", 7, (System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Italic));
                for (int i = 0; i < GlassList.Count; i++)
                {
                    Point point = new Point();

                    point.X = (int.Parse(GlassList[i].X.ToString("F0")) - int.Parse(GlassList[i].Width.ToString("F0"))) / 5;
                    point.Y = (int.Parse(GlassList[i].Y.ToString("F0")) - int.Parse(GlassList[i].Height.ToString("F0"))) / 5;
                    Rectangle rect = new Rectangle(point, new Size(int.Parse(GlassList[i].Width.ToString("F0")) / 5, int.Parse(GlassList[i].Height.ToString("F0")) / 5));//设置绘制区域
                    SolidBrush br1 = new SolidBrush(Color.DeepSkyBlue);
                    g.FillRectangle(br1, rect);
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), Color.White, Color.White, 0f, true);
                    g.DrawString(GlassList[i].Width + "*" + GlassList[i].Height, font, brush, point.X + 2, point.Y + 2);
                    g.DrawString(GlassList[i].Number + "", font, brush, rect.Width / 2 - (GlassList[i].Number.ToString().Length * 3) + rect.X, rect.Height / 2 - 5 + rect.Y);
                    g.DrawEllipse(new Pen(Color.White, 1), rect.Width / 2 - (GlassList[i].Number.ToString().Length * 3) - 2 + rect.X, rect.Height / 2 - (GlassList[i].Number.ToString().Length * 2) - 2 + rect.Y, GlassList[i].Number.ToString().Length * 5 + 10, GlassList[i].Number.ToString().Length * 5 + 10);
                    g.DrawRectangle(myPen, rect); //绘制
                }
                g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(0, 0, (int.Parse(yuanpian.Width.ToString()) / 5 - 1), (int.Parse(yuanpian.Height.ToString())/ 5 - 1))); //绘制边框+ GlassList.Count * x+ GlassList.Count * x

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                bMap.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                g.Dispose();
                bMap.Dispose();
                var aa = Convert.ToBase64String(ms.ToArray());
                //var aa = File(ms.ToArray(), @"iamge/Jpeg");
                data.Add(aa);

            }
            return data;
        }
        /// <summary>
        /// 套版图片保存
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        public static void savetxt(string input, string tid, string uid)
        {
            string LOG_DIR = (AppDomain.CurrentDomain.BaseDirectory + "bin");
            string path = string.Concat(new object[] { LOG_DIR, @"\ClassOverprint\" + tid + @"\" + uid, ".txt" });
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path.Substring(0, path.LastIndexOf(@"\")));
            di.Create();

            System.IO.StreamWriter writer = null;
            if (!System.IO.File.Exists(path))
            {
                using (System.IO.File.Create(path))
                {
                }
            }
            try
            {
                writer = new System.IO.StreamWriter(path, false, System.Text.Encoding.Default);
                writer.WriteLine(input);
                writer.Flush();
            }
            catch
            {
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }

        }
        /// <summary>
        /// 套版图片读取
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string readtxt(string tid, string uid)
        {
            string LOG_DIR = (AppDomain.CurrentDomain.BaseDirectory + "bin");
            string path = string.Concat(new object[] { LOG_DIR, @"\ClassOverprint\" + tid + @"\" + uid, ".txt" });
            string reslut = "";
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                reslut = sr.ReadToEnd();
                fs.Dispose();
                sr.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return reslut;
        }
    }

    #region 前台调用
    /*
         <link href="../../Contents/css/boxImg.css" rel="stylesheet" />
    <div>
        <a href="../Home/GenerateFileOPT"><input type="button" class="btn" value="生成OPT" id="btnok" /></a>
    </div>
    <div>
        @if (ViewBag.str != null)
        {
            foreach (var item in ViewBag.str)
            {
                foreach (var im in item)
                {
                    <img modal="zoomImg" class="responsive zoom-img" style="width:24%;margin:1px 1px 1px 1px" src="data:image/jpeg;base64,@im" name="picClass">
                }
            }
        }
    </div>
    <script src="../../Contents/js/boxImg.js"></script>
    <!--放置容器-->
    <div style="text-align:center;margin:50px 0; font:normal 14px/24px 'MicroSoft YaHei';color:#FFFFFF;">
        <p></p>
        <p></p>
    </div>
   */
    #endregion
}

