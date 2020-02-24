namespace BannerGrabbing.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BGModel : DbContext
    {
        public BGModel()
            : base("name=BGModel")
        {
        }

        public virtual DbSet<TIp> TIp { get; set; }
        public virtual DbSet<TPortStatus> TPortStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
