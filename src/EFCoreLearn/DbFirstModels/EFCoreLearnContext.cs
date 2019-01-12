using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCoreLearn.DbFirstModels
{
    public partial class EFCoreLearnContext : DbContext
    {
        public EFCoreLearnContext()
        {
        }

        public EFCoreLearnContext(DbContextOptions<EFCoreLearnContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Post> Post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;uid=sa;pwd=123qwe!@#;Database=EFCoreLearn;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(80);

                entity.Property(e => e.Street).HasMaxLength(80);

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Address)
                    .HasForeignKey<Address>(d => d.OrderId);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.Url).HasMaxLength(80);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Ddd).HasColumnName("ddd");

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.Test1).HasMaxLength(80);

                entity.Property(e => e.Test2).HasMaxLength(120);

                entity.Property(e => e.TestGuid).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.BlogId);

                entity.Property(e => e.Content).HasMaxLength(80);

                entity.Property(e => e.Title).HasMaxLength(80);
            });
        }
    }
}
