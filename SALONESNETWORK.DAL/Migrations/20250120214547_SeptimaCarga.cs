using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SALONESNETWORK.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeptimaCarga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_Asuntos_Id_Asunto",
                table: "Mensajes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_Paises_Id_Pais",
                table: "Mensajes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_Secciones_Id_Seccion",
                table: "Mensajes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_SubSecciones_Id_SubSeccion",
                table: "Mensajes");

            migrationBuilder.DropIndex(
                name: "IX_Mensajes_Id_Asunto",
                table: "Mensajes");

            migrationBuilder.DropIndex(
                name: "IX_Mensajes_Id_Pais",
                table: "Mensajes");

            migrationBuilder.DropIndex(
                name: "IX_Mensajes_Id_Seccion",
                table: "Mensajes");

            migrationBuilder.DropIndex(
                name: "IX_Mensajes_Id_SubSeccion",
                table: "Mensajes");

            migrationBuilder.DropColumn(
                name: "Id_Asunto",
                table: "Mensajes");

            migrationBuilder.DropColumn(
                name: "Id_Pais",
                table: "Mensajes");

            migrationBuilder.DropColumn(
                name: "Id_Seccion",
                table: "Mensajes");

            migrationBuilder.DropColumn(
                name: "Id_SubSeccion",
                table: "Mensajes");

            migrationBuilder.CreateTable(
                name: "UbicacionMensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Mensaje = table.Column<int>(type: "int", nullable: true),
                    Id_Asunto = table.Column<int>(type: "int", nullable: true),
                    Id_Pais = table.Column<int>(type: "int", nullable: true),
                    Id_Seccion = table.Column<int>(type: "int", nullable: true),
                    Id_SubSeccion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionMensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionMensajes_Asuntos_Id_Asunto",
                        column: x => x.Id_Asunto,
                        principalTable: "Asuntos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UbicacionMensajes_Mensajes_Id_Mensaje",
                        column: x => x.Id_Mensaje,
                        principalTable: "Mensajes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UbicacionMensajes_Paises_Id_Pais",
                        column: x => x.Id_Pais,
                        principalTable: "Paises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UbicacionMensajes_Secciones_Id_Seccion",
                        column: x => x.Id_Seccion,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UbicacionMensajes_SubSecciones_Id_SubSeccion",
                        column: x => x.Id_SubSeccion,
                        principalTable: "SubSecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionMensajes_Id_Asunto",
                table: "UbicacionMensajes",
                column: "Id_Asunto");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionMensajes_Id_Mensaje",
                table: "UbicacionMensajes",
                column: "Id_Mensaje");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionMensajes_Id_Pais",
                table: "UbicacionMensajes",
                column: "Id_Pais");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionMensajes_Id_Seccion",
                table: "UbicacionMensajes",
                column: "Id_Seccion");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionMensajes_Id_SubSeccion",
                table: "UbicacionMensajes",
                column: "Id_SubSeccion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UbicacionMensajes");

            migrationBuilder.AddColumn<int>(
                name: "Id_Asunto",
                table: "Mensajes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_Pais",
                table: "Mensajes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_Seccion",
                table: "Mensajes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_SubSeccion",
                table: "Mensajes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Id_Asunto",
                table: "Mensajes",
                column: "Id_Asunto");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Id_Pais",
                table: "Mensajes",
                column: "Id_Pais");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Id_Seccion",
                table: "Mensajes",
                column: "Id_Seccion");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_Id_SubSeccion",
                table: "Mensajes",
                column: "Id_SubSeccion");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_Asuntos_Id_Asunto",
                table: "Mensajes",
                column: "Id_Asunto",
                principalTable: "Asuntos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_Paises_Id_Pais",
                table: "Mensajes",
                column: "Id_Pais",
                principalTable: "Paises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_Secciones_Id_Seccion",
                table: "Mensajes",
                column: "Id_Seccion",
                principalTable: "Secciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_SubSecciones_Id_SubSeccion",
                table: "Mensajes",
                column: "Id_SubSeccion",
                principalTable: "SubSecciones",
                principalColumn: "Id");
        }
    }
}
