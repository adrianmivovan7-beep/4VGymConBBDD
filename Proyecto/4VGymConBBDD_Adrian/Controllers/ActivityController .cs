using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityRepository _repository;

        public ActivityController(ActivityRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ActivityCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ActivityCreateDto>> Validate(
            [FromBody] ActivityCreateDto activity)
        {
            try
            {
                List<ErrorResponse> errors =
                    await _repository.ValidateNew(activity);

                if (errors.Count > 0)
                {
                    return BadRequest(errors);
                }

                return Ok(activity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, "Se ha producido un error interno del servidor."));
            }
        }
    }
}