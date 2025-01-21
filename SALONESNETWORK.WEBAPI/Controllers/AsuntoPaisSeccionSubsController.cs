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
        private readonly IAsuntoPaisSeccionSubService _asuntoPaisSeccionSubService;

        public AsuntoPaisSeccionSubsController(IAsuntoPaisSeccionSubService asuntoPaisSeccionSubService)
        {
            _asuntoPaisSeccionSubService = asuntoPaisSeccionSubService;
        }

        // GET: api/AsuntoPaisSeccionSub
        [HttpGet("GetAsuntoPaisSeccionSubs")]
        public async Task<IActionResult> GetAsuntoPaisSeccionSubs()
        {
            try
            {
                var queryContactoSQL = await _asuntoPaisSeccionSubService.ObtenerTodos();

                var lista = queryContactoSQL
                    .Select(c => new AsuntoPaisSeccionSubDTO
                    {
                        Id = c.Id,
                        Id_Asunto = c.Id_Asunto,
                        Id_Pais = c.Id_Pais,
                        Id_Seccion = c.Id_Seccion,
                        Id_SubSeccion = c.Id_SubSeccion,
                        Estado = c.Estado
                    }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { Mensaje =  "Datos obtenidos correctamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener los datos.", Error = ex.Message });
            }
        }

        // GET: api/AsuntoPaisSeccionSub/5
        [HttpGet("GetAsuntoPaisSeccionSubById")]
        public async Task<IActionResult> GetAsuntoPaisSeccionSubById(int id)
        {
            try
            {
                var asuntoPaisSeccionSub = await _asuntoPaisSeccionSubService.ObtenerPorId(id);

                if (asuntoPaisSeccionSub == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El registro no fue encontrado.", Resultado = false });
                }

                var asuntoPaisSeccionSubDTO = new AsuntoPaisSeccionSubDTO
                {
                    Id = asuntoPaisSeccionSub.Id,
                    Id_Asunto = asuntoPaisSeccionSub.Id_Asunto,
                    Id_Pais = asuntoPaisSeccionSub.Id_Pais,
                    Id_Seccion = asuntoPaisSeccionSub.Id_Seccion,
                    Id_SubSeccion = asuntoPaisSeccionSub.Id_SubSeccion,
                    Estado = asuntoPaisSeccionSub.Estado
                };

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Registro obtenido correctamente.", Datos = asuntoPaisSeccionSubDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al obtener el registro.", Error = ex.Message });
            }
        }

        // PUT: api/AsuntoPaisSeccionSub/5
        [HttpPut("PutAsuntoPaisSeccionSub")]
        public async Task<IActionResult> PutAsuntoPaisSeccionSub(AsuntoPaisSeccionSubDTO modelo)
        {
            try
            {
                var asuntoPaisSeccionSubExistente = await _asuntoPaisSeccionSubService.ObtenerPorId(modelo.Id);

                if (asuntoPaisSeccionSubExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El registro no existe.", Resultado = false });
                }

                asuntoPaisSeccionSubExistente.Id_Asunto = modelo.Id_Asunto ?? asuntoPaisSeccionSubExistente.Id_Asunto;
                asuntoPaisSeccionSubExistente.Id_Pais = modelo.Id_Pais ?? asuntoPaisSeccionSubExistente.Id_Pais;
                asuntoPaisSeccionSubExistente.Id_Seccion = modelo.Id_Seccion ?? asuntoPaisSeccionSubExistente.Id_Seccion;
                asuntoPaisSeccionSubExistente.Id_SubSeccion = modelo.Id_SubSeccion ?? asuntoPaisSeccionSubExistente.Id_SubSeccion;
                asuntoPaisSeccionSubExistente.Estado = modelo.Estado ?? asuntoPaisSeccionSubExistente.Estado;

                bool respuesta = await _asuntoPaisSeccionSubService.Actualizar(asuntoPaisSeccionSubExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Registro actualizado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al actualizar el registro.", Error = ex.Message });
            }
        }

        // POST: api/AsuntoPaisSeccionSub
        [HttpPost("PostAsuntoPaisSeccionSub")]
        public async Task<IActionResult> PostAsuntoPaisSeccionSub(AsuntoPaisSeccionSubDTO modelo)
        {
            try
            {
                var nuevoModelo = new AsuntoPaisSeccionSub
                {
                    Id_Asunto = modelo.Id_Asunto,
                    Id_Pais = modelo.Id_Pais,
                    Id_Seccion = modelo.Id_Seccion,
                    Id_SubSeccion = modelo.Id_SubSeccion,
                    Estado = modelo.Estado
                };

                bool respuesta = await _asuntoPaisSeccionSubService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo ingresar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Registro creado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al crear el registro.", Error = ex.Message });
            }
        }

        // DELETE: api/AsuntoPaisSeccionSub/5
        [HttpDelete("DeleteAsuntoPaisSeccionSub")]
        public async Task<IActionResult> DeleteAsuntoPaisSeccionSub(int id)
        {
            try
            {
                var asuntoPaisSeccionSub = await _asuntoPaisSeccionSubService.ObtenerPorId(id);

                if (asuntoPaisSeccionSub == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El registro no existe.", Resultado = false });
                }

                bool respuesta = await _asuntoPaisSeccionSubService.Eliminar(id);


                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el registro.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Registro eliminado correctamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Ocurrió un error al eliminar el registro.", Error = ex.Message });
            }
        }
    }
}
