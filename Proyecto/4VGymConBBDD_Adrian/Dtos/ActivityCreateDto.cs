using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using _4VGymEF.Validations;

namespace _4VGymEF.Models
{
    public class ActivityCreateDto
    {
        [JsonPropertyName("activity_id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "30|El tipo de actividad es obligatorio.")]
        [JsonPropertyName("activity_type_id")]
        public int? ActivityTypeId { get; set; }

        [Range(1, 25, ErrorMessage = "25|El aforo maximo debe estar entre 1 y 25 participantes.")]
        [JsonPropertyName("max_participants")]
        public int MaxParticipants { get; set; }

        [Required(ErrorMessage = "28|La fecha de inicio es obligatoria.")]
        [FutureDate(ErrorMessage = "40|La fecha de inicio debe ser futura.")]
        [JsonPropertyName("date_start")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "29|La fecha de fin es obligatoria.")]
        [DateGreaterThan(nameof(DateStart), ErrorMessage = "41|La fecha de fin debe ser posterior a la fecha de inicio.")]
        [JsonPropertyName("date_end")]
        public DateTime DateEnd { get; set; }

        [JsonPropertyName("activity_type")]
        public ActivityTypeCreateDto? ActivityType { get; set; }

        [JsonPropertyName("instructors")]
        public List<InstructorCreateDto> Instructors { get; set; } = new();

    }

}
