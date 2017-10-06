using Common.Models;
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
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.HasKey(x => new { x.RoomNumber, x.HotelId });
                entity.Property(x => x.RoomNumber).IsRequired();
                entity.Property(x => x.HotelId).IsRequired();
                entity.Property(x => x.NightlyRate).IsRequired();
                entity.Property(x => x.NumberOfBeds).IsRequired();
                entity.Property(x => x.RoomTypeId).IsRequired();
                entity.Property(x => x.BedTypeId).IsRequired();

                entity.HasOne(e => e.Hotel)
                    .WithMany(e => e.HotelRooms)
                    .HasForeignKey(e => e.HotelId)
                    .OnDelete(DeleteBehavior.Cascade);
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
