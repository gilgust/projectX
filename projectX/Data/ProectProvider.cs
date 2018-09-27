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
    public class ProectProvider : IProectCrud
    {
        private static ObservableCollection<Proect> _proect;
        public ObservableCollection<Proect> Proects
        {
            get
            {
                if (_proect != null) return _proect;
                var proectsList = GetProectsAsync().Result; 
                _proect = new ObservableCollection<Proect>(proectsList);
                return _proect;
            }
            set => _proect= value;
        } 

        public Proect AddProect(Proect newProect)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var mark in newProect.Marks)
                {
                    db.Marks.Attach(mark);
                }
                var a =  db.Proects.Add(newProect);
                db.SaveChanges();
                Proects.Add(a);
                return a;
            }
        }

        public void RemoveProect(Proect remProect)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var item = db.Cases.Find(remProect.Id);
                if (item == null) return;
                 
                db.Cases.Remove(item);
                db.SaveChanges();
            }
        }

        public void EditProect(Proect newProect)
        { 
            using (ApplicationContext db =new ApplicationContext())
            {
                foreach (var item in newProect.Marks)
                {
                    db.Marks.Attach(item);
                }
                 
                var proectFromDb = db.Proects.Include(c => c.Marks).First(p=> p.Id == newProect.Id);

                if (proectFromDb.Name != newProect.Name)
                    proectFromDb.Name = newProect.Name;

                if (proectFromDb.Description != newProect.Description)
                    proectFromDb.Description = newProect.Description;

//A skilled wordpress marks
                foreach (var mark in newProect.Marks)
                {
                    if(!proectFromDb.Marks.Exists(m => m.Id == mark.Id))
                        proectFromDb.Marks.Add(mark);
                }
                proectFromDb.Marks.RemoveAll(m => !newProect.Marks.Exists(m2 => m2.Id == m.Id));
                
                db.SaveChanges();
            }
        }

        public Proect GetProectById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            { 
                return db.Proects.Include(p=> p.Marks).FirstOrDefault(p => p.Id == id);
            }
        }

        private List<Proect> GetProects()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Proects.Include(c => c.Marks).Include(p => p.CaseResults).Load();
                var proectsList = db.Proects.ToList();
                return proectsList;
            }
        } 
        private async Task<List<Proect>> GetProectsAsync()
        {
            return await Task.Run(() => GetProects());
        }
    }
}
