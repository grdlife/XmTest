using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using XmTest.Service;
using XmTest.IRepository;
using XmTest.IRepository.sysBasic;
using XmTest.Controllers;
using XmTest.Data.Entity;
namespace XmTest.Areas.Diary.Controllers
{

    public class DiaryController : BaseWebController
    {
        //日记
        // GET: /Diary/Diary/
        
        public ActionResult Index()
        {
           List<X_Diary> result = diaryService.GetList(x=>x.UserId==loginId);
           return View(result);
        }


        public ActionResult Detail(int id)
        {
            X_Diary result = diaryService.GetModel(x => x.Id == id);
            return View(result);
        }

        public ActionResult DiaryAdd()
        {
            return View();
        }

        public ActionResult DiaryEdit()
        {
            return View();
        }

    }
}
