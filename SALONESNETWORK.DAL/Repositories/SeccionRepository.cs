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
    public class SeccionRepository:ISeccionRepository<Seccion>
    {
        private readonly SalonesDbContext _dbContext;

        public SeccionRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(Seccion modelo)
        {
            var entidadExistente = await _dbContext.Secciones.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Seccion modelo = _dbContext.Secciones.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.Secciones.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Seccion modelo)
        {
            _dbContext.Secciones.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Seccion> ObtenerPorId(int id)
        {
            return await _dbContext.Secciones.FindAsync(id);
        }

        public async Task<IQueryable<Seccion>> ObtenerTodos()
        {
            IQueryable<Seccion> queryContactoSQL = _dbContext.Secciones.Where(c => c.Estado == true);
            return queryContactoSQL;
        }
    }
}
