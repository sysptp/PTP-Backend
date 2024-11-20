using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityLayer.Migrations
{
    /// <inheritdoc />
    public partial class alterUserFechaAdicion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioAdiccion",
                schema: "dbo",
                table: "Usuario",
                newName: "UsuarioAdicion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAdicion",
                schema: "dbo",
                table: "Usuario",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioAdicion",
                schema: "dbo",
                table: "Usuario",
                newName: "UsuarioAdiccion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAdicion",
                schema: "dbo",
                table: "Usuario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
