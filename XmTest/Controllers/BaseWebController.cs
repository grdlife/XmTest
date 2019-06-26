using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Models;
using Newtonsoft.Json;
using XmTest.Basic.Util;
using XmTest.Basic.Web;
using XmTest.IRepository.sysBasic;
using XmTest.Repository.sysBasic;
namespace XmTest.Controllers
{
    public class BaseWebController : Controller
    {
        public static int loginId { get; set; }

        #region[        调用服务对象    ]
        public IX_UserRepository userService = new X_UserRepository();
        public IX_RoleRepository roleSerivce = new X_RoleRepository();
        public IX_User_RoleRepository use_roleService = new X_User_RoleRepository();

        public INotesRepository noteService = new NotesRepository();
        public IX_ClassifyRepository classifyService = new X_ClassifyRepository();
        public ICommentRepository commentService = new CommentRepository();

        public IX_AlbumRepository albumService = new X_AlbumRepository();
        public IX_AlbumTypeRepository  albumTypeService= new X_AlbumTypeRepository();

        public IX_DiaryRepository diaryService = new X_DiaryRepository();
        #endregion

        //
        // GET: /BaseWeb/
        public BaseWebController()
        {
            loginId = GetLoginId();
            ViewBag.user = GetLoginName();
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public static string GetLoginName()
        {
            UserLoginModel iuser = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;
            if (iuser != null)
                return iuser.LoginName ?? string.Empty;
            else
                return string.Empty;

        }
        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public static int GetLoginId()
        {
            UserLoginModel iuser = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;
            if (iuser != null)
                return iuser.LoginId;
            else
                return 0;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="httpcontextbase"></param>
        /// <returns></returns>
        public static UserLoginModel GetUserInfo(HttpContextBase httpcontextbase)
        {
            UserLoginModel iuser = new UserLoginModel();
            iuser.LoginName = httpcontextbase.Request.Cookies[WebContent.UserSession] == null ? null : httpcontextbase.Request.Cookies[WebContent.UserSession].Value;
            return iuser;
        }

        protected virtual ActionResult Success(string msg)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), msg = msg }.ToJson());
        }

        protected virtual ActionResult Success(string msg, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), msg = msg, data = data }.ToJson());
        }

        protected virtual ActionResult Error(string msg)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), msg = msg }.ToJson());
        }

    }
}
