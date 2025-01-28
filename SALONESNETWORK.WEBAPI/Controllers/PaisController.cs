using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
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
    public class PaisController : ControllerBase
    {

        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        // GET: api/Pais
        [HttpGet("GetPaises")]
        public async Task<IActionResult> GetPaises()
        {
            try
            {
                var queryContactoSQL = await _paisService.ObtenerTodos();

                var lista = queryContactoSQL
                    .Select(c => new PaisDTO
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        FechaCreacion = c.FechaCreacion,
                        UsuarioCreacion = c.UsuarioCreacion,
                        FechaModificacion = c.FechaModificacion,
                        UsuarioModificacion = c.UsuarioModificacion,
                        Estado = c.Estado,
                    }).ToList();

                return ResponseHelper.Success(lista, "Países obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // GET: api/Pais/5
        [HttpGet("GetPaisById")]
        public async Task<IActionResult> GetPaisById(int id)
        {
            try
            {
                var pais = await _paisService.ObtenerPorId(id);

                if (pais == null)
                {
                    return ResponseHelper.NotFoundResponse("El país no fue encontrado.");
                }

                var paisDTO = new PaisDTO
                {
                    Id = pais.Id,
                    Nombre = pais.Nombre,
                    FechaCreacion = pais.FechaCreacion,
                    UsuarioCreacion = pais.UsuarioCreacion,
                    FechaModificacion = pais.FechaModificacion,
                    UsuarioModificacion = pais.UsuarioModificacion,
                    Estado = pais.Estado
                };

                return ResponseHelper.Success(paisDTO, "País obtenido correctamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // PUT: api/Pais/5
        [HttpPut("PutPais")]
        public async Task<IActionResult> PutPais(PaisDTO modelo)
        {
            try
            {
                var paisExistente = await _paisService.ObtenerPorId(modelo.Id);

                if (paisExistente == null)
                {
                    return ResponseHelper.NotFoundResponse("El país no fue encontrado.");
                }

                paisExistente.Nombre = modelo.Nombre ?? paisExistente.Nombre;
                paisExistente.FechaModificacion = DateTime.Now;
                paisExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? paisExistente.UsuarioModificacion;
                paisExistente.Estado = modelo.Estado ?? paisExistente.Estado;

                var respuesta = await _paisService.Actualizar(paisExistente);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo actualizar el pais.");
                }

                return ResponseHelper.Success("El país fue actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // POST: api/Pais
        [HttpPost("PostPais")]
        public async Task<IActionResult> PostPais(PaisDTO modelo)
        {
            try
            {
                var nuevoModelo = new Pais
                {
                    Nombre = modelo.Nombre,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = modelo.UsuarioCreacion ?? 1,
                    Estado = true
                };

                var respuesta = await _paisService.Insertar(nuevoModelo);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo insertar el pais.");
                }

                return ResponseHelper.Success("El país fue creado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }

        // DELETE: api/Pais/5
        [HttpDelete("DeletePais")]
        public async Task<IActionResult> DeletePais(int id)
        {
            try
            {
                var pais = await _paisService.ObtenerPorId(id);

                if (pais == null)
                {
                    return ResponseHelper.NotFoundResponse("El país no fue encontrado.");
                }

                bool respuesta = await _paisService.Eliminar(id);

                if (!respuesta)
                {
                    return ResponseHelper.BadRequestResponse("No se pudo eliminar el pais.");
                }

                return ResponseHelper.Success("El país fue eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(ex);
            }
        }
    }
}
