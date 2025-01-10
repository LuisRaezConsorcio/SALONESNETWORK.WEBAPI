using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.DAL.Data
{
    public class SalonesDbContext : DbContext
    {
        public SalonesDbContext(DbContextOptions<SalonesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Asunto> Asuntos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<DocumentoMensaje> DocumentoMensajes { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<RegistroVisita> RegistroVisitas { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<SubSeccion> SubSecciones { get; set; }
        public DbSet<TipoMensaje> TiposMensaje { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla DocumentoMensaje (muchos a muchos)
            modelBuilder.Entity<DocumentoMensaje>()
                .HasKey(dm => dm.Id); // Clave primaria
            modelBuilder.Entity<DocumentoMensaje>()
                .HasOne(dm => dm.Mensaje)
                .WithMany(m => m.DocumentoMensajes)
                .HasForeignKey(dm => dm.Id_Mensaje);
            modelBuilder.Entity<DocumentoMensaje>()
                .HasOne(dm => dm.Documento)
                .WithMany(d => d.DocumentoMensajes)
                .HasForeignKey(dm => dm.Id_Documento);

            // Configuración de la tabla UsuarioPerfil (muchos a muchos)
            modelBuilder.Entity<UsuarioPerfil>()
                .HasKey(up => up.Id); // Clave primaria
            modelBuilder.Entity<UsuarioPerfil>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioPerfiles)
                .HasForeignKey(up => up.Id_Usuario);
            modelBuilder.Entity<UsuarioPerfil>()
                .HasOne(up => up.Perfil)
                .WithMany(p => p.UsuarioPerfiles)
                .HasForeignKey(up => up.Id_Perfil);

            // Relación Mensaje con Asunto
            modelBuilder.Entity<Mensaje>()
                .HasOne<Asunto>()
                .WithMany()
                .HasForeignKey(m => m.Id_Asunto);

            // Relación Mensaje con Pais
            modelBuilder.Entity<Mensaje>()
                .HasOne<Pais>()
                .WithMany()
                .HasForeignKey(m => m.Id_Pais);

            // Relación Mensaje con Sección
            modelBuilder.Entity<Mensaje>()
                .HasOne<Seccion>()
                .WithMany()
                .HasForeignKey(m => m.Id_Seccion);

            // Relación Mensaje con SubSección
            modelBuilder.Entity<Mensaje>()
                .HasOne<SubSeccion>()
                .WithMany()
                .HasForeignKey(m => m.Id_SubSeccion);

            // Relación Mensaje con TipoMensaje
            modelBuilder.Entity<Mensaje>()
                .HasOne<TipoMensaje>()
                .WithMany()
                .HasForeignKey(m => m.Id_TipoMensaje);

            // Relación UsuarioPerfil con Perfil y Usuario
            modelBuilder.Entity<UsuarioPerfil>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioPerfiles)
                .HasForeignKey(up => up.Id_Usuario);

            modelBuilder.Entity<UsuarioPerfil>()
                .HasOne(up => up.Perfil)
                .WithMany(p => p.UsuarioPerfiles)
                .HasForeignKey(up => up.Id_Perfil);
        }

    }
}
