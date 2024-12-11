using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tm.Ws.Compania.Prod.Entity;
using Tm.Ws.Compania.Prod.Services;

namespace Tm.Ws.Compania.Prod.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniaController : ControllerBase
    {
        private readonly CompaniaService _companiaService;
        

        public CompaniaController(CompaniaService companiaService)
        {
            _companiaService = companiaService;
        }

        // Obtener todas las compañías
        [HttpGet]
        public ActionResult<List<CompaniaEntity>> ObtenerCompanias()
        {
            var companias = _companiaService.ObtenerCompanias();
            if (companias == null || companias.Count == 0)
            {
                return NotFound("No se encontraron compañías.");
            }
            return Ok(companias);
        }

        // Obtener detalles de una compañía específica por su código
        [HttpGet("{codCompania}")]
        public ActionResult<CompaniaEntity> ObtenerDetalleCompania(string codCompania)
        {
            var compania = _companiaService.ObtenerDetalleCompanias(codCompania);
            if (compania == null || compania.Count == 0)
            {
                return NotFound($"No se encontró la compañía con el código {codCompania}.");
            }
            return Ok(compania);
        }

        // Crear una nueva compañía
        [HttpPost]
        public ActionResult CrearCompania([FromBody] CompaniaEntity compania)
        {
            if (compania == null)
            {
                return BadRequest("La compañía no puede ser nula.");
            }

            _companiaService.CrearCompania(compania);
            return CreatedAtAction(nameof(ObtenerDetalleCompania), new { codCompania = compania.CodCompania }, compania);
        }

        // Actualizar una compañía existente
        [HttpPut]
        public ActionResult ActualizarCompania([FromBody] CompaniaEntity compania)
        {
            if (compania == null)
            {
                return BadRequest("La compañía no puede ser nula.");
            }

            _companiaService.ActualizarCompania(compania);
            return NoContent(); // Devuelve 204 No Content
        }

        // Eliminar una compañía por su código
        [HttpDelete("{codCompania}")]
        public ActionResult EliminarCompania(string codCompania)
        {
            bool eliminado = _companiaService.EliminarCompania(codCompania);
            if (!eliminado)
            {
                return NotFound($"No se encontró la compañía con el código {codCompania}.");
            }
            return NoContent(); // Devuelve 204 No Content
        }
    }
}
