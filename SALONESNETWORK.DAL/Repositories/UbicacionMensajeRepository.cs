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
    public class UbicacionMensajeRepository:IUbicacionMensajeRepository<UbicacionMensaje>
    {
        private readonly SalonesDbContext _dbContext;

        public UbicacionMensajeRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(UbicacionMensaje modelo)
        {
            var entidadExistente = await _dbContext.UbicacionMensajes.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            UbicacionMensaje modelo = _dbContext.UbicacionMensajes.First(c => c.Id == id);
            _dbContext.UbicacionMensajes.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(UbicacionMensaje modelo)
        {
            _dbContext.UbicacionMensajes.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UbicacionMensaje> ObtenerPorId(int id)
        {
            return await _dbContext.UbicacionMensajes.FindAsync(id);
        }

        public async Task<IQueryable<UbicacionMensaje>> ObtenerTodos()
        {
            IQueryable<UbicacionMensaje> queryContactoSQL = _dbContext.UbicacionMensajes;
            return queryContactoSQL;
        }
    }
}
