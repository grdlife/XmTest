﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmTest.Basic.Web
{
    public class Page
    {
        private string _sorttype = "Asc";

        /// <summary>
        /// 每页行数
        /// </summary>
        public int pagesize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int pageindex { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string sortcol { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string sorttype
        {
            get { return _sorttype; }
            set { this._sorttype = value; }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (records > 0)
                {
                    return records % this.pagesize == 0 ? records / this.pagesize : records / this.pagesize + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
