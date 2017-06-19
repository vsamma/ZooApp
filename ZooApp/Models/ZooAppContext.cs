using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class ZooAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ZooAppContext() : base("name=ZooAppContext")
        {
        }

        public System.Data.Entity.DbSet<ZooApp.Models.Species> Species { get; set; }

        public System.Data.Entity.DbSet<ZooApp.Models.Animal> Animals { get; set; }
    }
}
