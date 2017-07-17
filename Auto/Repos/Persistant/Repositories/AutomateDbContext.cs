using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using testAdmin.Models;

namespace testAdmin.Persistant.Repositories
{
    public class AutomateDbContext : DbContext
    {
        public AutomateDbContext() : base("name=AutomateDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

    #region DbSets
        public virtual DbSet<Subject> Subs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<VisualRepresentation> VisualRepresantations  { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Characteristic> Characteristics { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        #endregion

    }
}