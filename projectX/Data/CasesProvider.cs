using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using projectX.domain;
using projectX.Data.interfaces;

namespace projectX.Data
{
    class CasesProvider : ICaseCrud
    {
        private static ObservableCollection<Case> _cases;
        public ObservableCollection<Case> Cases
        {
            get
            {
                if (_cases != null) return _cases;
                var lc = GetCasesAsync().Result;

                _cases = new ObservableCollection<Case>(lc);
                return _cases;
            }
            set => _cases = value;
        } 

        public Case AddCase(Case newCase)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var mark in newCase.Marks)
                {
                    db.Marks.Attach(mark);
                }
                var a =  db.Cases.Add(newCase);
                db.SaveChanges();
                Cases.Add(a);
                return a;
            }
        }

        public void RemoveCace(Case remCase)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var item = db.Cases.Find(remCase.Id);
                if (item == null) return;
                 
                db.Cases.Remove(item);
                db.SaveChanges();
            }
        }

        public void EditCase(Case newCase)
        { 
            using (ApplicationContext db =new ApplicationContext())
            {
                foreach (var item in newCase.Marks)
                {
                    db.Marks.Attach(item);
                }
                 
                var caseFromDb = db.Cases.Include(c => c.Marks).Include(c => c.ImgSrc).First(c => c.Id == newCase.Id);

                if (caseFromDb.Name != newCase.Name)
                    caseFromDb.Name = newCase.Name;

                if (caseFromDb.Description != newCase.Description)
                    caseFromDb.Description = newCase.Description;

//A skilled wordpress marks
                foreach (var mark in newCase.Marks)
                {
                    if(!caseFromDb.Marks.Exists(m => m.Id == mark.Id))
                        caseFromDb.Marks.Add(mark);
                }
                caseFromDb.Marks.RemoveAll(m => !newCase.Marks.Exists(m2 => m2.Id == m.Id));

//add and delete img
                foreach (var img in newCase.ImgSrc)
                {
                    if(img.Id == 0)
                        caseFromDb.ImgSrc.Add(img);
                } 

                var remodeImgList = new List<Img>();
                foreach (var img in caseFromDb.ImgSrc)
                {
                    if (!newCase.ImgSrc.Exists(i => i.Id == img.Id))
                    {
                        remodeImgList.Add(img);
                    }
                }
                db.Imgs.RemoveRange(remodeImgList);


                db.SaveChanges();
            }
        }

        public Case GetCaseById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Cases.Include(c => c.Marks).Include(c => c.ImgSrc).Include(c => c.CaseResults).FirstOrDefault(c => c.Id == id);
            }
        }

        public List<Case> GetCases()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Cases.Include(c => c.Marks).Include(c => c.ImgSrc).Load();
                var casesList = db.Cases.ToList();
                return casesList;
            }
        } 
        public async Task<List<Case>> GetCasesAsync()
        {
            return await Task.Run(() => GetCases());
        }
    }
}
