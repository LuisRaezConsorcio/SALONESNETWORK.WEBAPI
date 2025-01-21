using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.DAL.Interfaces;
using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.DAL.Repositories
{
    public class UsuarioPerfilRepository:IUsuarioPerfilRepository<UsuarioPerfil>
    {
        private readonly SalonesDbContext _dbContext;

        public UsuarioPerfilRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Eliminar(int id)
        {
            UsuarioPerfil modelo = _dbContext.UsuarioPerfiles.FirstOrDefault(c => c.Id == id);
            if (modelo == null)
                return false; // Manejar si no se encuentra el registro
            

            modelo.Estado = false;
            _dbContext.UsuarioPerfiles.Update(modelo);

            var idPerfil = modelo.Id_Perfil;

            if (idPerfil == null)
                return false; // Si no tiene un perfil asociado, no hay más lógica que aplicar
            

            await _dbContext.SaveChangesAsync();

            // Consultar las secciones asociadas al perfil en la tabla PerfilSeccion
            var idsSecciones = await _dbContext.PerfilSecciones
                .Where(ps => ps.Id_Perfil == idPerfil)
                .Select(ps => ps.Id_Seccion)
                .ToListAsync();

            if (!idsSecciones.Any())
                return false; // Si no hay secciones asociadas al perfil, no hay más lógica que aplicar
            

            // Buscar los registros en UsuarioSeccion que coincidan con el Id_Usuario y las Id_Seccion obtenidas
            var usuarioSecciones = await _dbContext.UsuarioSecciones
                .Where(us => us.Id_Usuario == modelo.Id_Usuario && idsSecciones.Contains(us.Id_Seccion))
                .ToListAsync();

            // Cambiar el estado de los registros en UsuarioSeccion a false
            foreach (var usuarioSeccion in usuarioSecciones)
            {
                usuarioSeccion.Estado = false;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPorUsuario(int id)
        {
            // Obtén todos los registros con el Id_Usuario proporcionado
            var modelos = await _dbContext.UsuarioPerfiles.Where(c => c.Id_Usuario == id).ToListAsync();

            if (!modelos.Any())
                return false;

            foreach (var modelo in modelos)
            {
                modelo.Estado = false;
            }

            await _dbContext.SaveChangesAsync();

            var usuarioSecciones = await _dbContext.UsuarioSecciones
                .Where(us => us.Id_Usuario == id)
                .ToListAsync();

            foreach (var usuarioSeccion in usuarioSecciones)
            {
                usuarioSeccion.Estado = false;
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarPorPerfil(int id)
        {
            var modelos = await _dbContext.UsuarioPerfiles.Where(c => c.Id_Perfil == id).ToListAsync();

            if (!modelos.Any())
                return false;

            foreach (var modelo in modelos)
            {
                modelo.Estado = false;
            }

            await _dbContext.SaveChangesAsync();

            var idsSecciones = await _dbContext.PerfilSecciones
                .Where(ps => ps.Id_Perfil == id)
                .Select(ps => ps.Id_Seccion)
                .Distinct()
                .ToListAsync();

            if (!idsSecciones.Any())
                return false; // Si no hay secciones asociadas al perfil, no hay más lógica que aplicar
            

            // Obtener los usuarios relacionados con el perfil eliminado
            var idsUsuarios = modelos.Select(m => m.Id_Usuario).Distinct().ToList();

            // Buscar y desactivar los registros en UsuarioSeccion que coincidan con Id_Usuario y las Id_Seccion
            var usuarioSecciones = await _dbContext.UsuarioSecciones
                .Where(us => idsUsuarios.Contains(us.Id_Usuario) && idsSecciones.Contains(us.Id_Seccion))
                .ToListAsync();

            foreach (var usuarioSeccion in usuarioSecciones)
            {
                usuarioSeccion.Estado = false;
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Insertar(UsuarioPerfil modelo)
        {

            if (modelo == null || modelo.Id_Usuario <= 0 || modelo.Id_Perfil <= 0)
                return false;

            // Verificar duplicados
            bool existe = await _dbContext.UsuarioPerfiles
                                           .AnyAsync(p => p.Id_Perfil == modelo.Id_Perfil && p.Id_Usuario == modelo.Id_Usuario);

            if (existe)
                return false;

            // Obtener las secciones asociadas al perfil
            var idsSecciones = await _dbContext.PerfilSecciones
                                               .Where(ps => ps.Id_Perfil == modelo.Id_Perfil)
                                               .Select(ps => ps.Id_Seccion)
                                               .ToListAsync();

            // Crear una nueva entrada de UsuarioPerfil para cada Id_Seccion
            foreach (var idSeccion in idsSecciones)
            {
                if (idSeccion.HasValue)
                {
                    _dbContext.UsuarioSecciones.Add(new UsuarioSeccion
                    {
                        Id_Usuario = modelo.Id_Usuario,
                        Id_Seccion = idSeccion.Value,
                        Estado = true
                    });
                }
            }

            // Agregar el modelo principal
            _dbContext.UsuarioPerfiles.Add(modelo);

            // Guardar los cambios en la base de datos
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int?> ObtenerId(UsuarioPerfil modelo)
        {
            return await _dbContext.UsuarioPerfiles
                           .Where(p => p.Id_Perfil == modelo.Id_Perfil && p.Id_Usuario == modelo.Id_Usuario)
                           .Select(p => (int?)p.Id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<UsuarioPerfil>> ObtenerTodos()
        {
            //IQueryable<UsuarioPerfil> queryContactoSQL = _dbContext.UsuarioPerfiles;
            return _dbContext.UsuarioPerfiles;
        }

        public async Task<IQueryable<UsuarioPerfil>> ObtenerPorUsuario(UsuarioPerfil modelo)
        {
            return _dbContext.UsuarioPerfiles.Where(p => p.Id_Usuario == modelo.Id_Usuario);
        } 

        public async Task<IQueryable<UsuarioPerfil>> ObtenerPorPerfil(UsuarioPerfil modelo)
        {
            return _dbContext.UsuarioPerfiles.Where(p => p.Id_Perfil == modelo.Id_Perfil);
        }
    }
}
