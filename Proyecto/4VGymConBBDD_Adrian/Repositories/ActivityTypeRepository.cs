using _4VGymEF.Data;
using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _gymDbContext;

        public ActivityTypeRepository(GymDbContext gymDbContext)
        {
            _gymDbContext = gymDbContext;
        }

        public async Task<List<ActivityType>> GetAll()
        {
            return await _gymDbContext.ActivityTypes.ToListAsync();
        }



        /// <summary>
        /// Guarda un nuevo tipo de actividad generando su ID de forma automática. (Autonumerico)
        /// </summary>
        /// <param name="activityType">Objeto con los datos a guardar (sin ID o con ID no asignado).</param>
        /// <returns>La entidad guardada con su ID autogenerado.</returns>
        public ActivityType Add(ActivityType activityType)
        {
            return activityType;
        }

        // Método para obtener todos los tipos de actividades por Nombre
        public async Task<List<ActivityType>> GetByName(string name)
        {
            return await _gymDbContext.ActivityTypes.Where(act => act.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<ActivityType> GetById(int? id)
        {
            return _gymDbContext.ActivityTypes.SingleOrDefault(act => act.Id == id);
        }


    }

}
