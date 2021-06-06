using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MoviesApp.MovieModals
{
    public partial class MoviesDBContext : IdentityDbContext
    {
        public MoviesDBContext(DbContextOptions<MoviesDBContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Still> Stills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.ImdbId)
                    .HasMaxLength(50)
                    .HasColumnName("imdbId");

                entity.Property(e => e.ImdbRating)
                    .HasColumnType("decimal(1, 1)")
                    .HasColumnName("imdbRating");

                entity.Property(e => e.Language).HasMaxLength(50);

                entity.Property(e => e.ListingType)
                    .HasMaxLength(50)
                    .HasColumnName("listingType");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<Still>(entity =>
            {
                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Stills)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stills_Movies");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
