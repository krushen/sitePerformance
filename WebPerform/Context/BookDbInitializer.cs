using System.Data.Entity;
using WebPerform.Models;

namespace WebPerform
{
    public class BookDbInitializer : CreateDatabaseIfNotExists<ModelSiteContext>
    {
        //DropCreateDatabaseAlways 
        protected override void Seed(ModelSiteContext db)
        {
            base.Seed(db);
        }
    }
}