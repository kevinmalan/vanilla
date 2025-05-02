using Data.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(x => x.Id)
                .HasName("pk_users");

                entity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

                entity.Property(x => x.UniqueId)
                .HasColumnName("unique_id")
                .IsRequired();

                entity.HasIndex(x => x.UniqueId)
                .HasDatabaseName("ix_users_unique_id");

                entity.Property(x => x.Username)
                .HasColumnName("username")
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(x => x.Firstname)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(x => x.Lastname)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(x => x.Password)
                .HasColumnName("password_hash")
                .IsRequired();

                entity.Property(x => x.Salt)
                .HasColumnName("salt")
                .IsRequired();

                entity.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();

                entity.Property(x => x.StatusReason)
                .HasColumnName("status_reason")
                .HasMaxLength(200);

                entity.Property(x => x.Role)
                .HasColumnName("role")
                .HasConversion<int>()
                .IsRequired();
            });
        }
    }
}