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

        public async Task<RegistroVisita> ObtenerPorIdUsuario(RegistroVisita modelo)
        {
            return _dbContext.RegistroVisitas.First(c => c.Id_Usuario == modelo.Id_Usuario);
        }
    }
}
