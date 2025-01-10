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
    public class UsuarioRepository:IUsuarioRepository<Usuario>
    {
        private readonly SalonesDbContext _dbContext;

        public UsuarioRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(Usuario modelo)
        {
            var entidadExistente = await _dbContext.Usuarios.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Usuario modelo = _dbContext.Usuarios.First(c => c.Id == id);
            _dbContext.Usuarios.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> ObtenerPorId(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }

        public async Task<IQueryable<Usuario>> ObtenerTodos()
        {
            IQueryable<Usuario> queryContactoSQL = _dbContext.Usuarios;
            return queryContactoSQL;
        }
    }
}
