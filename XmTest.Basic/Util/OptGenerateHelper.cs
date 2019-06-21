//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.IO;
//using System.Text;
//using System.Threading;
//using System.Globalization;
//using GenerateOPT.Helpers;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using System.Drawing;
//using System.Diagnostics;
//using XmTest.temp;
//namespace GenerateOPT.Helpers
//{
//    public class OptGenerateHelper
//    {
//        /// <summary>
//        /// 仅生成Opt文件
//        /// </summary>
//        /// <param name="jsonData"></param>
//        /// <param name="fileName"></param>
//        /// <returns></returns>
//        public static bool GenerateOptFileOnly(string jsonData, string fileName)
//        {
//            string str = string.Empty;
//            //[OPT_Header]
//            str += GetOPT_Header();
//            //[OPT_Signature]
//            str += GetOPT_Signature();
//            List<ProClass> dataList = GetProClass(jsonData);
//            foreach (var item in dataList)
//            {
//                int index = dataList.IndexOf(item);//当前索引
//                if (index > 0 && index < dataList.Count)
//                {
//                    int lastIndex = (index - 1);
//                    ProClass plist = dataList[lastIndex];
//                }
//                str += SingleOPT_Pattern(item);//[OPT_Pattern]
//            }
//            //生成文件
//            //GenreateOPT(fileName, str);
//            //返回图片str
//            StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default);
//            sw.WriteLine(str);
//            sw.Flush();
//            sw.Close();
//            if (System.IO.File.Exists(fileName))
//            {
//                return true;
//            }
//            return false;
//        }

