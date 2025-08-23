using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class AuthContext(DbContextOptions<AuthContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User
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

            // RefreshToken
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("refresh_tokens");

                entity.HasKey(r => r.Id).HasName("pk_refresh_tokens");
                entity.Property(r => r.Id)
                      .HasColumnName("id")
                      .UseIdentityByDefaultColumn();

                entity.Property(r => r.TokenHash)
                      .HasColumnName("token_hash")
                      .IsRequired()
                      .HasMaxLength(88); // SHA256 hash base64-ish length

                entity.Property(r => r.ExpiresAt)
                      .HasColumnName("expires_at")
                      .IsRequired();

                entity.Property(r => r.CreatedAt)
                      .HasColumnName("created_at")
                      .IsRequired();

                entity.Property(r => r.CreatedByIp)
                      .HasColumnName("created_by_ip")
                      .HasMaxLength(45)  // support IPv6 text
                      .IsRequired();

                entity.Property(r => r.RevokedAt)
                      .HasColumnName("revoked_at");

                entity.Property(r => r.RevokedByIp)
                      .HasColumnName("revoked_by_ip")
                      .HasMaxLength(45);

                entity.Property(r => r.ReplacedBy)
                      .HasColumnName("replaced_by")
                      .HasMaxLength(88);

                // Foreign key to Users table:
                entity.Property(r => r.UserId)
                      .HasColumnName("user_id")
                      .IsRequired();
                entity.HasOne(r => r.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}