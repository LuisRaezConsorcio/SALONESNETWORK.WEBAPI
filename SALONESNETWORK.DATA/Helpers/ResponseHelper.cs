using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SALONESNETWORK.MODELS.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SALONESNETWORK.BLL.Helpers
{
    public static class ResponseHelper
    {

        public static IActionResult Success(string mensaje = "Operación exitosa.")
        {
            var apiResponse = new ApiResponse<object>
            {
                Mensaje = mensaje,
                Resultado = true
            };

            return new OkObjectResult(new ApiResponse
            {
                Mensaje = mensaje,
                Resultado = true
            });
        }

        public static IActionResult Success<T>(T datos, string mensaje = "Operación exitosa.")
        {
            return new OkObjectResult(new ApiResponse<T>
            {
                Mensaje = mensaje,
                Resultado = true,
                Datos = datos
            });
        }

        public static IActionResult Error(Exception ex, string mensaje = "Ocurrió un error en la operación.")
        {
            return new ObjectResult(new ApiResponse<string>
            {
                Mensaje = mensaje,
                Resultado = false,
                Datos = ex?.Message // Incluir el mensaje de la excepción
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        public static IActionResult NotFoundResponse(string mensaje)
        {
            return new NotFoundObjectResult(new ApiResponse<object>
            {
                Mensaje = mensaje,
                Resultado = false,
                Datos = null
            });
        }

        public static IActionResult BadRequestResponse(string mensaje)
        {
            return new BadRequestObjectResult(new ApiResponse<object>
            {
                Mensaje = mensaje,
                Resultado = false,
                Datos = null
            });
        }
    }
}
