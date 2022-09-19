using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; 

namespace WebApplication12.Models
{
    public class context:DbContext
    {
        public context():base("baglan")
        {
            Database.SetInitializer<context>(new CreateDatabaseIfNotExists<context>());
            Database.SetInitializer<context>(new DropCreateDatabaseIfModelChanges<context>());
        }
        public System.Data.Entity.DbSet<WebApplication12.Models.Kategori> kategoris { get; set; }

        public System.Data.Entity.DbSet<WebApplication12.Models.urunler> urunlers { get; set; }
        public System.Data.Entity.DbSet<WebApplication12.Models.login> girisler { get; set; }

 
    }


}