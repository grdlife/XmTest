using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmTest.Basic.Web
{
    public class AjaxResult
    {
        public string state { get; set; }

        public string msg { get; set; }

        public object data { get; set; }
    }

    public class OutMsg
    {
        /// <summary>
        /// -1 失败    1  成功
        /// </summary>
        public int code { get; set; }


        public string msg { get; set; }


        public object data { get; set; }
    }



    public enum ResultType
    {
        success,
        info,
        error,
        warning,
    }
}
