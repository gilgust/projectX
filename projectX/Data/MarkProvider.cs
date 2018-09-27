using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;
using projectX.Data.interfaces;

namespace projectX.Data
{
    class MarkProvider: IMarkCrud
    {
        private static List<Mark> _marks;

        public List<Mark> Marks
        {
            get
            {
                if (_marks != null) return _marks;

                return _marks = GetMarks();
            }
        }

        public Mark AddMark(string newMark)
        { 
            if (Marks.Exists((m) => m.Text == newMark))
                return Marks.First(m => m.Text == newMark);

            using (ApplicationContext db = new ApplicationContext())
            {
                var mark=  db.Marks.Add(new Mark{Text = newMark});
                Marks.Add(mark);
                db.SaveChanges();
                return mark;
            }

        } 


        private List<Mark> GetMarks()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Marks.Load();
                return db.Marks.ToList();
            }
        }

        private async Task<List<Mark>> GetMarksAsync()
        {
            return await Task.Run(() => GetMarks());
        }
    }
}
