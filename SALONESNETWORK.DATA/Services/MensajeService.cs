using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Interfaces;
using SALONESNETWORK.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALONESNETWORK.BLL.Services
{
    public class MensajeService:IMensajeService
    {
        private readonly IMensajeRepository<Mensaje> _MensajeRepository;
        public MensajeService(IMensajeRepository<Mensaje> MensajeRepository)
        {
            _MensajeRepository = MensajeRepository;
        }
        public async Task<bool> Actualizar(Mensaje modelo)
        {
            return await _MensajeRepository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _MensajeRepository.Eliminar(id);
        }

        public async Task<bool> Insertar(Mensaje modelo)
        {
            return await _MensajeRepository.Insertar(modelo);
        }

        public async Task<Mensaje> ObtenerPorId(int id)
        {
            return await _MensajeRepository.ObtenerPorId(id);
        }

        //public async Task<Mensaje> ObtenerPorNombre(string nombreContacto)
        //{
        //    IQueryable<Mensaje> queryContactoSQL = await _MensajeRepository.ObtenerTodos();
        //    Mensaje contacto = queryContactoSQL.Where(c => c.Nombre == nombreContacto).FirstOrDefault();
        //    return contacto;
        //}

        public async Task<IQueryable<Mensaje>> ObtenerTodos()
        {
            return await _MensajeRepository.ObtenerTodos();
        }
    }
}
