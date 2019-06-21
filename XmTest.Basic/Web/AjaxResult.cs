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

    public enum ResultType
    {
        success,
        info,
        error,
        warning,
    }
}
