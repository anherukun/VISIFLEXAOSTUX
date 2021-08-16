using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;

namespace VisiflexAOSTUX.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))] public class VisiflexContext : DbContext
    {
#if DEBUG
        public VisiflexContext() : base("DefaultConnectionMySQL") { }
#else
        public VisiflexContext() : base("DefaultConnectionMySQL-PROD") { }
#endif
        static VisiflexContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.EntityFramework.MySqlEFConfiguration());
        }

        public DbSet<LaboralTask> LaboralTasks { get; set; }
    }
}