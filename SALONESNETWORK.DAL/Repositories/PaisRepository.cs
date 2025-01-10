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
    public class PaisRepository : IPaisRepository<Pais>
    {
        private readonly SalonesDbContext _dbContext;

        public PaisRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(Pais modelo)
        {
            var entidadExistente = await _dbContext.Paises.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false; 

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Pais modelo = _dbContext.Paises.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.Paises.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Pais modelo)
        {
            _dbContext.Paises.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Pais> ObtenerPorId(int id)
        {
            return await _dbContext.Paises.FindAsync(id);
        }

        public async Task<IQueryable<Pais>> ObtenerTodos()
        {
            IQueryable<Pais> queryContactoSQL = _dbContext.Paises.Where(c => c.Estado == true);
            return queryContactoSQL;
        }
    }
}