//        /// <summary>
//        /// 生成opt文件并返回pic字符串
//        /// </summary>
//        /// <param name="jsonData"></param>
//        /// <param name="fileName"></param>
//        /// <returns></returns>
//        public static List<List<string>> GenerateFileOPT(string jsonData, string fileName)
//        {
//            string str = string.Empty;
//            //[OPT_Header]
//            str += GetOPT_Header();
//            //[OPT_Signature]
//            str += GetOPT_Signature();
//            try
//            {
//                List<ProClass> dataList = GetProClass(jsonData);
//                ProClass plist = new ProClass();
//                foreach (var item in dataList)
//                {
//                    int index = dataList.IndexOf(item);//当前索引
//                    if (index > 0 && index < dataList.Count)
//                    {
//                        plist = dataList[(index - 1)];
//                    }
//                    str += SingleOPT_Pattern(item);//[OPT_Pattern]
//                }
//                //生成文件
//                GenreateOPT(fileName, str);
//                //返回图片str
//                return GetOPTpicStr(fileName);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message, ex.ToString());
//            }
//            return new List<List<string>>();
//        }
//        /// <summary>
//        /// 判断 Json集合/Json
//        /// </summary>
//        /// <param name="jsonStr"></param>
//        /// <returns></returns>
//        private static List<ProClass> GetProClass(string jsonStr)
//        {
//            if (jsonStr.IsNotNullOrEmpty())
//            {
//                if (jsonStr.Substring(0, 1) == "{")//单个数组
//                {
//                    return new List<ProClass> { (ProClass)JsonHelper.DeserializeJsonToObject<ProClass>(jsonStr) };
//                }
//                if (jsonStr.Substring(0, 1) == "[")//数组集合
//                {
//                    return (List<ProClass>)JsonHelper.DeserializeJsonToList<ProClass>(jsonStr);
//                }
//            }
//            return new List<ProClass>();
//        }
//        /// <summary>
//        /// 生成并获取OPTpic字符串
//        /// </summary>
//        /// <returns></returns>
//        public static List<List<string>> GetOPTpicStr(string fileName)
//        {
//            List<List<string>> strLists = new List<List<string>>();
//            Encoding encode6 = GetFileEncodeType(fileName);
//            StringBuilder strSb = new StringBuilder();
//            //读取文件
//            strSb.AppendFormat("{0} \r\n", System.IO.File.ReadAllText(fileName, encode6));
//            string str = strSb.ToString();
//            List<int> OPT_PatternAry = GetIndexStr(str, "[OPT_Pattern]");
//            List<ProClass> jsonList = new List<ProClass>();
//            List<string> strList = new List<string>();
//            foreach (var item in OPT_PatternAry)
//            {
//                string Strs = string.Empty;
//                if (item == OPT_PatternAry.Last())
//                    Strs = str.Substring(item, str.Length - item);
//                else
//                    Strs = str.Substring(item, OPT_PatternAry[OPT_PatternAry.IndexOf(item) + 1] - item);
//                jsonList = GetJCList(Strs);
//                strList = DrawHelpers.Drawing(jsonList);
//                strLists.Add(strList);
//            }
//            StreamWriter sw = new StreamWriter(fileName, false, encode6);//Encoding.GetEncoding("GB2312"));
//            sw.Write(strSb.ToString());
//            sw.Close();
//            return strLists;
//        }
//        /// <summary>
//        /// 获取ProClass集合
//        /// </summary>
//        /// <param name="str2"></param>
//        /// <returns></returns>
//        private static List<ProClass> GetJCList(string str2)
//        {
//            List<ProClass> jsList = new List<ProClass>();
//            ProClass jsclass = new ProClass
//            {
//                Width = int.Parse(StringHelper.Substr(str2, "Width=", "\r\nHeight")),
//                Height = int.Parse(StringHelper.Substr(str2, "Height=", "\r\nTrimLeft")),
//                ID = int.Parse(StringHelper.Substr(str2, "GlassID=", "\r\nGlassThickness")),
//                Ratio = int.Parse(StringHelper.Substr(str2, "Ratio=", "\r\nWidth"))
//            };
//            List<Info> InfoList = GetInfoList(str2);
//            //找到每一个X
//            str2 = str2.Substring(str2.IndexOf("[OPT_Pattern]"), str2.IndexOf("[Cuttings]") - str2.IndexOf("[OPT_Pattern]"));
//            List<int> xList = GetIndexStr(str2, "X");
//            //定位坐标点
//            List<int> positionX = new List<int>();
//            List<ProXY> ProxyList = new List<ProXY>();
//            foreach (var item in xList)
//            {
//                //截取每个X字符串集合
//                string zfc = string.Empty;
//                if (item == xList.Last())
//                    zfc = str2.Substring(item, str2.Length - item);
//                else
//                    zfc = str2.Substring(item, xList[xList.IndexOf(item) + 1] - item);
//                int X = GetXYZWC(zfc, EnumNum.X);
//                int xType = -1;//内矩形宽 0  外矩形宽 1    ProClass的宽 2
//                if (InfoList.Where(x => x.SheetWidth == X || x.SheetHeight == X).ToList().Count > 0)
//                    xType = 0;
//                else
//                    xType = 1;//外矩形宽
//                ProXY ProXY = new ProXY();
//                ///判断Y
//                List<int> yList = GetIndexStr(zfc, "Y");
//                List<int> positionY = new List<int>();
//                foreach (var yitem in yList)
//                {
//                    ProXY = new ProXY();
//                    //Y字符串
//                    string yStr = string.Empty;
//                    if (yitem == yList.Last())
//                        yStr = zfc.Substring(yitem, zfc.Length - yitem);
//                    else
//                        yStr = zfc.Substring(yitem, yList[yList.IndexOf(yitem) + 1] - yitem);
//                    if (xType == 0)
//                    {
//                        if (!yStr.Contains("Z"))//不包含Z X一致 Y一致
//                        {
//                            int CurrentX = 0, CurrentY = 0;
//                            if (positionX.Count < 1)
//                                CurrentX = X;
//                            else
//                                CurrentX = X + positionX.Max(x => x);
//                            ProXY.X = CurrentX;
//                            //if (yitem==yList.First())//yStr.Contains("Info")
//                            //    CurrentY = GetXyz(yStr, EnumNum.Y);
//                            //else
//                            //     CurrentY= ProxyList.Where(x => x.X == CurrentX).Max(x => x.Y) + GetXyz(yStr, EnumNum.Y);
//                            if (positionY.Count < 1)
//                                CurrentY = GetXYZWC(yStr, EnumNum.Y);
//                            else
//                                CurrentY = positionY.Max(x => x) + GetXYZWC(yStr, EnumNum.Y);
//                            ProXY.Width = X;
//                            ProXY.Y = CurrentY;
//                            ProXY.Height = GetXYZWC(yStr, EnumNum.Y);
//                            ProXY.Area = X * GetXYZWC(yStr, EnumNum.Y);
//                            ProXY.Number = int.Parse(StringHelper.Substr(yStr, "Info=", "\r\n"));//info
//                            ProxyList.Add(ProXY);
//                            positionY.Add(CurrentY);
//                            continue;
//                        }
//                        else//x不一致 Test
//                        {
//                            int indexX = 0;
//                            if (positionX.Count > 0)
//                                indexX = positionX.Max(x => x);
//                            if (yStr.Contains("W"))
//                            {
//                                //循环Z
//                                List<int> zList = GetIndexStr(yStr, "Z");
//                                foreach (var zitem in zList)
//                                {
//                                    string zStr = string.Empty;
//                                    if (zitem == zList.Last())
//                                        zStr = yStr.Substring(zitem, yStr.Length - zitem);
//                                    else
//                                        zStr = yStr.Substring(zitem, zList[zList.IndexOf(zitem) + 1] - zitem);

