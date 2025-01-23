﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SALONESNETWORK.DAL.Data;

#nullable disable

namespace SALONESNETWORK.DAL.Migrations
{
    [DbContext(typeof(SalonesDbContext))]
    [Migration("20250122142031_NovenaCarga")]
    partial class NovenaCarga
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Asunto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Asuntos");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.AsuntoPaisSeccionSub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Id_Asunto")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Pais")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Seccion")
                        .HasColumnType("int");

                    b.Property<int?>("Id_SubSeccion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Asunto");

                    b.HasIndex("Id_Pais");

                    b.HasIndex("Id_Seccion");

                    b.HasIndex("Id_SubSeccion");

                    b.ToTable("AsuntoPaisSeccionSubs");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Documento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Ubicacion")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.DocumentoMensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Id_Documento")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Mensaje")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Documento");

                    b.HasIndex("Id_Mensaje");

                    b.ToTable("DocumentoMensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Mensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Id_MensajeRespuesta")
                        .HasColumnType("int");

                    b.Property<int?>("Id_MensajeSeguimiento")
                        .HasColumnType("int");

                    b.Property<int?>("Id_TipoMensaje")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Usuario")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Respuesta")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("Seguimiento")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("UsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_TipoMensaje");

                    b.ToTable("Mensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Perfiles");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.PerfilSeccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Id_Perfil")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Seccion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Perfil");

                    b.HasIndex("Id_Seccion");

                    b.ToTable("PerfilSecciones");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.RegistroVisita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Id_Usuario")
                        .HasColumnType("int");

                    b.Property<string>("Ip")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("RegistroVisitas");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Seccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Secciones");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.SubSeccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SubSecciones");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.TipoMensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int?>("UsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TiposMensaje");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.UbicacionMensaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Id_Asunto")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Mensaje")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Pais")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Seccion")
                        .HasColumnType("int");

                    b.Property<int?>("Id_SubSeccion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Asunto");

                    b.HasIndex("Id_Mensaje");

                    b.HasIndex("Id_Pais");

                    b.HasIndex("Id_Seccion");

                    b.HasIndex("Id_SubSeccion");

                    b.ToTable("UbicacionMensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployedId")
                        .HasColumnType("int");

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<int?>("LocalId")
                        .HasColumnType("int");

                    b.Property<int?>("LocalTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserLocalComercialId")
                        .HasColumnType("int");

                    b.Property<int?>("UserLocalId")
                        .HasColumnType("int");

                    b.Property<int?>("UserLocalId2")
                        .HasColumnType("int");

                    b.Property<int?>("UserLocalId3")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.UsuarioPerfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Id_Perfil")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Usuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Perfil");

                    b.HasIndex("Id_Usuario");

                    b.ToTable("UsuarioPerfiles");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.UsuarioSeccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Id_Seccion")
                        .HasColumnType("int");

                    b.Property<int?>("Id_Usuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Seccion");

                    b.HasIndex("Id_Usuario");

                    b.ToTable("UsuarioSecciones");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.AsuntoPaisSeccionSub", b =>
                {
                    b.HasOne("SALONESNETWORK.MODELS.Entities.Asunto", "Asunto")
                        .WithMany("AsuntoPaisSeccionSubs")
                        .HasForeignKey("Id_Asunto");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Pais", "Pais")
                        .WithMany("AsuntoPaisSeccionSubs")
                        .HasForeignKey("Id_Pais");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Seccion", "Seccion")
                        .WithMany("AsuntoPaisSeccionSubs")
                        .HasForeignKey("Id_Seccion");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.SubSeccion", "SubSeccion")
                        .WithMany("AsuntoPaisSeccionSubs")
                        .HasForeignKey("Id_SubSeccion");

                    b.Navigation("Asunto");

                    b.Navigation("Pais");

                    b.Navigation("Seccion");

                    b.Navigation("SubSeccion");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.DocumentoMensaje", b =>
                {
                    b.HasOne("SALONESNETWORK.MODELS.Entities.Documento", "Documento")
                        .WithMany("DocumentoMensajes")
                        .HasForeignKey("Id_Documento");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Mensaje", "Mensaje")
                        .WithMany("DocumentoMensajes")
                        .HasForeignKey("Id_Mensaje");

                    b.Navigation("Documento");

                    b.Navigation("Mensaje");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Mensaje", b =>
                {
                    b.HasOne("SALONESNETWORK.MODELS.Entities.TipoMensaje", null)
                        .WithMany()
                        .HasForeignKey("Id_TipoMensaje");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.PerfilSeccion", b =>
                {
                    b.HasOne("SALONESNETWORK.MODELS.Entities.Perfil", "Perfil")
                        .WithMany("PerfilSecciones")
                        .HasForeignKey("Id_Perfil");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Seccion", "Seccion")
                        .WithMany("PerfilSecciones")
                        .HasForeignKey("Id_Seccion");

                    b.Navigation("Perfil");

                    b.Navigation("Seccion");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.UbicacionMensaje", b =>
                {
                    b.HasOne("SALONESNETWORK.MODELS.Entities.Asunto", "Asunto")
                        .WithMany("UbicacionMensajes")
                        .HasForeignKey("Id_Asunto");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Mensaje", "Mensaje")
                        .WithMany("UbicacionMensajes")
                        .HasForeignKey("Id_Mensaje");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Pais", "Pais")
                        .WithMany("UbicacionMensajes")
                        .HasForeignKey("Id_Pais");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Seccion", "Seccion")
                        .WithMany("UbicacionMensajes")
                        .HasForeignKey("Id_Seccion");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.SubSeccion", "SubSeccion")
                        .WithMany("UbicacionMensajes")
                        .HasForeignKey("Id_SubSeccion");

                    b.Navigation("Asunto");

                    b.Navigation("Mensaje");

                    b.Navigation("Pais");

                    b.Navigation("Seccion");

                    b.Navigation("SubSeccion");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.UsuarioPerfil", b =>
                {
                    b.HasOne("SALONESNETWORK.MODELS.Entities.Perfil", "Perfil")
                        .WithMany("UsuarioPerfiles")
                        .HasForeignKey("Id_Perfil");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioPerfiles")
                        .HasForeignKey("Id_Usuario");

                    b.Navigation("Perfil");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.UsuarioSeccion", b =>
                {
                    b.HasOne("SALONESNETWORK.MODELS.Entities.Seccion", "Seccion")
                        .WithMany("UsuarioSecciones")
                        .HasForeignKey("Id_Seccion");

                    b.HasOne("SALONESNETWORK.MODELS.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioSecciones")
                        .HasForeignKey("Id_Usuario");

                    b.Navigation("Seccion");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Asunto", b =>
                {
                    b.Navigation("AsuntoPaisSeccionSubs");

                    b.Navigation("UbicacionMensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Documento", b =>
                {
                    b.Navigation("DocumentoMensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Mensaje", b =>
                {
                    b.Navigation("DocumentoMensajes");

                    b.Navigation("UbicacionMensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Pais", b =>
                {
                    b.Navigation("AsuntoPaisSeccionSubs");

                    b.Navigation("UbicacionMensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Perfil", b =>
                {
                    b.Navigation("PerfilSecciones");

                    b.Navigation("UsuarioPerfiles");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Seccion", b =>
                {
                    b.Navigation("AsuntoPaisSeccionSubs");

                    b.Navigation("PerfilSecciones");

                    b.Navigation("UbicacionMensajes");

                    b.Navigation("UsuarioSecciones");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.SubSeccion", b =>
                {
                    b.Navigation("AsuntoPaisSeccionSubs");

                    b.Navigation("UbicacionMensajes");
                });

            modelBuilder.Entity("SALONESNETWORK.MODELS.Entities.Usuario", b =>
                {
                    b.Navigation("UsuarioPerfiles");

                    b.Navigation("UsuarioSecciones");
                });
#pragma warning restore 612, 618
        }
    }
}
