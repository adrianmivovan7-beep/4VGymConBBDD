using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("instructors")]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorRepository _repository;

        public InstructorsController(InstructorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Obtener todos o filtrar por Nombre o Id")]
        [ProducesResponseType(typeof(List<InstructorCreateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<InstructorCreateDto>>> GetAll(
            [FromQuery] int? id,
            [FromQuery(Name = "first_initial")] string? firstInitial)
        {
            try
            {
                if (id.HasValue)
                {
                    InstructorCreateDto? instructor =
                        await _repository.GetById(id.Value);

                    if (instructor == null)
                    {
                        return NotFound(new ErrorResponse(
                            2,
                            $"No existe el instructor con ID {id.Value}."));
                    }

                    return Ok(new List<InstructorCreateDto> { instructor });
                }

                if (!string.IsNullOrWhiteSpace(firstInitial))
                {
                    return Ok(await _repository.GetByFirstInitial(firstInitial));
                }

                return Ok(await _repository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError,"Se ha producido un error interno del servidor."));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(InstructorCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InstructorCreateDto>> Validate(
            [FromBody] InstructorCreateDto instructor)
        {
            try
            {
                ErrorResponse? error =
                    await _repository.ValidateNew(instructor);

                if (error != null)
                {
                    return BadRequest(error);
                }

                return Ok(instructor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, "Se ha producido un error interno del servidor."));
            }
        }
    }
}