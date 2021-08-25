using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VisiflexAOSTUX.Models
{
    public class VisiflexContext : DbContext
    {
#if DEBUG
        public VisiflexContext() : base("DefaultDebug") { }
#else
        public VisiflexContext() : base("Default") { }
#endif
        static VisiflexContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        public DbSet<LaboralTask> LaboralTasks { get; set; }
        public DbSet<DocumentFile> DocumentFiles { get; set; }
        public DbSet<AttentionArea> AttentionAreas { get; set; }
        public DbSet<RequesterArea> RequesterAreas { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
    }
}