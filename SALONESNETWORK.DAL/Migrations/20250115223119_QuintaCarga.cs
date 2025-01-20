using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SALONESNETWORK.DAL.Migrations
{
    /// <inheritdoc />
    public partial class QuintaCarga : Migration
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
        
        migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Asuntos_AsuntoId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Paises_PaisId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Secciones_SeccionId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_SubSecciones_SubSeccionId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_AsuntoId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_PaisId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_SeccionId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_SubSeccionId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropColumn(
                name: "AsuntoId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropColumn(
                name: "PaisId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropColumn(
                name: "SeccionId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropColumn(
                name: "SubSeccionId",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_Asunto",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_Asunto");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_Pais",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_Pais");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_Seccion",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_Seccion");

            migrationBuilder.CreateIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_SubSeccion",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_SubSeccion");

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Asuntos_Id_Asunto",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_Asunto",
                principalTable: "Asuntos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Paises_Id_Pais",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_Pais",
                principalTable: "Paises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Secciones_Id_Seccion",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_Seccion",
                principalTable: "Secciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_SubSecciones_Id_SubSeccion",
                table: "AsuntoPaisSeccionSubs",
                column: "Id_SubSeccion",
                principalTable: "SubSecciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Asuntos_Id_Asunto",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Paises_Id_Pais",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Secciones_Id_Seccion",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_SubSecciones_Id_SubSeccion",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_Asunto",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_Pais",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_Seccion",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropIndex(
                name: "IX_AsuntoPaisSeccionSubs_Id_SubSeccion",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.AddColumn<int>(
                name: "AsuntoId",
                table: "AsuntoPaisSeccionSubs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaisId",
                table: "AsuntoPaisSeccionSubs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeccionId",
                table: "AsuntoPaisSeccionSubs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSeccionId",
                table: "AsuntoPaisSeccionSubs",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Asuntos_AsuntoId",
                table: "AsuntoPaisSeccionSubs",
                column: "AsuntoId",
                principalTable: "Asuntos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Paises_PaisId",
                table: "AsuntoPaisSeccionSubs",
                column: "PaisId",
                principalTable: "Paises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_Secciones_SeccionId",
                table: "AsuntoPaisSeccionSubs",
                column: "SeccionId",
                principalTable: "Secciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsuntoPaisSeccionSubs_SubSecciones_SubSeccionId",
                table: "AsuntoPaisSeccionSubs",
                column: "SubSeccionId",
                principalTable: "SubSecciones",
                principalColumn: "Id");
        }
    }
}
