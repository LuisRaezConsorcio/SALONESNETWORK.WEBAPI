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
    public class TipoMensajeRepository:ITipoMensajeRepository<TipoMensaje>
    {
        private readonly SalonesDbContext _dbContext;

        public TipoMensajeRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(TipoMensaje modelo)
        {
            var entidadExistente = await _dbContext.TiposMensaje.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            TipoMensaje modelo = _dbContext.TiposMensaje.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.TiposMensaje.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(TipoMensaje modelo)
        {
            _dbContext.TiposMensaje.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TipoMensaje> ObtenerPorId(int id)
        {
            return await _dbContext.TiposMensaje.FindAsync(id);
        }

        public async Task<IQueryable<TipoMensaje>> ObtenerTodos()
        {
            IQueryable<TipoMensaje> queryContactoSQL = _dbContext.TiposMensaje.Where(c => c.Estado == true);
            return queryContactoSQL;
        }
    }
}
