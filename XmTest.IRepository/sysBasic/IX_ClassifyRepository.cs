using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.Data.Entity;
using XmTest.Data.Repository;
namespace XmTest.IRepository.sysBasic
{
    public interface IX_ClassifyRepository : IRepositoryBase<X_Classify>
    {
        bool AddNote(string str, int userId);
        bool EditNote(string str, int userId);

       
    }
}
