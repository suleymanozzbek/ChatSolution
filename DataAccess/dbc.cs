namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbc : DbContext
    {
        public dbc()
            : base("name=dbcEntities")
        {
        }

        public virtual DbSet<MvcMsg> MvcMsg { get; set; }
        public virtual DbSet<MvcUsers> MvcUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
