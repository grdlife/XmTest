using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmTest.temp
{
    public class ModelHelper
    {
    }

    /// <summary>
    /// 原片
    /// </summary>
    public class ProClass
    {
        public int Height { set; get; }
        public int Width { set; get; }
        public double Ratio { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// 成品集合
        /// </summary>
        public List<ProXY> ProxyList { set; get; }

        public string GlassName { get; set; }

        /// <summary>
        /// 掰片距离
        /// </summary>
        public int baipianMargin { get; set; }
        /// <summary>
        /// 修片距离
        /// </summary>
        public int xiupianMargin { get; set; }
    }
    /// <summary>
    /// 成品
    /// </summary>
    public class ProXY
    {
        public int X { set; get; }
        public int Y { set; get; }
        public int Height { set; get; }
        public int Width { set; get; }
        public int Number { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public int Area { get; set; }
        public string GlassName { get; set; }
    }
}