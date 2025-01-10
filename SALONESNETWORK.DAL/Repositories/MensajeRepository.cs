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
    public class MensajeRepository:IMensajeRepository<Mensaje>
    {
        private readonly SalonesDbContext _dbContext;

        public MensajeRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(Mensaje modelo)
        {
            var entidadExistente = await _dbContext.Mensajes.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Mensaje modelo = _dbContext.Mensajes.First(c => c.Id == id);
            _dbContext.Mensajes.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Mensaje modelo)
        {
            _dbContext.Mensajes.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Mensaje> ObtenerPorId(int id)
        {
            return await _dbContext.Mensajes.FindAsync(id);
        }

        public async Task<IQueryable<Mensaje>> ObtenerTodos()
        {
            IQueryable<Mensaje> queryContactoSQL = _dbContext.Mensajes;
            return queryContactoSQL;
        }
    }
}