//                                    int iCurrentX = indexX + GetXYZWC(zStr, EnumNum.Z);
//                                    List<int> wList = GetIndexStr(zStr, "W");
//                                    foreach (var witem in wList)
//                                    {
//                                        ProXY = new ProXY();
//                                        string wStr = string.Empty;
//                                        if (witem == wList.Last())
//                                            wStr = zStr.Substring(witem, zStr.Length - witem);
//                                        else
//                                            wStr = zStr.Substring(witem, wList[wList.IndexOf(witem) + 1] - witem);

//                                        int iCurrentY = 0;
//                                        if (positionY.Count == 0)
//                                            iCurrentY = GetXYZWC(wStr, EnumNum.W);
//                                        else
//                                            iCurrentY = positionY.Max(x => x) + GetXYZWC(wStr, EnumNum.W);
//                                        ProXY.X = iCurrentX;
//                                        ProXY.Y = iCurrentY;
//                                        ProXY.Width = GetXYZWC(zStr, EnumNum.Z);
//                                        ProXY.Height = GetXYZWC(wStr, EnumNum.W);
//                                        ProXY.Area = GetXYZWC(zStr, EnumNum.Z) * GetXYZWC(wStr, EnumNum.W);
//                                        ProXY.Number = int.Parse(StringHelper.Substr(wStr, "Info=", "\r\n"));//info
//                                        ProxyList.Add(ProXY);
//                                        positionY.Add(iCurrentY);
//                                    }
//                                }
//                            }
//                            else
//                            {
//                                int CurrentY = 0;
//                                if (positionX.Count > 0)
//                                    ProXY.X = positionX.Max(x => x) + GetXYZWC(yStr, EnumNum.Z);
//                                else
//                                    ProXY.X = GetXYZWC(yStr, EnumNum.Z);
//                                if (positionY.Count < 1)
//                                    CurrentY = GetXYZWC(yStr, EnumNum.Y);
//                                else
//                                    CurrentY = positionY.Max(x => x) + GetXYZWC(yStr, EnumNum.Y);
//                                ProXY.Y = CurrentY;
//                                ProXY.Width = GetXYZWC(yStr, EnumNum.Z);
//                                ProXY.Height = GetXYZWC(yStr, EnumNum.Y);
//                                ProXY.Area = GetXYZWC(yStr, EnumNum.Z) * GetXYZWC(yStr, EnumNum.Y);
//                                yStr = yStr.Substring(yStr.IndexOf("Z="), yStr.Length - yStr.IndexOf("Z"));
//                                ProXY.Number = int.Parse(StringHelper.Substr(yStr, "Info=", "\r\n"));//info
//                                ProxyList.Add(ProXY);
//                                positionY.Add(CurrentY);
//                            }
//                            continue;
//                        }
//                    }
//                    else if (xType == 1) //外矩形宽
//                    {
//                        int indexX = 0, indexY = 0;
//                        if (positionX.Count > 0)
//                            indexX = positionX.Max(x => x);
//                        if (positionY.Count > 0)
//                            indexY = positionY.Max(x => x);
//                        indexY = indexY + GetXYZWC(yStr, EnumNum.Y);
//                        if (!yStr.Contains("W"))
//                        {
//                            List<int> zList = GetIndexStr(yStr, "Z");
//                            List<int> LesspositionX = new List<int>();
//                            foreach (var zitem in zList)
//                            {
//                                string zStr = string.Empty;
//                                if (zitem == zList.Last())
//                                    zStr = yStr.Substring(zitem, yStr.Length - zitem);
//                                else
//                                    zStr = yStr.Substring(zitem, zList[zList.IndexOf(zitem) + 1] - zitem);
//                                int iCurrentX = 0;
//                                if (zitem == zList.First())
//                                {
//                                    iCurrentX += indexX;
//                                }
//                                if (LesspositionX.Count > 0)
//                                    iCurrentX += LesspositionX.Max(x => x) + GetXYZWC(zStr, EnumNum.Z);
//                                else
//                                    iCurrentX += GetXYZWC(zStr, EnumNum.Z);
//                                ProXY = new ProXY
//                                {
//                                    X = iCurrentX,
//                                    Y = indexY,
//                                    Width = GetXYZWC(zStr, EnumNum.Z),
//                                    Height = GetXYZWC(yStr, EnumNum.Y),
//                                    Area = GetXYZWC(zStr, EnumNum.Z) * GetXYZWC(yStr, EnumNum.Y),
//                                    Number = int.Parse(StringHelper.Substr(zStr, "Info=", "\r\n"))
//                                };
//                                ProxyList.Add(ProXY);
//                                LesspositionX.Add(iCurrentX);
//                            }
//                            positionY.Add(indexY);
//                            continue;
//                        }
//                        else //循环Z
//                        {
//                            List<int> zList = GetIndexStr(yStr, "Z");
//                            foreach (var zitem in zList)
//                            {
//                                string zStr = string.Empty;
//                                if (zitem == zList.Last())
//                                    zStr = yStr.Substring(zitem, yStr.Length - zitem);
//                                else
//                                    zStr = yStr.Substring(zitem, zList[zList.IndexOf(zitem) + 1] - zitem);

