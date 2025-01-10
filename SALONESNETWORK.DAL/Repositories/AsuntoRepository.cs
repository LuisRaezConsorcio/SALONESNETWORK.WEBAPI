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
    public class AsuntoRepository: IAsuntoRepository<Asunto>
    {
        private readonly SalonesDbContext _dbContext;

        public AsuntoRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(Asunto modelo)
        {
            var entidadExistente = await _dbContext.Asuntos.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Asunto modelo = _dbContext.Asuntos.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.Asuntos.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Asunto modelo)
        {
            _dbContext.Asuntos.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Asunto> ObtenerPorId(int id)
        {
            return await _dbContext.Asuntos.FindAsync(id);
        }

        public async Task<IQueryable<Asunto>> ObtenerTodos()
        {
            IQueryable<Asunto> queryContactoSQL = _dbContext.Asuntos.Where(c => c.Estado == true);
            return queryContactoSQL;
        }
    }
}
