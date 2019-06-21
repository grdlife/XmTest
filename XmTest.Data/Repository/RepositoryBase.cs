using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.Data.DBEntity;
using XmTest.Data.Factory;
namespace XmTest.Data.Repository
{
    public class RepositoryBase : IRepositoryBase, IDisposable
    {
        private XmDBConetext _dbContext = DbFactory.GetInstance();

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
    }
}
