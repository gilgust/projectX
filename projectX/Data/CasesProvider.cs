using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;
using projectX.Data.interfaces;

namespace projectX.Data
{
    class CasesProvider : ICaseCrud
    {
        public CasesProvider()
        { }

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

        public ObservableCollection<string> Marks { get; set; }

        public Case AddCase(Case newCase)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var a =  db.Cases.Add(newCase);
                db.SaveChanges();
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
                var item = db.Cases.Find(newCase.Id);
                if (item == null) return;

                item = db.Cases.Include(c => c.Marks).Include(c => c.ImgSrc).First(c => c.Id == newCase.Id);

                if (item.Name != newCase.Name)
                    item.Name = newCase.Name;

                if (item.Description != newCase.Description)
                    item.Description = newCase.Description;

                item.Marks.RemoveAll(el => !newCase.Marks.Exists(el2 => el2.Id == el.Id));
                foreach (var mark in newCase.Marks)
                {
                    if(!item.Marks.Exists(el => el.Id == mark.Id))
                        item.Marks.Add(mark);
                }
                item.ImgSrc.RemoveAll(el => !newCase.ImgSrc.Exists(el2 => el2.Id == el.Id));
                foreach (var img in newCase.ImgSrc)
                {
                    if (!item.ImgSrc.Exists(el => el.Id == img.Id))
                        item.ImgSrc.Add(img);
                }

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Case GetCaseById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Cases.Include(c => c.Marks).Include(c => c.ImgSrc).FirstOrDefault(c => c.Id == id);
            }
        }

        private List<Case> GetCases()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Cases.Include(c => c.Marks).Include(c => c.ImgSrc).Load();
                var casesList = db.Cases.ToList();
                return casesList;
            }
        }

        

        private async Task<List<Case>> GetCasesAsync()
        {
            return await Task.Run(() => GetCases());
        }
    }
}
