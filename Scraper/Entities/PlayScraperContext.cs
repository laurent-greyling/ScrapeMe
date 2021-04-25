using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Scraper.Entities
{
    public partial class PlayScraperContext : DbContext
    {
        public PlayScraperContext()
        {
        }

        public PlayScraperContext(DbContextOptions<PlayScraperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cast> Cast { get; set; }
        public virtual DbSet<Shows> Shows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DbConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cast>(entity =>
            {
                entity.HasKey(e => e.CastShowId);

                entity.ToTable("Cast", "Tv");

                entity.Property(e => e.CastShowId).HasMaxLength(50);

                entity.Property(e => e.Birthday).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.Cast)
                    .HasForeignKey(d => d.ShowId)
                    .HasConstraintName("FK_Cast_Shows");
            });

            modelBuilder.Entity<Shows>(entity =>
            {
                entity.ToTable("Shows", "Tv");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
