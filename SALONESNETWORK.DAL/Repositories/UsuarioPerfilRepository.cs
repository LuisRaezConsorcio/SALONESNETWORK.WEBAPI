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

        public async Task<bool> Actualizar(UsuarioPerfil modelo)
        {
            var entidadExistente = await _dbContext.UsuarioPerfiles.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            UsuarioPerfil modelo = _dbContext.UsuarioPerfiles.First(c => c.Id == id);
            _dbContext.UsuarioPerfiles.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(UsuarioPerfil modelo)
        {
            _dbContext.UsuarioPerfiles.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UsuarioPerfil> ObtenerPorId(int id)
        {
            return await _dbContext.UsuarioPerfiles.FindAsync(id);
        }

        public async Task<IQueryable<UsuarioPerfil>> ObtenerTodos()
        {
            IQueryable<UsuarioPerfil> queryContactoSQL = _dbContext.UsuarioPerfiles;
            return queryContactoSQL;
        }
    }
}
