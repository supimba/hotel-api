using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using PostgresEFCore.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgresEFCore.Factories
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<Context>();

            builder.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=HotelManagement");
            return new Context(builder.Options);
        }
    }
}
