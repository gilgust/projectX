using projectX.domain;
using System;
using System.Data.Entity;
using System.Linq;


namespace projectX.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=ApplicationContext"){}

        static ApplicationContext()
        {
            Database.SetInitializer(new DB_Init());
        }

        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<Proect> Proects{ get; set; }
    }
}