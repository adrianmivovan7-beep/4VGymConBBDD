using _4VGymEF.Models;

namespace _4VGymEF.Repositories
{
    public class ActivityRepository
    {
        private readonly ActivityTypeRepository _repositoryActivityType;
        private readonly InstructorRepository _repositoryInstructor;

        public ActivityRepository(ActivityTypeRepository repositoryActivityType, InstructorRepository repositoryInstructor)
        {
            _repositoryActivityType = repositoryActivityType;
            _repositoryInstructor = repositoryInstructor;
        }


        public Activity? Add(Activity activity, out List<ErrorResponse> errorResponse)
        {

            errorResponse = new List<ErrorResponse>();

            // Validamos todas los referente a las entidades cruzadas
            // Validación 1: Que exista el tipo de actividad
            if (_repositoryActivityType.GetById(activity.ActivityTypeId) == null)
            {
                errorResponse.Add(new ErrorResponse(34, "EL tipo de actividad debe de funcionar"));
            }
            // Validacion 2: Que existan todos los monitores
            errorResponse =
                activity.InstructorsId
                        .Where(instructorId => _repositoryInstructor.GetById(instructorId) == null)
                        .Select(instructorIdNotFound =>
                        {
                            return new ErrorResponse(35, "El instructor: " + instructorIdNotFound + " no existe");
                        })
                        .ToList();
            if (errorResponse.Count > 0)
            {
                return null;
            }

            return activity;
        }
    }

}
