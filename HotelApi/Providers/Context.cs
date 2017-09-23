using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgresEFCore.Providers
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Car>(entity => {
        }

        //public virtual DbSet<Car> Cars { get; set; }
    }
}
