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
    public class UsuarioSeccionRepository:IUsuarioSeccionRepository<UsuarioSeccion>
    {
        private readonly SalonesDbContext _dbContext;

        public UsuarioSeccionRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(UsuarioSeccion modelo)
        {
            var entidadExistente = await _dbContext.UsuarioSecciones.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            
            UsuarioSeccion modelo = _dbContext.UsuarioSecciones.First(c => c.Id == id);
            modelo.Estado = false;
            _dbContext.UsuarioSecciones.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(UsuarioSeccion modelo)
        {
            _dbContext.UsuarioSecciones.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UsuarioSeccion> ObtenerPorId(int id)
        {
            return await _dbContext.UsuarioSecciones.FindAsync(id);
        }

        public async Task<IQueryable<UsuarioSeccion>> ObtenerTodos()
        {
            IQueryable<UsuarioSeccion> queryContactoSQL = _dbContext.UsuarioSecciones;
            return queryContactoSQL;
        }
    }
}
