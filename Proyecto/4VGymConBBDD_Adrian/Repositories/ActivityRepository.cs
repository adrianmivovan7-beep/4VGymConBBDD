using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Repositories
{
    public class ActivityRepository
    {
        private readonly GymDbContext _dbContext;

        public ActivityRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ErrorResponse>> ValidateNew(ActivityCreateDto activity)
        {
            var errors = new List<ErrorResponse>();

            bool activityTypeExists = await _dbContext.ActivityTypes
                .AnyAsync(type => type.Id == activity.ActivityTypeId);

            if (!activityTypeExists)
            {
                errors.Add(new ErrorResponse(34, "El tipo de actividad indicado no existe."));
            }

            List<int> existingInstructorIds = await _dbContext.Instructors
                .Select(instructor => instructor.Id)
                .ToListAsync();

            List<int> missingIds = activity.Instructors.Select(instructor => instructor.Id)
                .Except(existingInstructorIds)
                .ToList();

            errors.AddRange(missingIds.Select(id =>
                new ErrorResponse(35, $"El instructor con ID {id} no existe.")));

            return errors;
        }

    }
}