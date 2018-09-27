using projectX.domain;
using System;
using System.Data.Entity;
using System.Linq;


namespace projectX.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=ApplicationContext-1223") {}

        static ApplicationContext()
        {
            //Database.SetInitializer(new DB_Init());
        }

        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<Proect> Proects{ get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<projectX.domain.Img> Imgs { get; set; }
        public virtual DbSet<CaseResult> CaseResults { get; set; }
    }
}