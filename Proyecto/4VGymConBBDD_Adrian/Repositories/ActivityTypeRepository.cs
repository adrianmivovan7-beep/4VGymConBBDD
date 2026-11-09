using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _dbContext;

        public ActivityTypeRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ActivityTypeCreateDto>> GetAll()
        {
            return await _dbContext.ActivityTypes
                .OrderBy(type => type.Name)
                .Select(type => new ActivityTypeCreateDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    NumberMonitors = type.NumberMonitors
                })
                .ToListAsync();
        }

        public async Task<ActivityTypeCreateDto?> GetById(int id)
        {
            return await _dbContext.ActivityTypes
                .Where(type => type.Id == id)
                .Select(type => new ActivityTypeCreateDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    NumberMonitors = type.NumberMonitors
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<ActivityTypeCreateDto>> GetByName(string name)
        {
            return await _dbContext.ActivityTypes
                .AsNoTracking()
                .Where(type => type.Name.ToLower() == name.Trim().ToLower())
                .Select(type => new ActivityTypeCreateDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    NumberMonitors = type.NumberMonitors
                })
                .ToListAsync();
        }

        public async Task<ErrorResponse?> ValidateNew(ActivityTypeCreateDto activityType)
        {
            bool exists = await _dbContext.ActivityTypes
                .AnyAsync(type => type.Name.ToLower() == activityType.Name.Trim().ToLower());

            if (exists)
            {
                return new ErrorResponse(5, "Ya existe un tipo de actividad con ese nombre.");
            }

            return null;
        }
    }
}
