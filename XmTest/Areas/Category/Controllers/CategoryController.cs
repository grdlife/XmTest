using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Controllers;
using XmTest.Models;
using Newtonsoft.Json;
using XmTest.Basic;
using XmTest.Basic.ViewModel;
using XmTest.IRepository.sysBasic;
using XmTest.Repository.sysBasic;
using XmTest.Basic.Util;
using XmTest.Data.Entity;
using XmTest.Utils;
using XmTest.Service.Basic;
namespace XmTest.Areas.Category
{
    public class CategoryController : BaseWebController
    {
        //
        // GET: /Category/Category/

        private IX_ClassifyRepository service = new X_ClassifyRepository();
         

        #region 分类页面
        /// <summary>
        /// 分类=>列表
        /// </summary>
        public ActionResult Index()
        {
            List<ClassifyMode> classify = service.GetList(x => x.UserID == loginId).Select(x => new ClassifyMode { Name = x.Name, Count = x.Count }).ToList();
            ViewBag.cfList = classify;
            return View();
        }

        /// <summary>
        /// 分类=>详情列表
        /// </summary>
        public ActionResult CategoryDetail(string category)
        {
            ViewBag.CategoryTitle = category;
            List<Notes> xcList = NotesService.Instance.GetclassifyDetail(category, loginId);
            return View(xcList);
        }

        /// <summary>
        /// 分类=>详情页
        /// </summary>
        public ActionResult Detail(int NoteID)
        {
            Notes note = noteService.GetModel(x => x.Id == NoteID);
            note.Content = note.Content!=null &&note.Content.IndexOf("<br />\r\n")>-1? note.Content.Replace("<br />\r\n", "<br />"):"";
            return View(note);
        }

        public ActionResult AddNote()
        {
            ViewBag.Classifys = Helpers.GetWebItems();
            return View();
        }

        /// <summary>
        /// 分类=>编辑=>富文本编辑器
        /// </summary>
        public ActionResult EditNote(int id = 0)
        {
            Notes note = noteService.GetModel(x => x.Id.Equals(id)) ?? new Notes();
            ViewBag.Classifys = Helpers.GetWebItems(note.ClassifyID.ToString());
            return View(note);
        }

        #endregion

        #region 分类方法
        [HttpPost]
        public ActionResult AddNote(string str)
        {
            if (str.IsNullOrEmpty())
                return Json(new { code = 0, msg = "请完善数据后再提交！" });
            bool result = service.AddNote(str, loginId);
            if (result)
                return Success("保存成功");
            else
                return Error("保存失败");
        }

        /// <summary>
        /// 分类=>内容编辑
        /// </summary>
        /// <param name="str">参数</param>
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditNote(string str)
        {
            if (str.IsNullOrEmpty())
                return Json(new { code = 0, msg = "请完善数据后再提交！" });
            bool result = service.EditNote(str, loginId);
            if (result)
                return Success("保存成功");
            else
                return Error("保存失败");
        }

      
        #endregion

        [HttpPost]
        public JsonResult GetData()
        {
            List<string> category = new List<string>{
                "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"
            };
            List<int> data = new List<int>{
              10, 52, 200, 334, 390, 330, 220
            };
            return Json(new { category = category, data = data });
        }

    }
}
