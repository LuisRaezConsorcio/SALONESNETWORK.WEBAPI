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

        public DbSet<AsuntoPaisSeccionSub> AsuntoPaisSeccionSubs { get; set; }
        public DbSet<Asunto> Asuntos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<DocumentoMensaje> DocumentoMensajes { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<PerfilSeccion> PerfilSecciones { get; set; }
        public DbSet<RegistroVisita> RegistroVisitas { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<SubSeccion> SubSecciones { get; set; }
        public DbSet<TipoMensaje> TiposMensaje { get; set; }
        public DbSet<UbicacionMensaje> UbicacionMensajes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfiles { get; set; }
        public DbSet<UsuarioSeccion> UsuarioSecciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla AsuntoPaisSeccionSub (muchos a muchos)
            modelBuilder.Entity<AsuntoPaisSeccionSub>()
                .HasKey(dm => dm.Id); // Clave primaria
            modelBuilder.Entity<AsuntoPaisSeccionSub>()
                .HasOne(dm => dm.Asunto)
                .WithMany(m => m.AsuntoPaisSeccionSubs)
                .HasForeignKey(dm => dm.Id_Asunto);
            modelBuilder.Entity<AsuntoPaisSeccionSub>()
                .HasOne(dm => dm.Pais)
                .WithMany(d => d.AsuntoPaisSeccionSubs)
                .HasForeignKey(dm => dm.Id_Pais);
            modelBuilder.Entity<AsuntoPaisSeccionSub>()
                .HasOne(dm => dm.Seccion)
                .WithMany(d => d.AsuntoPaisSeccionSubs)
                .HasForeignKey(dm => dm.Id_Seccion);
            modelBuilder.Entity<AsuntoPaisSeccionSub>()
                .HasOne(dm => dm.SubSeccion)
                .WithMany(d => d.AsuntoPaisSeccionSubs)
                .HasForeignKey(dm => dm.Id_SubSeccion);

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

            // Configuración de la tabla UbicacionMensajex (muchos a muchos)
            modelBuilder.Entity<UbicacionMensaje>()
                .HasKey(dm => dm.Id); // Clave primaria
            modelBuilder.Entity<UbicacionMensaje>()
                .HasOne(dm => dm.Mensaje)
                .WithMany(m => m.UbicacionMensajes)
                .HasForeignKey(dm => dm.Id_Mensaje); 
            modelBuilder.Entity<UbicacionMensaje>()
                .HasOne(dm => dm.Asunto)
                .WithMany(m => m.UbicacionMensajes)
                .HasForeignKey(dm => dm.Id_Asunto);
            modelBuilder.Entity<UbicacionMensaje>()
                .HasOne(dm => dm.Pais)
                .WithMany(d => d.UbicacionMensajes)
                .HasForeignKey(dm => dm.Id_Pais);
            modelBuilder.Entity<UbicacionMensaje>()
                .HasOne(dm => dm.Seccion)
                .WithMany(d => d.UbicacionMensajes)
                .HasForeignKey(dm => dm.Id_Seccion);
            modelBuilder.Entity<UbicacionMensaje>()
                .HasOne(dm => dm.SubSeccion)
                .WithMany(d => d.UbicacionMensajes)
                .HasForeignKey(dm => dm.Id_SubSeccion);


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

            // Relación UsuarioPerfil con Seccion y Usuario
            modelBuilder.Entity<UsuarioSeccion>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioSecciones)
                .HasForeignKey(up => up.Id_Usuario);

            modelBuilder.Entity<UsuarioSeccion>()
                .HasOne(up => up.Seccion)
                .WithMany(p => p.UsuarioSecciones)
                .HasForeignKey(up => up.Id_Seccion);

            // Relación UsuarioPerfil con Perfil y Seccion
            modelBuilder.Entity<PerfilSeccion>()
                .HasOne(up => up.Perfil)
                .WithMany(p => p.PerfilSecciones)
                .HasForeignKey(up => up.Id_Perfil);

            modelBuilder.Entity<PerfilSeccion>()
                .HasOne(up => up.Seccion)
                .WithMany(u => u.PerfilSecciones)
                .HasForeignKey(up => up.Id_Seccion);

            
        }

    }
}
