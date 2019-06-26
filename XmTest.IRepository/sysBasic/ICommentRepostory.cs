using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.Data.Entity;
using XmTest.Data.Repository;

namespace XmTest.IRepository.sysBasic
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
    }
}
