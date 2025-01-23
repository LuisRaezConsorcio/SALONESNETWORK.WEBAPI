using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SALONESNETWORK.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DecimaCarga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "UsuarioSecciones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "UsuarioSecciones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreacion",
                table: "UsuarioSecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacion",
                table: "UsuarioSecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Usuarios",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Usuarios",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreacion",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacion",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "UsuarioPerfiles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "UsuarioPerfiles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreacion",
                table: "UsuarioPerfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacion",
                table: "UsuarioPerfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "UbicacionMensajes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "UbicacionMensajes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreacion",
                table: "UbicacionMensajes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacion",
                table: "UbicacionMensajes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "PerfilSecciones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "PerfilSecciones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreacion",
                table: "PerfilSecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacion",
                table: "PerfilSecciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "AsuntoPaisSeccionSubs",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "AsuntoPaisSeccionSubs",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreacion",
                table: "AsuntoPaisSeccionSubs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacion",
                table: "AsuntoPaisSeccionSubs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "UsuarioSecciones");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "UsuarioSecciones");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "UsuarioSecciones");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "UsuarioSecciones");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "UsuarioPerfiles");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "UsuarioPerfiles");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "UsuarioPerfiles");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "UsuarioPerfiles");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "UbicacionMensajes");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "UbicacionMensajes");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "UbicacionMensajes");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "UbicacionMensajes");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "PerfilSecciones");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "PerfilSecciones");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "PerfilSecciones");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "PerfilSecciones");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "AsuntoPaisSeccionSubs");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "AsuntoPaisSeccionSubs");
        }
    }
}
