using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityLayer.Migrations
{
    /// <inheritdoc />
    public partial class GnPerfilChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bloquear",
                schema: "dbo",
                table: "GnPerfil");

            migrationBuilder.RenameColumn(
                name: "UltimaFechaModificacion",
                schema: "dbo",
                table: "GnPerfil",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "FechaCreada",
                schema: "dbo",
                table: "GnPerfil",
                newName: "FechaAdicion");

            migrationBuilder.AddColumn<bool>(
                name: "Borrado",
                schema: "dbo",
                table: "GnPerfil",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                schema: "dbo",
                table: "GnPerfil",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAdicion",
                schema: "dbo",
                table: "GnPerfil",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                schema: "dbo",
                table: "GnPerfil",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Borrado",
                schema: "dbo",
                table: "GnPerfil");

            migrationBuilder.DropColumn(
                name: "Perfil",
                schema: "dbo",
                table: "GnPerfil");

            migrationBuilder.DropColumn(
                name: "UsuarioAdicion",
                schema: "dbo",
                table: "GnPerfil");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                schema: "dbo",
                table: "GnPerfil");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                schema: "dbo",
                table: "GnPerfil",
                newName: "UltimaFechaModificacion");

            migrationBuilder.RenameColumn(
                name: "FechaAdicion",
                schema: "dbo",
                table: "GnPerfil",
                newName: "FechaCreada");

            migrationBuilder.AddColumn<int>(
                name: "Bloquear",
                schema: "dbo",
                table: "GnPerfil",
                type: "int",
                nullable: true);
        }
    }
}
