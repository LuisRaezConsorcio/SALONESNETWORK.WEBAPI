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
    public class PerfilSeccionRepository:IPerfilSeccionRepository<PerfilSeccion>
    {
        private readonly SalonesDbContext _dbContext;

        public PerfilSeccionRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(PerfilSeccion modelo)
        {
            var entidadExistente = await _dbContext.PerfilSecciones.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            PerfilSeccion modelo = _dbContext.PerfilSecciones.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.PerfilSecciones.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(PerfilSeccion modelo)
        {
            _dbContext.PerfilSecciones.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PerfilSeccion> ObtenerPorId(int id)
        {
            return await _dbContext.PerfilSecciones.FindAsync(id);
        }

        public async Task<IQueryable<PerfilSeccion>> ObtenerTodos()
        {
            IQueryable<PerfilSeccion> queryContactoSQL = _dbContext.PerfilSecciones;
            return queryContactoSQL;
        }
    }
}
