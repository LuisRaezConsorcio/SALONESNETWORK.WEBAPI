using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.BLL.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;

namespace SALONESNETWORK.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsuntoPaisSeccionSubsController : ControllerBase
    {
        private readonly IAsuntoPaisSeccionSubService _AsuntoPaisSeccionSubService;

        public AsuntoPaisSeccionSubsController(IAsuntoPaisSeccionSubService AsuntoPaisSeccionSubService)
        {
            _AsuntoPaisSeccionSubService = AsuntoPaisSeccionSubService;
        }

        // GET: api/AsuntoPaisSeccionSub
        [HttpGet("GetAsuntoPaisSeccionSubs")]
        public async Task<ActionResult<IEnumerable<AsuntoPaisSeccionSub>>> GetAsuntoPaisSeccionSubs()
        {
            //return await _context.AsuntoPaisSeccionSubs.ToListAsync();
            IQueryable<AsuntoPaisSeccionSub> queryContactoSQL = await _AsuntoPaisSeccionSubService.ObtenerTodos();

            List<AsuntoPaisSeccionSubDTO> lista = queryContactoSQL
                                                     .Select(c => new AsuntoPaisSeccionSubDTO()
                                                     {
                                                         Id = c.Id,
                                                         Id_Asunto = c.Id_Asunto,
                                                         Id_Pais = c.Id_Pais,
                                                         Id_Seccion = c.Id_Seccion,
                                                         Id_SubSeccion = c.Id_SubSeccion
                                                     }).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        // GET: api/AsuntoPaisSeccionSub/5
        [HttpGet("GetAsuntoPaisSeccionSubById")]
        public async Task<ActionResult<AsuntoPaisSeccionSub>> GetAsuntoPaisSeccionSubById(int id)
        {
            // Llama al servicio para obtener el registro por ID
            var AsuntoPaisSeccionSub = await _AsuntoPaisSeccionSubService.ObtenerPorId(id);

            // Verifica si el resultado es nulo
            if (AsuntoPaisSeccionSub == null)
            {
                return NotFound(new { mensaje = "El país no fue encontrado." });
            }

            // Convierte la entidad a DTO
            var AsuntoPaisSeccionSubDTO = new AsuntoPaisSeccionSubDTO
            {
                Id = AsuntoPaisSeccionSub.Id,
                Id_Asunto = AsuntoPaisSeccionSub.Id_Asunto,
                Id_Pais = AsuntoPaisSeccionSub.Id_Pais,
                Id_Seccion = AsuntoPaisSeccionSub.Id_Seccion,
                Id_SubSeccion = AsuntoPaisSeccionSub.Id_SubSeccion
            };

            // Retorna el DTO con un status 200
            return Ok(AsuntoPaisSeccionSubDTO);
        }

        // PUT: api/AsuntoPaisSeccionSub/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutAsuntoPaisSeccionSub")]
        public async Task<IActionResult> PutAsuntoPaisSeccionSub(AsuntoPaisSeccionSubDTO modelo)
        {
            // Buscar el modelo existente en la base de datos
            var AsuntoPaisSeccionSubExistente = await _AsuntoPaisSeccionSubService.ObtenerPorId(modelo.Id);

            if (AsuntoPaisSeccionSubExistente == null)
                return NotFound(new { mensaje = "El país no existe." });

            // Actualizar solo las propiedades del modelo que tienen datos en el DTO
            AsuntoPaisSeccionSubExistente.Id_Asunto = modelo.Id_Asunto ?? AsuntoPaisSeccionSubExistente.Id_Asunto;
            AsuntoPaisSeccionSubExistente.Id_Pais = modelo.Id_Pais ?? AsuntoPaisSeccionSubExistente.Id_Pais;
            AsuntoPaisSeccionSubExistente.Id_Seccion = modelo.Id_Seccion ?? AsuntoPaisSeccionSubExistente.Id_Seccion;
            AsuntoPaisSeccionSubExistente.Id_SubSeccion = modelo.Id_SubSeccion ?? AsuntoPaisSeccionSubExistente.Id_SubSeccion;

            // Realizar la actualización
            bool respuesta = await _AsuntoPaisSeccionSubService.Actualizar(AsuntoPaisSeccionSubExistente);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
        }

        // POST: api/AsuntoPaisSeccionSub
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostAsuntoPaisSeccionSub")]
        public async Task<IActionResult> PostAsuntoPaisSeccionSub(AsuntoPaisSeccionSubDTO modelo)
        {

            AsuntoPaisSeccionSub NuevoModelo = new AsuntoPaisSeccionSub()
            {
                Id_Asunto = modelo.Id_Asunto,
                Id_Pais = modelo.Id_Pais,
                Id_Seccion = modelo.Id_Seccion,
                Id_SubSeccion = modelo.Id_SubSeccion
            };

            bool respuesta = await _AsuntoPaisSeccionSubService.Insertar(NuevoModelo);

            return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });

        }

        // DELETE: api/AsuntoPaisSeccionSub/5
        [HttpDelete("DeleteAsuntoPaisSeccionSub")]
        public async Task<IActionResult> DeleteAsuntoPaisSeccionSub(int id)
        {
            var AsuntoPaisSeccionSub = await _AsuntoPaisSeccionSubService.ObtenerPorId(id);
            if (AsuntoPaisSeccionSub == null)
            {
                return NotFound();
            }

            await _AsuntoPaisSeccionSubService.Eliminar(id);
            //await _AsuntoPaisSeccionSubService.SaveChangesAsync();

            return NoContent();
        }
    }
}
