/* Note: The Factories folder holds a single 'factory' file which creates the Database Context.
 *
 * This file, MyDbContextFactory.cs, tells the Entity Framework Core ORM that this project is using 
 * PostgreSQL for the database via a Data Provider package. This class passes the connection string,
 * hostname, username, database password and database name to EF Core. Edit this file to direct
 * Entity Framework to recognize other databases.
 */


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

            // This line specifies that the Data Provider is Npgsql.EntityFrameworkCore and passes
            // in the hostname, username, password and database name.
            builder.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=HotelManagement");
            return new Context(builder.Options);
        }
    }
}
