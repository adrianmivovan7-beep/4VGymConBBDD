using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace _4VGymEF.Repositories
{
    public class InstructorRepository
    {
        private readonly GymDbContext _dbContext;

        public InstructorRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InstructorCreateDto>> GetAll()
        {
            return await _dbContext.Instructors
                .OrderBy(instructor => instructor.Name)
                .Select(instructor => new InstructorCreateDto
                {
                    Id = instructor.Id,
                    Name = instructor.Name,
                    Email = instructor.Email,
                    PhotoUrl = instructor.PhotoUrl,
                    Phone = instructor.Phone
                })
                .ToListAsync();
        }

        public async Task<InstructorCreateDto?> GetById(int id)
        {
            return await _dbContext.Instructors
                .Where(instructor => instructor.Id == id)
                .Select(instructor => new InstructorCreateDto
                {
                    Id = instructor.Id,
                    Name = instructor.Name,
                    Email = instructor.Email,
                    PhotoUrl = instructor.PhotoUrl,
                    Phone = instructor.Phone
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<InstructorCreateDto>> GetByFirstInitial(string firstInitial)
        {
            return await _dbContext.Instructors
                .Where(instructor => instructor.Name.StartsWith(firstInitial))
                .OrderBy(instructor => instructor.Name)
                .Select(instructor => new InstructorCreateDto
                {
                    Id = instructor.Id,
                    Name = instructor.Name,
                    Email = instructor.Email,
                    PhotoUrl = instructor.PhotoUrl,
                    Phone = instructor.Phone
                })
                .ToListAsync();
        }

        public async Task<List<InstructorCreateDto>> GetByIds(List<int> ids)
        {
            return await _dbContext.Instructors
                .Where(instructor => ids.Contains(instructor.Id))
                .Select(instructor => new InstructorCreateDto
                {
                    Id = instructor.Id,
                    Name = instructor.Name,
                    Email = instructor.Email,
                    PhotoUrl = instructor.PhotoUrl,
                    Phone = instructor.Phone
                })
                .ToListAsync();
        }

        public async Task<ErrorResponse?> ValidateNew(InstructorCreateDto instructor)
        {
            bool emailExists = await _dbContext.Instructors
                .AnyAsync(ins => ins.Email.ToLower() == instructor.Email.Trim().ToLower());

            bool nameExists = await _dbContext.Instructors
                .AnyAsync(item => item.Name.ToLower() == instructor.Name.Trim().ToLower());

            if (emailExists && nameExists)
            {
                return new ErrorResponse(34, "Ya existe un instructor con ese email y nombre");
            }

            return null;
        }
    }
}