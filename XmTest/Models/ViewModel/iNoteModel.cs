using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmTest.Models.ViewModel
{
    /// <summary>
    /// 文章编辑视图
    /// </summary>
    public class iNoteModel
    {
        /// <summary>
        /// 类别
        /// </summary>
        public int ClassifyID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public string iCon { get; set; }

    }
}