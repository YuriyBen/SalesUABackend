using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SalesUA.Entities
{
    public partial class SalesProjectContext : DbContext
    {
        public SalesProjectContext()
        {
        }

        public SalesProjectContext(DbContextOptions<SalesProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                optionsBuilder.UseSqlServer("Name=SalesDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.NewPrice).HasColumnType("smallmoney");

                entity.Property(e => e.OldPrice).HasColumnType("smallmoney");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("FK__Product__ShopId__267ABA7A");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("UQ_Title")
                    .IsUnique();

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
