using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
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
    public class PaisController : ControllerBase
    {
        //private readonly SalonesDbContext _context;

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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "Países obtenidos exitosamente.", Datos = lista, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener los países.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El país no fue encontrado.", Resultado = false });
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

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "País obtenido exitosamente.", Datos = paisDTO, Resultado = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al obtener el país por ID.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El país no existe.", Resultado = false });
                }

                paisExistente.Nombre = modelo.Nombre ?? paisExistente.Nombre;
                paisExistente.FechaModificacion = DateTime.Now;
                paisExistente.UsuarioModificacion = modelo.UsuarioModificacion ?? paisExistente.UsuarioModificacion;
                paisExistente.Estado = modelo.Estado ?? paisExistente.Estado;

                var respuesta = await _paisService.Actualizar(paisExistente);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo actualizar el pais.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El país fue actualizado exitosamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al actualizar el país.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo insertar el pais.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El país fue creado exitosamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al crear el país.", Error= ex.Message });
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
                    return StatusCode(StatusCodes.Status404NotFound, new { Mensaje = "El país no existe.", Resultado = false });
                }

                bool respuesta = await _paisService.Eliminar(id);

                if (!respuesta)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Mensaje = "No se pudo eliminar el pais.", Resultado = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Mensaje = "El país fue eliminado exitosamente.", Resultado = respuesta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensaje = "Error al eliminar el país.", Error= ex.Message });
            }
        }
    }
}
