using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EFTest.Models;

namespace EFTest
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("Conn")
        {

        }

        public DbSet<Cars > Carses { get; set; }

        public DbSet<Users > Userses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Users>()
            //    .HasOptional( c=>c.InterestCar ).WithOptionalDependent().Map(c=>c.MapKey("InterestCarId"));

         
            base.OnModelCreating(modelBuilder);
        }
    }
}