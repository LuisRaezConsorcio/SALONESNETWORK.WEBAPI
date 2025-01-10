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
    public class DocumentoMensajeRepository:IDocumentoMensajeRepository<DocumentoMensaje>
    {
        private readonly SalonesDbContext _dbContext;

        public DocumentoMensajeRepository(SalonesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Actualizar(DocumentoMensaje modelo)
        {
            var entidadExistente = await _dbContext.DocumentoMensajes.FindAsync(modelo.Id);

            if (entidadExistente == null)
                return false;

            _dbContext.Entry(entidadExistente).CurrentValues.SetValues(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            DocumentoMensaje modelo = _dbContext.DocumentoMensajes.First(c => c.Id == id);
            _dbContext.DocumentoMensajes.Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(DocumentoMensaje modelo)
        {
            _dbContext.DocumentoMensajes.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<DocumentoMensaje> ObtenerPorId(int id)
        {
            return await _dbContext.DocumentoMensajes.FindAsync(id);
        }

        public async Task<IQueryable<DocumentoMensaje>> ObtenerTodos()
        {
            IQueryable<DocumentoMensaje> queryContactoSQL = _dbContext.DocumentoMensajes;
            return queryContactoSQL;
        }
    }
}
