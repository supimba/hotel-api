/* SETTING UP THE DATABASE FOR THE HOTEL RESERVATION SYSTEM
 *
 * This project will store data in a PostgreSQL database and it will use Entity Framework Core for
 * the ORM. EF Core acts as a bridge between the Hotel Reservation project and the database,
 * translating C# code into SQL commands and C# objects into records in the database, without
 * writing SQL.
 *
 * To facilitate communications between the program and a database, EF Core needs to know the:
 * 
 *      1) Data Provider and, (defined in Factories/MyDbContextFactory.cs)
 *      2) Data Model (defined in DbContext/Context.cs)
 *      3) Database Schema (Generated in the Migrations Folder)
 * 
 * This file defines a Data Model for the database.
 * 
 * 
 * DATA PROVIDERS
 *  
 * A Data Provider is a "a software library consisting of classes that provide data access 
 * services such as connecting to a data source, executing commands at a data source and fetching 
 * data from a data source with support to execute commands within transactions. It resides as a 
 * lightweight layer between data source and code, providing data access services with increased 
 * performance." (Source: https://www.techopedia.com/definition/25227/net-data-provider). 
 *  
 * EF Core can talk to any database if it has the data provider library for it. The Data Provider
 * for this project is the Npgsql.EntityFrameworkCore.PostgreSQL package, which was added via Nuget.
 * When HotelApi runs, Entity Framework will search for a Data Provider. EF Core will use the Data
 * Provider to determine that this project uses a PostgreSQL database. 
 *  
 * EF Core will use the Data Model to transform a SQL query into an object model representation
 * of a query (which is called a canonical command tree) and vice versa. .NET Data Providers can 
 * consume canonical command trees to talk the database.
 *
 * Go the Factories folder and open MyDbContextFactory.cs. The PostgreSQL data provider is defined 
 * in that file.
 * 
 * 
 * DATABASE DATA MODEL vs. DATABASE SCHEMA
 * 
 * In order to do two-way translations, the ORM library (EF Core) needs a Data Model of the
 * database. A Data Model is an abstract formalization of the entities in a database and their
 * relationships. It conceptually standardizes how entites relate to one another. In this file,
 * this is the Data Model for a Hotel table:
 *
 *
 *     public class Context : DbContext
 *     {
 *         public virtual DbSet<Hotel> Hotels { get; set; }
 *         protected override void OnModelCreating(ModelBuilder modelBuilder)
 *         {
 *             modelBuilder.Entity<Hotel>(entity =>
 *             {
 *                 entity.HasKey(e => e.Id);
 *                 entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
 *                 entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
 *                 entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
 *                 entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(100);
 *             });
 *         }
 *     }
 *         
 * A data structure is defined to hold the properties of the Hotel table, followed by definitions of 
 * these properties. HasKey() denotes the Id field as the Primary Key of a table. The other 
 * properties are defined with the Property() method. The Id, Name, Address and PhoneNumber fields
 * are marked as required fields. The character lengths of each field is also specified. This
 * defines a general, high-level set of constraints on Hotel tables in this database. 
 * 
 * The Data Model is conceptual framework that shapes and decides the contents of the Schema. A
 * Database Schema is a more concrete set of rules that govern the database. There are schemas for 
 * at least three levels of a database: physical schema (how data blocks are stored at the lowest
 * level; this is where database designers work), logical schema (how records get stored in data
 * structures; this level is where programmers and DB administrators work) and view schemas (which
 * are schemas for end-user interaction). Data Models are not intended to be easily changed, but
 * Schemas are.
 *
 * This is the Logical schema for the Hotel table: 
 *
 *     public partial class CreateHotelHotelRoomRoomTypeBedTypeRoomReservationTables : Migration
 *     {
 *         protected override void Up(MigrationBuilder migrationBuilder)
 *         {
 *             migrationBuilder.CreateTable(
 *                 name: "Hotels",
 *                 columns: table => new
 *                 {
 *                     Id = table.Column<long>(type: "int8", nullable: false)
 *                         .Annotation("Npgsql:ValueGenerationStrategy",
 *                                      NpgsqlValueGenerationStrategy.SerialColumn),
 *                     Address = table.Column<string>(type: "varchar(200)",
 *                                                    maxLength: 200,
 *                                                    nullable: false),
 *                     Name = table.Column<string>(type: "varchar(100)",
 *                                                 maxLength: 100,
 *                                                 nullable: false),
 *                     PhoneNumber = table.Column<string>(type: "varchar(100)",
 *                                                        maxLength: 100,
 *                                                        nullable: false)
 *                 },
 *                 constraints: table =>
 *                 {
 *                     table.PrimaryKey("PK_Hotels", x => x.Id);
 *                 });
 *         }
 *     }
 *
 * The Hotel entity defined the Data Model is explicitly defined as a Hotels Table in the schema.
 * This table's columns are given names defined in the Data Model. The schema goes further and
 * assigns types to each column, and other properties such as nullability and annotations. Note that
 * the character length of the columns are inherited from the Data Model.
 *
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
 *      (Source: https://stackoverflow.com/questions/25093452/difference-between-data-model-and-database-schema-in-dbms)
 *
 * Therefore, a Data Model is a general blueprint that defines some properties of the Schema. 
 *
 * 
 * Context.cs serves as a bridge between the model classes (classes that model database tables in 
 * the Models/Tables folder in the Common project) and database entities (records stored in the 
 * database).
 *
 *
 ***************************************************************************************************
 ***************************************************************************************************
 *
 * 
 * CONTEXT CLASS
 *
 * This class, Context, inherits from an EF Core class called Microsoft.EntityFrameworkCore.DbContext.
 * From the docs: "A DbContext instance represents a session with the database and can be used to
 * query and save instances of your entities." Put succintly, DbContext helps you talk to the
 * database. If EF Core is to act as a bridge, it must know the topology of the database. The
 * programmer must create Context.cs and set it to inherit from DbContext. Then, the programmer has
 * to outline certain information about the database in Context class.
 *
 * In this class, the programmer must use the Models classes (from project Common) to define the
 * Data Model of the database. 
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
        // This is list of Models that will be turned into Tables in the Database.
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<RoomReservation> RoomReservations { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<BedType> BedTypes { get; set; }
        
        
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
                entity.HasKey(e => e.ReservationId);
                entity.Property(e => e.ReservationId).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.HotelId).IsRequired();
                entity.Property(e => e.RoomNumber).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

                entity.HasOne(e => e.HotelRoom)
                    .WithMany(e => e.RoomReservations)
                    .HasForeignKey(e => new { e.RoomNumber, e.HotelId })
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
