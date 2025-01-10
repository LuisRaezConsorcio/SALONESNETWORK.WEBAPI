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
    public class SubSeccionRepository:ISubSeccionRepository<SubSeccion>
    {
        private readonly SalonesDbContext _dbContext;

        public SubSeccionRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(SubSeccion modelo)
        {
            var entidadExistente = await _dbContext.SubSecciones.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            SubSeccion modelo = _dbContext.SubSecciones.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.SubSecciones.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(SubSeccion modelo)
        {
            _dbContext.SubSecciones.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<SubSeccion> ObtenerPorId(int id)
        {
            return await _dbContext.SubSecciones.FindAsync(id);
        }

        public async Task<IQueryable<SubSeccion>> ObtenerTodos()
        {
            IQueryable<SubSeccion> queryContactoSQL = _dbContext.SubSecciones.Where(c => c.Estado == true);
            return queryContactoSQL;
        }
    }
}
