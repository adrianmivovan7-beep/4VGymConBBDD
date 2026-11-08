using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class ActivityTypeCreateDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "4|El nombre del tipo de actividad es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("number-monitors")]
        [Range(1, 20, ErrorMessage = "6|El numero de monitores debe estar entre 1 y 20.")]
        public int NumberMonitors { get; set; } = 1;
    }

}
