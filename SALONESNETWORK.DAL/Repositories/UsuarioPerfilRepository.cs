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

        //public async Task<bool> Actualizar(UsuarioPerfil modelo)
        //{
        //    var entidadExistente = await _dbContext.UsuarioPerfiles.FindAsync(modelo.Id);

        //    if (entidadExistente == null)
        //        return false;

        //    _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
        //    await _dbContext.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> Eliminar(int id)
        {
            UsuarioPerfil modelo = _dbContext.UsuarioPerfiles.FirstOrDefault(c => c.Id == id);
            if (modelo == null)
            {
                return false; // Manejar si no se encuentra el registro
            }
            modelo.Estado = false;
            _dbContext.UsuarioPerfiles.Update(modelo);
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
                        Id_Seccion = idSeccion.Value
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
            IQueryable<UsuarioPerfil> queryContactoSQL = _dbContext.UsuarioPerfiles;
            return queryContactoSQL;
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
