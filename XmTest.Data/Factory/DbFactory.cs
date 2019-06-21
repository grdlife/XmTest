using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using XmTest.Data.DBEntity;

namespace XmTest.Data.Factory
{
    public class DbFactory
    {
        public static XmDBConetext GetInstance() 
        {
            XmDBConetext dbcontext = CallContext.GetData("DbContext") as XmDBConetext;
            if(dbcontext==null)
            {
                dbcontext = new XmDBConetext();
                CallContext.SetData("DbContext", dbcontext);
            }
            return dbcontext;
        }
    }
}
