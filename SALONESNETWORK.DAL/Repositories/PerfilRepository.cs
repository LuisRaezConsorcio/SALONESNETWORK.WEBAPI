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
    public class PerfilRepository : IPerfilRepository<Perfil>
    {
        private readonly SalonesDbContext _dbContext;

        public PerfilRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(Perfil modelo)
        {
            var entidadExistente = await _dbContext.Perfiles.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Perfil modelo = _dbContext.Perfiles.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.Perfiles.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Perfil modelo)
        {
            _dbContext.Perfiles.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Perfil> ObtenerPorId(int id)
        {
            return await _dbContext.Perfiles.FindAsync(id);
        }

        public async Task<IQueryable<Perfil>> ObtenerTodos()
        {
            IQueryable<Perfil> queryContactoSQL = _dbContext.Perfiles.Where(c => c.Estado == true);
            return queryContactoSQL;
        }
    }
}
