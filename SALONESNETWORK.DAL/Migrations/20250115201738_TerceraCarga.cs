using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SALONESNETWORK.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TerceraCarga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Usuarios",
                newName: "UserLocalId3");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployedId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "LocalId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocalTypeId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UserLocalComercialId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserLocalId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserLocalId2",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PerfilSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Perfil = table.Column<int>(type: "int", nullable: true),
                    Id_Seccion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilSecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilSecciones_Perfiles_Id_Perfil",
                        column: x => x.Id_Perfil,
                        principalTable: "Perfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PerfilSecciones_Secciones_Id_Seccion",
                        column: x => x.Id_Seccion,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuarioSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Id_Seccion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioSecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioSecciones_Secciones_Id_Seccion",
                        column: x => x.Id_Seccion,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioSecciones_Usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilSecciones_Id_Perfil",
                table: "PerfilSecciones",
                column: "Id_Perfil");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilSecciones_Id_Seccion",
                table: "PerfilSecciones",
                column: "Id_Seccion");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSecciones_Id_Seccion",
                table: "UsuarioSecciones",
                column: "Id_Seccion");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSecciones_Id_Usuario",
                table: "UsuarioSecciones",
                column: "Id_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilSecciones");

            migrationBuilder.DropTable(
                name: "UsuarioSecciones");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EmployedId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LocalId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LocalTypeId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UserLocalComercialId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UserLocalId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UserLocalId2",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "UserLocalId3",
                table: "Usuarios",
                newName: "Name");
        }
    }
}
