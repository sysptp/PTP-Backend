using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityLayer.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perfil",
                schema: "dbo",
                table: "GnPerfil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                schema: "dbo",
                table: "GnPerfil",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
