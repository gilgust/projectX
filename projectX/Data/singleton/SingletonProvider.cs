using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;
using projectX.Data.interfaces;

namespace projectX.Data.singleton
{ 
    public class SingletonProvider : ICaseCrud, IDisposable
    {
        private ApplicationContext db;

        private SingletonProvider()
        {
            db = new ApplicationContext();

            db.Cases.Include(c => c.Marks).Load();
            db.Marks.Load();
            Cases = db.Cases.Local;
        }
        
        public ObservableCollection<Case> Cases { get; set; }
        public ObservableCollection<string> Marks { get; set; }

        #region CaseCrud 
        public Case AddCase(Case newCase)
        {
            var c = db.Cases.Add(newCase);
            db.SaveChanges();
            return c;
        }

        public void RemoveCace(Case remCase)
        {
            var item = db.Cases.Find(remCase.Id);
            if (item == null) return;

            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges(); 
        }

        public void EditCase(Case newCase)
        {
            db.Entry(newCase).State = EntityState.Modified;
            db.SaveChanges();
        }

        #endregion

        #region singleton
        private static readonly Lazy<SingletonProvider> Lazy =
            new Lazy<SingletonProvider>(() => new SingletonProvider());

        public static SingletonProvider Instance => Lazy.Value;
        #endregion

        #region Dispose
        public void Dispose()
        {
            db?.Dispose();
        }
        #endregion
    }
}
