using Microsoft.AspNetCore.Mvc;
using CalculadoraWebAPI.Models;
using CalculadoraWebAPI.Services;
using System.Collections.Generic;

namespace CalculadoraWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CalculosController : ControllerBase
    {
        private readonly CalculoService _calculoService;

        public CalculosController(CalculoService calculoService)
        {
            _calculoService = calculoService;
        }


        [HttpGet]
        public ActionResult<ApiResponse<List<Calculo>>> GetTodosLosCalculos()
        {
            try
            {
                var calculos = _calculoService.ObtenerTodosLosCalculos();
                return Ok(new ApiResponse<List<Calculo>>
                {
                    Success = true,
                    Count = calculos.Count,
                    Data = calculos
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Success = false,
                    Message = "Error al obtener los cálculos",
                    Error = ex.Message
                });
            }
        }


        [HttpGet("sumas")]
        public ActionResult<ApiResponse<List<Calculo>>> GetSumas()
        {
            try
            {
                var sumas = _calculoService.ObtenerSumas();
                return Ok(new ApiResponse<List<Calculo>>
                {
                    Success = true,
                    Count = sumas.Count,
                    Data = sumas
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Success = false,
                    Message = "Error al obtener las sumas",
                    Error = ex.Message
                });
            }
        }


        [HttpGet("restas")]
        public ActionResult<ApiResponse<List<Calculo>>> GetRestas()
        {
            try
            {
                var restas = _calculoService.ObtenerRestas();
                return Ok(new ApiResponse<List<Calculo>>
                {
                    Success = true,
                    Count = restas.Count,
                    Data = restas
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Success = false,
                    Message = "Error al obtener las restas",
                    Error = ex.Message
                });
            }
        }


        [HttpGet("multiplicaciones")]
        public ActionResult<ApiResponse<List<Calculo>>> GetMultiplicaciones()
        {
            try
            {
                var multiplicaciones = _calculoService.ObtenerMultiplicaciones();
                return Ok(new ApiResponse<List<Calculo>>
                {
                    Success = true,
                    Count = multiplicaciones.Count,
                    Data = multiplicaciones
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Success = false,
                    Message = "Error al obtener las multiplicaciones",
                    Error = ex.Message
                });
            }
        }


        [HttpGet("divisiones")]
        public ActionResult<ApiResponse<List<Calculo>>> GetDivisiones()
        {
            try
            {
                var divisiones = _calculoService.ObtenerDivisiones();
                return Ok(new ApiResponse<List<Calculo>>
                {
                    Success = true,
                    Count = divisiones.Count,
                    Data = divisiones
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Success = false,
                    Message = "Error al obtener las divisiones",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("recientes")]
        public ActionResult<ApiResponse<List<Calculo>>> GetCalculosRecientes([FromQuery] int dias = 7)
        {
            try
            {
                var recientes = _calculoService.ObtenerCalculosRecientes(dias);
                return Ok(new ApiResponse<List<Calculo>>
                {
                    Success = true,
                    Message = $"Últimos {dias} días",
                    Count = recientes.Count,
                    Data = recientes
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Success = false,
                    Message = "Error al obtener cálculos recientes",
                    Error = ex.Message
                });
            }
        }


        [HttpPost]
        public ActionResult<ApiResponse<Calculo>> PostCalculo([FromBody] CalculoRequest request)
        {
            try
            {
                if (!EsOperacionValida(request.Operacion))
                {
                    return BadRequest(new ErrorResponse
                    {
                        Success = false,
                        Message = "Operación no válida. Use: +, -, x, /, ^, √"
                    });
                }

                double resultado = CalcularResultado(request.Valor1, request.Valor2, request.Operacion);

                var calculoGuardado = _calculoService.GuardarCalculo(
                    request.Valor1,
                    request.Valor2,
                    request.Operacion,
                    resultado
                );

                return Created($"api/calculos/{calculoGuardado.Id}", new ApiResponse<Calculo>
                {
                    Success = true,
                    Message = "Cálculo guardado exitosamente",
                    Data = calculoGuardado
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Success = false,
                    Message = "Error al guardar el cálculo",
                    Error = ex.Message
                });
            }
        }

        private bool EsOperacionValida(string operacion)
        {
            return operacion == "+" || operacion == "-" || operacion == "x" || 
                   operacion == "/" || operacion == "^" || operacion == "√";
        }

        private double CalcularResultado(double valor1, double valor2, string operacion)
        {
            return operacion switch
            {
                "+" => valor1 + valor2,
                "-" => valor1 - valor2,
                "x" => valor1 * valor2,
                "/" => valor2 != 0 ? valor1 / valor2 : throw new System.Exception("División por cero"),
                "^" => System.Math.Pow(valor1, valor2),
                "√" => System.Math.Sqrt(valor1),
                _ => throw new System.Exception("Operación no válida")
            };
        }
    }

    public class CalculoRequest
    {
        public double Valor1 { get; set; }
        public double Valor2 { get; set; }
        public string Operacion { get; set; }
    }
}
