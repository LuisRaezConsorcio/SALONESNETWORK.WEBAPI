using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SALONESNETWORK.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CuartaCarga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsuntoPaisSeccionSubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Asunto = table.Column<int>(type: "int", nullable: true),
                    AsuntoId = table.Column<int>(type: "int", nullable: true),
                    Id_Pais = table.Column<int>(type: "int", nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: true),
                    Id_Seccion = table.Column<int>(type: "int", nullable: true),
                    SeccionId = table.Column<int>(type: "int", nullable: true),
                    Id_SubSeccion = table.Column<int>(type: "int", nullable: true),
                    SubSeccionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsuntoPaisSeccionSubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsuntoPaisSeccionSubs_Asuntos_AsuntoId",
                        column: x => x.AsuntoId,
                        principalTable: "Asuntos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AsuntoPaisSeccionSubs_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AsuntoPaisSeccionSubs_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AsuntoPaisSeccionSubs_SubSecciones_SubSeccionId",
                        column: x => x.SubSeccionId,
                        principalTable: "SubSecciones",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_AsuntoId",
                table: "AsuntoPaisSeccionSubs",
                column: "AsuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_PaisId",
                table: "AsuntoPaisSeccionSubs",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_SeccionId",
                table: "AsuntoPaisSeccionSubs",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_SubSeccionId",
                table: "AsuntoPaisSeccionSubs",
                column: "SubSeccionId");
        }

        /// <inheritdoc />
        
    }
}
