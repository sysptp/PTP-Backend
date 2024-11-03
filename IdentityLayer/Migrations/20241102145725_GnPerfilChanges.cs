using System;
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
            migrationBuilder.CreateTable(
                name: "GnPerfil",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Perfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDEmpresa = table.Column<long>(type: "bigint", nullable: true),
                    FechaAdicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioAdicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Borrado = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GnPerfil", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "GnPerfil",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_GnPerfil_RoleId",
                schema: "dbo",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "GnPerfil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_GnPerfil_RoleId",
                schema: "dbo",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "GnPerfil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Usuario_UserId",
                schema: "dbo",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_GnPerfil_RoleId",
                schema: "dbo",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_GnPerfil_RoleId",
                schema: "dbo",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Usuario_UserId",
                schema: "dbo",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "GnPerfil",
                schema: "dbo");
        }
    }
}