//                                int iCurrentX = indexX + GetXYZWC(zStr, EnumNum.Z);
//                                List<int> wList = GetIndexStr(zStr, "W");
//                                foreach (var witem in wList)
//                                {
//                                    string wStr = string.Empty;
//                                    if (witem == wList.Last())
//                                        wStr = zStr.Substring(witem, zStr.Length - witem);
//                                    else
//                                        wStr = zStr.Substring(witem, wList[wList.IndexOf(witem) + 1] - witem);

//                                    int iCurrentY = 0;
//                                    if (positionY.Count == 0)
//                                        iCurrentY = GetXYZWC(wStr, EnumNum.W);
//                                    else
//                                        iCurrentY = positionY.Max(x => x) + GetXYZWC(wStr, EnumNum.W);
//                                    ProXY = new ProXY
//                                    {
//                                        X = iCurrentX,
//                                        Y = iCurrentY,
//                                        Width = GetXYZWC(zStr, EnumNum.Z),
//                                        Height = GetXYZWC(wStr, EnumNum.W),
//                                        Area = GetXYZWC(zStr, EnumNum.Z) * GetXYZWC(wStr, EnumNum.W),
//                                        Number = int.Parse(StringHelper.Substr(wStr, "Info=", "\r\n"))
//                                    };
//                                    ProxyList.Add(ProXY);
//                                    positionY.Add(iCurrentY);
//                                }
//                            }
//                            continue;
//                        }
//                    }
//                }
//                if (positionX.Count > 0)
//                    X = positionX.Max(x => x) + X;
//                positionX.Add(X);
//            }
//            jsclass.ProxyList = ProxyList;
//            jsList.Add(jsclass);
//            return jsList;
//        }
//        /// <summary>
//        /// 获取所有指定字符串的索引
//        /// </summary>
//        /// <param name="index"></param>
//        /// <returns></returns>
//        private static List<int> GetIndexStr(string str, string index)
//        {
//            List<int> ary = new List<int>();
//            if (str.IndexOf(index) == -1)
//                return new List<int>();
//            ary.Add(str.IndexOf(index));
//            for (int i = str.IndexOf(index) + 1; i <= str.LastIndexOf(index); i++)
//            {
//                i = str.IndexOf(index, i);
//                ary.Add(i);
//            }
//            return ary;
//        }
//        /// <summary>
//        /// 获取X/Y/Z/W/C对应值
//        /// </summary>
//        /// <param name="str"></param>
//        /// <param name="eNum"></param>
//        /// <returns></returns>
//        private static int GetXYZWC(string str, EnumNum eNum)
//        {
//            string xyzwc = string.Empty;
//            if (str.IsNotNullOrEmpty())
//            {
//                if (str.Contains("Info"))
//                {
//                    xyzwc = StringHelper.Substr(str, Enum.GetName(typeof(EnumNum), eNum) + "=", " Info");
//                    if (xyzwc.Contains("\r\n"))
//                        xyzwc = xyzwc.Substring(0, xyzwc.IndexOf("\r\n"));
//                }
//                else
//                    xyzwc = StringHelper.Substr(str, Enum.GetName(typeof(EnumNum), eNum) + "=", "\r\n ");
//            }
//            if (xyzwc.IsNullOrEmpty())
//                return 0;
//            return int.Parse(xyzwc);
//        }
//        /// <summary>
//        /// 根据字符串获取InfoList
//        /// </summary>
//        /// <param name="str"></param>
//        /// <returns></returns>
//        private static List<Info> GetInfoList(string str)
//        {
//            List<Info> infoList = new List<Info>();
//            Info info = new Info();
//            List<int> infoIndexAry = GetIndexStr(str, "[Info]");
//            foreach (var item in infoIndexAry)
//            {
//                string zfc = string.Empty;
//                if (item == infoIndexAry.Last())
//                    zfc = str.Substring(item, str.Length - item);
//                else
//                    zfc = str.Substring(item, infoIndexAry[infoIndexAry.IndexOf(item) + 1] - item);
//                info = new Info
//                {
//                    ID = int.Parse(StringHelper.Substr(zfc, "Id=", "\r\nRackNo")),
//                    SheetWidth = int.Parse(StringHelper.Substr(zfc, "SheetWidth=", "\r\nSheetHeight")),
//                    SheetHeight = int.Parse(StringHelper.Substr(zfc, "SheetHeight=", "\r\n\r\n"))
//                };
//                infoList.Add(info);
//            }
//            return infoList;
//        }
//        #region 生成OPT文件
//        /// <summary>
//        /// 生成[OPT_Header] 头部
//        /// </summary>
//        private static string GetOPT_Header()
//        {
//            OPT_Header opt_header = new OPT_Header
//            {
//                Name = "[OPT_Header]",
//                OPTCutVersion = "1.0",
//                Dimension = "mm",
//                Date = DateTime.Now.ToString("r", DateTimeFormatInfo.InvariantInfo)
//            };
//            string str = opt_header.Name.Rn();
//            str += "OPTCutVersion=" + opt_header.OPTCutVersion.Rn();
//            str += "Dimension=" + opt_header.Dimension.Rn();
//            str += "Date=" + opt_header.Date.Rn().Rn();
//            return str;
//        }
//        /// <summary>
//        /// 生成[OPT_Signature] 签名
//        /// </summary>
//        private static string GetOPT_Signature()
//        {
//            string str = string.Empty;
//            OPT_Signature opt_signature = new OPT_Signature
//            {
//                Name = "[OPT_Signature]",
//                Creator = "Aajubo H.S.K"
//            };
//            str += opt_signature.Name.Rn();
//            str += "Creator=" + opt_signature.Creator.Rn(2);
//            return str;
//        }
//        /// <summary>
//        /// 生成单个[OPT_Pattern] 部分
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        private static string SingleOPT_Pattern(ProClass data)
//        {
//            string str = string.Empty;
//            //[OPT_Pattern]
//            str += "[OPT_Pattern]".Rn();
//            str += "GlassID=" + data.ID.Rn();
//            str += "GlassThickness=" + "0.00".Rn();//玻璃厚度
//            str += "GlassCoated=" + "0".Rn();//是否涂层玻璃
//            str += "Pieces=" + "1".Rn();
//            str += "Ratio=" + data.Ratio.Rn();
//            str += "Width=" + data.Width.Rn();
//            str += "Height=" + data.Height.Rn();
//            str += "TrimLeft=" + "0.00".Rn();
//            str += "TrimBottom=" + "0.00".Rn();
//            //获取对应的X,Y,Z,W,V,A,B,C,D,E
//            #region 说明
//            /*默认X=Width，Y=Height 
//              最后一块需记录  
//               X=Width+边距长 Y=坐标为X的高度和【最大Y值】。
//               Z=Height   W=Width  
//               其他值类推
//             */
//            #endregion
//            List<int> Xlist = data.ProxyList.Where(x => (x.Y == x.Height)).Select(x => x.X).Distinct().ToList();///获取与X轴贴边长为0的所有X值                                                    
//            ///获取所有最大X坐标集合
//            List<int> allX = GetAllX(data, Xlist);
//            List<int> UsedX = new List<int> { 0 };
//            foreach (var CurrentX in allX)
//            {
//                List<ProXY> list = data.ProxyList.Where(x => (x.X <= CurrentX && x.X > UsedX.Last())).ToList();//等于CurrentX 或者位于两个  
//                //查询矩形种 类
//                int LastX = Xlist.Last();
//                List<int> WidthList = new List<int>();
//                List<int> xUsedList = new List<int>();
//                List<int> yUsedList = new List<int>();
//                EnumNum eNum = new EnumNum();
//                WidthList.Add(list.First().Width);
//                xUsedList.Add(list.First().X);
//                yUsedList.Add(list.First().Y);
//                foreach (var item in list)
//                {
//                    if (allX.Count != Xlist.Count)//非正常 X块 即x范围内与其他矩形交叉。
//                    {
//                        if (item != list.FirstOrDefault())
//                        {
//                            if (item.Y == yUsedList.Last())
//                            {
//                                str += ("    Z=" + item.Width + " Info=" + item.Number).Rn();
//                            }
//                            else
//                            {
//                                str += ("  Y=" + (item.Y - yUsedList.Last())).Rn();
//                                str += ("    Z=" + item.Width + " Info=" + item.Number).Rn();
//                            }
//                            WidthList.Add(item.Width);
//                            if (!xUsedList.Contains(item.X))
//                            {
//                                xUsedList.Add(item.X);
//                            }
//                            if (!yUsedList.Contains(item.Y))
//                            {
//                                yUsedList.Add(item.Y);
//                            }
//                            continue;
//                        }

