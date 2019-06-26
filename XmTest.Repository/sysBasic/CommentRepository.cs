using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.Data.Entity;
using XmTest.Data.Repository;
using XmTest.IRepository.sysBasic;

namespace XmTest.Repository.sysBasic
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
    }
}
