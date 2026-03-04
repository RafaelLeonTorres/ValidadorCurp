using CurpValidator.Application.Interfaces;
using CurpValidator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CurpValidatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurpController : ControllerBase
    {
        private readonly ICurpValidatorService _validator;

        public CurpController(ICurpValidatorService validator)
        {
            _validator = validator;
        }

        [HttpPost("validar")]
        [SwaggerOperation(
            Summary = "Valida el CURP contra los datos proporcionados",
            Description = "Compara la CURP recibido con nombre, apellidos, fecha de nacimiento, sexo y nacionalidad. Devuelve una lista de errores si existen inconsistencias; de lo contrario, devuelve un arreglo vacío."
        )]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<string>>> Validar([FromBody] DatosEntrada datos)
        {
            if (datos == null) 
            {
                return BadRequest();
            }

            var errores = await Task.Run(() => _validator.Validar(datos));
            return Ok(errores);
        }
    }
}