//                        if (CurrentX != allX.FirstOrDefault())
//                        {
//                            if (CurrentX == allX.Last()) //上一个X
//                            {
//                                if (CurrentX != data.Width)
//                                    str += ("X=" + (data.Width - UsedX.Last())).ToString().Rn();
//                                else
//                                    str += ("X=" + (CurrentX - UsedX.Last())).ToString().Rn();
//                            }
//                            else
//                            {
//                                str += ("X=" + (CurrentX - UsedX.Last())).ToString().Rn();
//                            }
//                        }
//                        else
//                            str += ("X=" + CurrentX).Rn();
//                        if (item.Y == yUsedList.Last())
//                        {
//                            str += "  Y=" + item.Height.Rn();
//                            str += ("    Z=" + item.Width + " Info=" + item.Number).Rn();
//                        }
//                        else
//                        {
//                            str += ("  Y=" + item.Width).Rn();
//                            str += ("    Z=" + item.Height + " Info=" + item.Number).Rn();
//                        }
//                        WidthList.Add(item.Width);
//                        if (!xUsedList.Contains(item.X))
//                        {
//                            xUsedList.Add(item.X);
//                        }
//                        if (!yUsedList.Contains(item.Y))
//                        {
//                            yUsedList.Add(item.Y);
//                        }
//                        continue;
//                    }
//                    else//正常
//                    {
//                        if (item == list.FirstOrDefault())
//                        {
//                            if (CurrentX != LastX)
//                            {
//                                str += ("X=" + item.Width).Rn();
//                                str += ("  Y=" + item.Height + " Info=" + item.Number).Rn();
//                                eNum = EnumNum.Y;
//                            }
//                            else//最后一个
//                            {
//                                int MarginLong = data.Width - LastX + item.Width;
//                                str += ("X=" + MarginLong).Rn();
//                                if (CurrentX != data.Width)//最后一个元素
//                                {
//                                    int MaxY = list.Max(x => x.Y);//一种为保存其高度 另一种是保存data.Height
//                                    if (MaxY != data.Height)
//                                    {
//                                        if (Xlist.Count == 1)//两种情况 一种计算内矩形的高的和 另一种等于外矩形的高
//                                            str += "  Y=" + data.Height.Rn();
//                                        else
//                                            str += "  Y=" + MaxY.Rn();
//                                        str += ("    Z=" + item.Width).Rn();
//                                        str += ("      W=" + item.Height + " Info=" + list.FirstOrDefault().Number).Rn();//实际宽
//                                        eNum = EnumNum.W;
//                                    }
//                                    else
//                                    {
//                                        str += ("  Y=" + item.Width).Rn();
//                                        str += ("    Z=" + item.Height + " Info=" + list.FirstOrDefault().Number).Rn();
//                                        eNum = EnumNum.Z;
//                                    }
//                                }
//                                else
//                                {
//                                    if (item.Height == yUsedList.Last())
//                                    {
//                                        str += ("  Y=" + item.Height + " Info=" + item.Number).Rn();
//                                    }
//                                    else
//                                    {
//                                        str += ("  Y=" + item.Height).Rn();
//                                    }
//                                    eNum = EnumNum.Y;
//                                }
//                                WidthList.Add(item.Width);
//                                continue;
//                            }
//                        }
//                        else
//                        {
//                            if (CurrentX != LastX)
//                            {
//                                //只有一种时
//                                if (item.Width == WidthList.Last())//CurrentX!=UsedX.Last()
//                                {
//                                    str += ("  Y=" + item.Height + " Info=" + item.Number).Rn();
//                                    WidthList.Add(item.Width);
//                                    eNum = EnumNum.Y;
//                                    continue;
//                                }
//                                else//若与边距长为0的宽度不一致  
//                                {
//                                    if (CurrentX != UsedX.Last())
//                                    {
//                                        str += ("  Y=" + item.Height).Rn();
//                                        str += ("    Z=" + item.Width + " Info=" + item.Number).Rn();
//                                        WidthList.Add(item.Width);
//                                        continue;
//                                    }
//                                    str += ("X=" + item.Width).Rn();
//                                    str += ("  Y=" + item.Height + " Info=" + item.Number).Rn();
//                                    WidthList.Add(item.Width);
//                                    eNum = EnumNum.Y;
//                                }
//                            }
//                            else
//                            {
//                                string space = "", str2 = "";
//                                if (eNum == EnumNum.Y)
//                                {
//                                    str2 = "  Y=";
//                                    space = "";
//                                }
//                                if (eNum == EnumNum.Z)
//                                {
//                                    str2 = "    Z=";
//                                    space = "  ";
//                                }
//                                if (eNum == EnumNum.W)
//                                {
//                                    str2 = "      W=";
//                                    space = "    ";
//                                }
//                                if (item.Width != WidthList.Last())
//                                {
//                                    str += (space + Enum.GetName(typeof(EnumNum), (int)eNum - 1) + "=" + item.Width).Rn();
//                                }
//                                str += (str2 + item.Height + " Info=" + list.FirstOrDefault().Number).Rn();
//                                continue;
//                            }
//                        }
//                    }
//                }
//                UsedX.Add(CurrentX);
//            }
//            str += "\r\n";
//            //[Cuttings]
//            str += GetCutting(data);
//            //[Shape] 如果有内切 则有Shape
//            str += GetShape(data);
//            //[Info]
//            str += GetInfo(data);
//            return str;
//        }
//        /// <summary>
//        /// 获取所有最大X坐标集合
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="Xlist"></param>
//        /// <returns></returns>
//        private static List<int> GetAllX(ProClass data, List<int> Xlist)
//        {
//            List<int> allX = new List<int> { };
//            List<int> XList2 = new List<int>();
//            foreach (var item in Xlist)
//            {
//                if (XList2.Count > 0)
//                {
//                    if (item < XList2.Last())
//                        continue;
//                }
//                var obj = data.ProxyList.Where(x => x.X - x.Width < item && x.X > item).ToList();//获取部分位于此X之内的集合
//                if (obj.Count > 0)
//                {
//                    int firstX = obj.Where(x => x.X > item).Select(x => x.X).FirstOrDefault();//如果在此x范围内 有部分位于其中 找到第一个x
//                    if (item == Xlist.Last())
//                    {
//                        if (data.ProxyList.Where(x => x.X - x.Width < firstX && x.X > firstX).Count() > 0)
//                        {
//                            allX.Add(data.Width);
//                        }
//                        allX.Add(firstX);
//                    }
//                    //obj.Where(x => x.X > item).Select(x=>x.X).FirstOrDefault()
//                    XList2.Add(firstX);
//                    continue;
//                }
//                allX.Add(item);
//            }
//            if (allX.Count == 0)
//                allX.Add(data.Width);
//            return allX;
//        }
//        /// <summary>
//        /// 生成[Cutting]
//        /// </summary>
//        /// <param name="data">ProClass实体</param>
//        /// <returns></returns>
//        private static string GetCutting(ProClass data)
//        {
//            string str = string.Empty;
//            Cuttings cuttings = new Cuttings
//            {
//                Name = "[Cuttings]"
//            };
//            cuttings.itemList = new List<CuttingsItem>();
//            List<ProXY> ProxyList = new List<ProXY>();
//            //分割线 从（x,y)到（X，Y）
//            //查找第一条分割线 从上到下 从左到右进行分割'
//            //除去边界
//            List<int> XList = data.ProxyList.Where(x => x.X != data.Width).Select(x => x.X).OrderBy(x => x).Distinct().ToList();
//            ProxyList = new List<ProXY>();
//            foreach (var CurrentX in XList)
//            {
//                // x y X Y 为0则+1  为data.Width 或者data.Heigh 则-1
//                ProxyList = data.ProxyList.Where(x => x.X == CurrentX).ToList();
//                //单个竖切
//                CuttingsItem cutitem = new CuttingsItem();
//                int MinY = ProxyList.Min(x => x.Y);
//                int Height = ProxyList.Find(x => x.Y == MinY).Height;
//                int MaxY = ProxyList.Max(x => x.Y);
//                if (XList.IndexOf(CurrentX) % 2 == 0)
//                {
//                    cutitem.x = CurrentX;//CurrentX==0?CurrentX+1:CurrentX;
//                    cutitem.y = MinY - Height;//
//                    cutitem.X = CurrentX;
//                    cutitem.Y = MaxY;
//                }
//                else
//                {
//                    cutitem.x = CurrentX;//CurrentX==0?CurrentX+1:CurrentX;
//                    cutitem.y = MinY;//
//                    cutitem.X = CurrentX;
//                    cutitem.Y = MaxY - Height;
//                }
//                cuttings.itemList.Add(cutitem);
//            }
//            //除去边界
//            List<int> Ylist = data.ProxyList.Where(x => x.Y != data.Height).Select(x => x.Y).OrderBy(x => x).Distinct().ToList();///获取不重复的所有Y值
//            foreach (var item in Ylist)
//            {
//                ProxyList = data.ProxyList.Where(x => x.Y == item).ToList();
//                //单个横切
//                CuttingsItem cutitem = new CuttingsItem();
//                int MinX = ProxyList.Min(x => x.X);
//                int Width = ProxyList.Find(x => x.X == MinX).Width;
//                int maxX = ProxyList.Max(x => x.X);
//                if (Ylist.IndexOf(item) % 2 == 0)
//                {
//                    cutitem.x = MinX - Width;
//                    cutitem.y = item;
//                    cutitem.X = maxX;
//                    cutitem.Y = item;
//                }
//                else
//                {
//                    cutitem.x = maxX;
//                    cutitem.y = item;
//                    cutitem.X = MinX - Width;
//                    cutitem.Y = item;
//                }
//                cuttings.itemList.Add(cutitem);
//            }

