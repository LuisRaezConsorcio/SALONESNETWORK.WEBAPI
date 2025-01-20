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
    public class AsuntoPaisSeccionSubRepository:IAsuntoPaisSeccionSubRepository<AsuntoPaisSeccionSub>
    {
        private readonly SalonesDbContext _dbContext;

        public AsuntoPaisSeccionSubRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(AsuntoPaisSeccionSub modelo)
        {
            var entidadExistente = await _dbContext.AsuntoPaisSeccionSubs.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            AsuntoPaisSeccionSub modelo = _dbContext.AsuntoPaisSeccionSubs.First(c => c.Id == id);
            _dbContext.AsuntoPaisSeccionSubs.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(AsuntoPaisSeccionSub modelo)
        {
            _dbContext.AsuntoPaisSeccionSubs.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<AsuntoPaisSeccionSub> ObtenerPorId(int id)
        {
            return await _dbContext.AsuntoPaisSeccionSubs.FindAsync(id);
        }

        public async Task<IQueryable<AsuntoPaisSeccionSub>> ObtenerTodos()
        {
            IQueryable<AsuntoPaisSeccionSub> queryContactoSQL = _dbContext.AsuntoPaisSeccionSubs;
            return queryContactoSQL;
        }
    }
}
