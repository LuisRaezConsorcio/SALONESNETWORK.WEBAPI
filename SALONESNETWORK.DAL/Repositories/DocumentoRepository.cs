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
    public class DocumentoRepository:IDocumentoRepository<Documento>
    {
        private readonly SalonesDbContext _dbContext;

        public DocumentoRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(Documento modelo)
        {
            var entidadExistente = await _dbContext.Documentos.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Documento modelo = _dbContext.Documentos.First(c => c.Id == id);
            _dbContext.Documentos.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Documento modelo)
        {
            _dbContext.Documentos.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Documento> ObtenerPorId(int id)
        {
            return await _dbContext.Documentos.FindAsync(id);
        }

        public async Task<IQueryable<Documento>> ObtenerTodos()
        {
            IQueryable<Documento> queryContactoSQL = _dbContext.Documentos;
            return queryContactoSQL;
        }
    }
}