//            str += cuttings.Name.Rn();
//            foreach (var item in cuttings.itemList)
//            {
//                str += "x=" + item.x + " y=" + item.y + " X=" + item.X + " Y=" + item.Y.Rn();
//            }
//            str += "\r\n";
//            return str;
//        }
//        /// <summary>
//        /// 处理边界值
//        /// </summary>
//        /// <param name="boilder">边</param>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        private static int GetValue(int boilder, int value)
//        {
//            if (value == boilder)
//                return value - 1;
//            else if (value == 0)
//                return 1;
//            else
//                return value;
//        }
//        /// <summary>
//        /// 获取Shape列表 注：当前未用到玻璃内切 返回空
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        private static string GetShape(ProClass data)
//        {
//            #region
//            //List<Shape> shapelist = new List<Shape>{
//            //    new Shape {
//            //    Name = "[Shape]",
//            //    ID = 1,
//            //    Description = "1",
//            //    itemList = new List<ShapeItem>
//            //    {
//            //        new ShapeItem{x=174.51F,y=0.00F,X=20.00F,Y=475.53F,C=1},
//            //        new ShapeItem{x=20.00F,y=475.51F,X=424.51F,Y=769.42F,C=1},
//            //        new ShapeItem{x=424.51F,y=769.42F,X=829.02F,Y=475.53F,C=1},
//            //        new ShapeItem{x=829.02F,y=475.53F,X=674.51F,Y=0.00F,C=1}
//            //    }}
//            //};
//            //foreach (var item in shapelist)
//            //{
//            //    str += item.Name.Rn();
//            //    str += "Id=" + item.ID.Rn();
//            //    str += "Description=" + item.Description.Rn();
//            //    foreach (var im in item.itemList)
//            //    {
//            //        str += "x=" + im.x + " y=" + im.y + " X=" + im.X + " Y=" + im.Y + " C=" + im.C.Rn();
//            //    }
//            //    str += "\r\n";
//            //}
//            #endregion
//            return string.Empty;
//        }
//        /// <summary>
//        /// 获取对应的Infol列表
//        /// </summary>
//        /// <param name="data">ProClass</param>
//        /// <returns></returns>
//        private static string GetInfo(ProClass data)
//        {
//            StringBuilder str = new StringBuilder();
//            List<int> NumberList = data.ProxyList.OrderBy(x => x.Number).Select(x => x.Number).Distinct().ToList();
//            ProXY ProXY = new ProXY();
//            foreach (var item in NumberList)
//            {
//                ProXY = data.ProxyList.FirstOrDefault(x => x.Number == item);
//                str.Append("[Info]".Rn());
//                str.Append("Id=" + item.ToString().Rn());
//                //str.Append("SecondGlassReference=".Rn());//第二层
//                str.Append("RackNo=" + "0".Rn());//分架数量
//                str.Append("Area=" + ProXY.Area.Rn());
//                str.Append("SheetWidth=" + ProXY.Height.Rn());
//                str.Append("SheetHeight=" + ProXY.Width.Rn(2));
//            }
//            return str.ToString();
//        }
//        #endregion

