using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityLayer.Entities;

namespace TaskMaster.Infraestructure.Identity.Context
{
    public class IdentityContext : IdentityDbContext<SC_USUAR001, IdentityRole<long>, long>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo"); 

            modelBuilder.Entity<SC_USUAR001>(entity =>
            {
                entity.ToTable("SC_USUAR001");
                entity.Property(e => e.Id).HasColumnName("CODIGO_USUARIO");
                entity.Property(e => e.CodigoEmp).HasColumnName("CODIGO_EMP");
                entity.Property(e => e.NombreUsuario).HasColumnName("NOMBRE_USUARIO");
                entity.Property(e => e.UserName).HasColumnName("USUARIO");
                entity.Property(e => e.PasswordHash).HasColumnName("PASSWOR");
                entity.Property(e => e.IdHorario).HasColumnName("ID_HORARIO");
                entity.Property(e => e.IdPerfil).HasColumnName("ID_PERFIL");
                entity.Property(e => e.EmailConfirmed).HasColumnName("CORREO_CONFIRMADO");
                entity.Property(e => e.ImagenUsuario).HasColumnName("IMAGEN_USUARIO");
                entity.Property(e => e.Email).HasColumnName("CORREO");
                entity.Property(e => e.TelefonoPersonal).HasColumnName("TELEFONO_PERSONAL");
                entity.Property(e => e.ExtencionPersonal).HasColumnName("EXTENCION_PERSONAL");
                entity.Property(e => e.PhoneNumber).HasColumnName("TELEFONO");
                entity.Property(e => e.Extencion).HasColumnName("EXTENCION");
                entity.Property(e => e.OnlineUsuario).HasColumnName("ONLINE_USUARIO");
                entity.Property(e => e.CodigoSuc).HasColumnName("CODIGO_SUC");
                entity.Property(e => e.IpAdiccion).HasColumnName("IP_ADICCION");
                entity.Property(e => e.IpModificacion).HasColumnName("IP_MODIFICACION");
                entity.Property(e => e.UsuarioAdiccion).HasColumnName("USUARIO_ADICCION");
                entity.Property(e => e.FechaAdicion).HasColumnName("FECHA_ADICION");
                entity.Property(e => e.UsuarioModificacion).HasColumnName("USUARIO_MODIFICACION");
                entity.Property(e => e.FechaModificacion).HasColumnName("FECHA_MODIFICACION");
                entity.Property(e => e.Longitud).HasColumnName("LONGITUD");
                entity.Property(e => e.Latitud).HasColumnName("LATITUD");
            });

            modelBuilder.Entity<IdentityRole<long>>(entity =>
            {
                entity.ToTable("gn_perfil");
                entity.Property(r => r.Id).HasColumnName("IDPerfil");
                entity.Property(r => r.Name).HasColumnName("Perfil");
            });

            modelBuilder.Entity<IdentityUserRole<long>>(entity =>
            {
                entity.ToTable("UserRoles"); 
            });

            modelBuilder.Entity<IdentityUserLogin<long>>(entity =>
            {
                entity.ToTable("UserLogins"); 
            });

            modelBuilder.Entity<IdentityUserClaim<long>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserToken<long>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
