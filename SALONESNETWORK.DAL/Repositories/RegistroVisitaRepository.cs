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
    public class RegistroVisitaRepository:IRegistroVisitaRepository<RegistroVisita>
    {
        private readonly SalonesDbContext _dbContext;

        public RegistroVisitaRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(RegistroVisita modelo)
        {
            var entidadExistente = await _dbContext.RegistroVisitas.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            RegistroVisita modelo = _dbContext.RegistroVisitas.First(c => c.Id == id);
            _dbContext.RegistroVisitas.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(RegistroVisita modelo)
        {
            _dbContext.RegistroVisitas.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<RegistroVisita> ObtenerPorId(int id)
        {
            return await _dbContext.RegistroVisitas.FindAsync(id);
        }

        public async Task<IQueryable<RegistroVisita>> ObtenerTodos()
        {
            IQueryable<RegistroVisita> queryContactoSQL = _dbContext.RegistroVisitas;
            return queryContactoSQL;
        }
    }
}
