using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("activity-types")]
    public class ActivityTypesController : ControllerBase
    {
        private readonly ActivityTypeRepository _repository;

        public ActivityTypesController(ActivityTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Ver todos ActivityType")]
        [ProducesResponseType(typeof(List<ActivityTypeCreateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ActivityTypeCreateDto>>> GetAll()
        {
            try
            {
                return Ok(await _repository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, "Se ha producido un error interno del servidor."));
            }
        }

        [HttpGet("Obtener por Id")]
        [ProducesResponseType(typeof(ActivityTypeCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ActivityTypeCreateDto>> GetById(int id)
        {
            try
            {
                ActivityTypeCreateDto? activityType =
                    await _repository.GetById(id);

                if (activityType == null)
                {
                    return NotFound(new ErrorResponse(
                        2,
                        $"No existe el tipo de actividad con ID {id}."));
                }

                return Ok(activityType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, "Se ha producido un error interno del servidor."));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ActivityTypeCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ActivityTypeCreateDto>> Validate(
            [FromBody] ActivityTypeCreateDto activityType)
        {
            try
            {
                ErrorResponse? error =
                    await _repository.ValidateNew(activityType);

                if (error != null)
                {
                    return BadRequest(error);
                }

                return Ok(activityType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, "Se ha producido un error interno del servidor."));
            }
        }
    }
}