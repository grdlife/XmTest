using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.Models;
using XmTest.IService;
using XmTest.IRepository;
namespace XmTest.Service
{
    #region 用户
    public class X_UserService : BaseService<X_User>, IX_UserService
    {
        public X_UserService(IBaseRepository<X_User> baseRepository)
            : base(baseRepository)
        {
        }
    }
    public class X_RoleService : BaseService<X_Role>, IX_RoleService
    {
        public X_RoleService(IBaseRepository<X_Role> baseRepository)
            : base(baseRepository)
        {
        }
    }
    public class X_User_RoleService : BaseService<X_User_Role>, IX_User_RoleService
    {
        public X_User_RoleService(IBaseRepository<X_User_Role> baseReposity)
            : base(baseReposity)
        {
        }
    }
    #endregion
    #region 笔记
    public class NotesService : BaseService<Notes>, INotesService
    {
        public NotesService(IBaseRepository<Notes> baseRepository)
            : base(baseRepository)
        {
        }
    }
    public class X_ClassifyService : BaseService<X_Classify>, IX_ClassifyService
    {
        public X_ClassifyService(IBaseRepository<X_Classify> baseRepository)
            : base(baseRepository)
           
        {
        }

      
    }
    #endregion

}
