/* Note: This project uses PostgreSQL for the database and Entity Framework Core for the ORM. EF 
 * Core sits in between the program and the database, translating C# code into SQL commands and C# 
 * objects into records in the database. 
 * 
 * To facilitate communications between the program and a database, EF Core needs to know the:
 * 
 *      1) Data Provider and,
 *      2) Data Model (which is a Map to the layout of the entire Database).
 *  
 * DATA PROVIDERS
 *  
 * A Data Provider is a "a software library consisting of classes that provide data access 
 * services such as connecting to a data source, executing commands at a data source and fetching 
 * data from a data source with support to execute commands within transactions. It resides as a 
 * lightweight layer between data source and code, providing data access services with increased 
 * performance." (https://www.techopedia.com/definition/25227/net-data-provider). 
 *  
 * EF Core can talk to most databases if it has the data provider library for that database. The 
 * Data Provider for this project is the Npgsql.EntityFrameworkCore.PostgreSQL package. When 
 * this program runs, Entity Framework will search for a Data Provider. When it finds the  
 * PostgreSQL data provider, it will determine that this project uses a PostgreSQL database. 
 *  
 * EF Core will use a Data Model Map to transform a SQL query into an object model representation
 * of a query (which is called a canonical command tree) and vice versa. .NET Data Providers can 
 * consume canonical command trees to talk the database.
 * 
 * 
 * DATA MODEL
 * 
 * In order to do two-way translations, the ORM library (EF Core) needs to have a map of the 
 * database. The Data Model is a topological map of the whole database. The Data Model is a
 * list of all the tables in the database (Models folder) and their relationships. In this 
 * project, the Data Model is defined in a file called Context.cs in the DbContext folder. 
 * 
 * The Data Model is distinct from a Database Schema; see this StackOverflow Answer from 
 * richik jaiswal:
 * 
 *      A schema is a blueprint of the database which specifies what fields will be present and 
 *      what their types will be. For example an employee table will have an employee_ID column 
 *      represented by a string of 10 digits and an employee_Name column with a string of 45 
 *      characters.
 *      
 *      Data model is a high level design implementation which decides what can be present in the 
 *      schema. It provides a database user with a conceptual framework in which we specify the 
 *      database requirements of the database user and the structure of the database to fulfill 
 *      these requirements.
 *      
 *      A data model can, for example, be a relational model where the data will be organised in 
 *      tables whereas the schema for this model would be the set of attributes and their 
 *      corresponding domains.
 *      
 *      (Link: https://stackoverflow.com/questions/25093452/difference-between-data-model-and-database-schema-in-dbms)
 *
 * Context.cs serves as a bridge between the model classes (classes that model database tables in 
 * the Models > Tables folder in the Common project) and database entities (records stored in the 
 * database). 
 */

using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostgresEFCore.Providers
{
    // DbContext is an EF Core class.
    public class Context : DbContext
    {
        // This constructor calls the base constructor. "Initializes a new instance of the 
        // DbContext class using the specified options."
        public Context(DbContextOptions options) : base(options)
        {
            
        }


        // DECLARING DATA MODELS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Declare the Hotel Class. Also see the HotelRoom declaration.
            modelBuilder.Entity<Hotel>(entity =>
            {
                // Declaring columns in the Hotel Table. Primary Keys are declared with HasKey(). 
                // Other keys are declared with the Property keyword. 
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(100);
            });

            // Declare the HotelRoom Class. 
            modelBuilder.Entity<HotelRoom>(entity =>
            {
                // This is where the 'O' in ORM in defined.
                // Declare columns in the HotelRoom Table. Composite key is declared on the first
                // line.
                entity.HasKey(x => new { x.RoomNumber, x.HotelId });
                entity.Property(x => x.RoomNumber).IsRequired();
                entity.Property(x => x.HotelId).IsRequired();
                entity.Property(x => x.NightlyRate).IsRequired();
                entity.Property(x => x.NumberOfBeds).IsRequired();
                entity.Property(x => x.RoomTypeId).IsRequired();
                entity.Property(x => x.BedTypeId).IsRequired();

                // This is where the 'R' in ORM in defined. Here, the relationship between Hotel
                // and HotelRoom is declared. 
                entity.HasOne(e => e.Hotel)             // HotelRoom is declared to be owned by Hotel objects.
                    .WithMany(e => e.HotelRooms)        // Hotels will have many HotelRooms.
                    .HasForeignKey(e => e.HotelId)      // Hotels have the HotelId foreign key.
                    .OnDelete(DeleteBehavior.Cascade);  // Deletes any dependent entities, so any row that
                                                        // has a foreign key to the deleted entity. Delete
                                                        // a hotel and all its hotels rooms will be deleted
                                                        // as well.
            });

            modelBuilder.Entity<RoomReservation>(entity => {
                entity.HasKey(e => new { e.RoomId, e.HotelId, e.StartDate, e.EndDate });
                entity.Property(e => e.HotelId).IsRequired();
                entity.Property(e => e.RoomId).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

                entity.HasOne(e => e.HotelRoom)
                    .WithMany(e => e.RoomReservations)
                    .HasForeignKey(e => new { e.RoomId, e.HotelId })
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BedType>(entity => {
                entity.HasKey(e => new { e.Id, e.Name });
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<RoomType>(entity => {
                entity.HasKey(e => new { e.Id, e.Name });
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
            });
        }

        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<RoomReservation> RoomReservations { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<BedType> BedTypes { get; set; }

        public void FixState()
        {
            foreach (var entry in ChangeTracker.Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = entry.Entity;
                entry.State = ConvertState(stateInfo.State);
            }
        }

        public static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
