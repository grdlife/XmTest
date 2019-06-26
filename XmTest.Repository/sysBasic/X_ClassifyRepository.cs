using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmTest.Data.Entity;
using XmTest.Data.Repository;
using XmTest.IRepository;
using XmTest.IRepository.sysBasic;
using XmTest.Basic.ViewModel;
using XmTest.Basic.Util;
namespace XmTest.Repository.sysBasic
{
    public class X_ClassifyRepository : RepositoryBase<X_Classify>, IX_ClassifyRepository
    {
        private INotesRepository noteService = new NotesRepository();
        public bool EditNote(string str, int userId)
        {
            if (str.IsNullOrEmpty())
                return false;
            NoteMode note = JsonConvert.DeserializeObject<NoteMode>(str);
            Notes notes = new Notes
            {
                Id=note.Id,
                Title = note.Title,
                ClassifyID = note.ClassifyID,
                UserID = userId,
                Content = note.Content,
                iCon = note.iCon,
                Viewed = 0,
                ThumbUpCount = 0
            };


            X_Classify xc = this.GetModel(x => x.Id == note.ClassifyID);
            if (xc != null)
                xc.Count += 1;
            if(this.Update(xc))
                return noteService.Update(notes);
            return  false;
        }


        public bool AddNote(string str, int userId)
        {
            if (str.IsNullOrEmpty())
                return false;
            NoteMode note = JsonConvert.DeserializeObject<NoteMode>(str);
            Notes notes = new Notes
            {
                Title = note.Title,
                ClassifyID = note.ClassifyID,
                UserID = userId,
                Content = note.Content,
                iCon = note.iCon,
                Viewed = 0,
                ThumbUpCount = 0
            };
            X_Classify xc = this.GetModel(x => x.Id == note.ClassifyID);
            if (xc != null)
                xc.Count += 1;
            if (this.Update(xc))
               return noteService.Insert(notes);
            return false;
        }
    }
}
