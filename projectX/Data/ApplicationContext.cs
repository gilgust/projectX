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
            Database.SetInitializer<ApplicationContext>(new DB_Init());
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Proect> Proects{ get; set; }
    }
}