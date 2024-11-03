using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityLayer.Entities;

namespace Identity.Context
{
    public class IdentityContext : IdentityDbContext<Usuario, GnPerfil, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable(name: "Usuario");
            });

            modelBuilder.Entity<GnPerfil>(entity =>
            {
                entity.ToTable(name: "GnPerfil");
                entity.Property(r => r.NormalizedName).IsRequired().HasMaxLength(256);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(256);
                entity.HasIndex(r => r.NormalizedName).IsUnique();
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
        }
    }

}