//        /// <summary>
//        /// 生成.OPT文件
//        /// </summary>
//        public static void GenreateOPT(string fileName, string content)
//        {
//            string newTxtPath = fileName; //"F://depth.opt";//创建txt文件的具体路径，我这里在选中的路径中创建名为depth的txt文件
//            StreamWriter sw = new StreamWriter(newTxtPath, false, Encoding.Default);//实例化StreamWriter
//            sw.WriteLine(content);
//            sw.Flush();
//            sw.Close();
//        }
//        /// <summary>
//        /// 获得文件编码格式的类
//        /// </summary>
//        /// <param name="extension"></param>
//        /// <returns></returns>
//        public static Encoding GetFileEncodeType(string filename)
//        {
//            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
//            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
//            Byte[] buffer = br.ReadBytes(2);
//            br.Close();
//            fs.Close();

//            if (buffer[0] >= 0xEF)
//            {
//                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
//                {
//                    return System.Text.Encoding.UTF8;
//                }
//                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
//                {
//                    return System.Text.Encoding.BigEndianUnicode;
//                }
//                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
//                {
//                    return System.Text.Encoding.Unicode;
//                }
//                else
//                {
//                    return System.Text.Encoding.Default;
//                }
//            }
//            else
//            {
//                return System.Text.Encoding.Default;
//            }
//        }
//    }
//    public enum EnumNum
//    {
//        X = 1,
//        Y = 2,
//        Z = 3,
//        W = 4,
//        V = 5,
//        A = 6,
//        B = 7,
//        C = 8,
//        D = 9,
//        E = 10
//    }


//}