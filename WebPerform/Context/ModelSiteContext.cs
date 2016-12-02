namespace WebPerform
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ModelSiteContext : DbContext
    {

        public ModelSiteContext()
            : base("name=ModelSiteContext")

        {
            // Database.SetInitializer<ModelSiteContext>(new DropCreateDatabaseAlways<ModelSiteContext>());
            Database.SetInitializer(new BookDbInitializer());
        }

        public virtual DbSet<SiteInfo> Sites { get; set; }


    }
}

    