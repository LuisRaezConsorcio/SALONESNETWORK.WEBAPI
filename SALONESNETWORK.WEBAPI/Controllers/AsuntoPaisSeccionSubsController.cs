using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SALONESNETWORK.WEBAPI.DTOs;
using SALONESNETWORK.BLL.Interfaces;
using SALONESNETWORK.DAL.Data;
using SALONESNETWORK.MODELS.Entities;
using SALONESNETWORK.BLL.Helpers;

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

                return ResponseHelper.Success(lista, "Registro obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");
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

                return ResponseHelper.Success(asuntoPaisSeccionSubDTO, "Registro obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");
                }

                asuntoPaisSeccionSubExistente.Id_Asunto = modelo.Id_Asunto ?? asuntoPaisSeccionSubExistente.Id_Asunto;
                asuntoPaisSeccionSubExistente.Id_Pais = modelo.Id_Pais ?? asuntoPaisSeccionSubExistente.Id_Pais;
                asuntoPaisSeccionSubExistente.Id_Seccion = modelo.Id_Seccion ?? asuntoPaisSeccionSubExistente.Id_Seccion;
                asuntoPaisSeccionSubExistente.Id_SubSeccion = modelo.Id_SubSeccion ?? asuntoPaisSeccionSubExistente.Id_SubSeccion;
                asuntoPaisSeccionSubExistente.Estado = modelo.Estado ?? asuntoPaisSeccionSubExistente.Estado;

                bool respuesta = await _asuntoPaisSeccionSubService.Actualizar(asuntoPaisSeccionSubExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el registro.");
                }

                return ResponseHelper.Success("Registro actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    Estado = true
                };

                bool respuesta = await _asuntoPaisSeccionSubService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo ingresar el registro.");
                }

                return ResponseHelper.Success("Registro creado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
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
                    return ResponseHelper.NotFoundResponse("El registro no fue encontrado.");
                }

                bool respuesta = await _asuntoPaisSeccionSubService.Eliminar(id);


                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el registro.");
                }

                return ResponseHelper.Success("Registro eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
