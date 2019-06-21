using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.Data.Entity;
using XmTest.IRepository;
using XmTest.Repository;
using XmTest.IRepository.sysBasic;
using XmTest.Repository.sysBasic;
using XmTest.Data.Factory;
namespace XmTest.Service.Basic
{
    public class NotesDAL
    {
        public static NotesDAL Instance
        {
            get
            {
                return DALFactory<NotesDAL>.Instance;
            }
        }

        private INotesRepository noteService = new NotesRepository();
        private IX_ClassifyRepository classifyService = new X_ClassifyRepository();



        public List<Notes> GetDatail(int userId = 1)
        {
            return noteService.GetList(x => x.UserID == userId);
        }


        /// <summary>
        /// 获取单个详情
        /// </summary>
        /// <param name="category"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Notes> GetclassifyDetail(string category, int userId)
        {
            X_Classify classify = classifyService.GetModel(x => x.Name == category.Trim() && x.UserID == userId);
            if (classify == null)
                return null;
            List<Notes> xcList = noteService.GetList(x => x.ClassifyID == classify.Id && x.UserID == userId);
            xcList.RemoveAll(x => x.Content == null);
            xcList.ForEach(x => x.Content = x.Content.Length > 100 ? x.Content.Substring(0, 200) + "..." : x.Content + "...");
            return xcList;
        }

        public Dictionary<string, string> GetClassifyfield(int userId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            classifyService.GetList(x =>x.UserID.Equals(userId)).ForEach(x =>
           {
               dic.Add(x.Id.ToString(), x.Name);
           });
            return dic;
        }

    }
}
