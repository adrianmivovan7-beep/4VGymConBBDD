using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class InstructorCreateDto
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del instructor es obligatorio.")]
        [StringLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato valido.")]
        [StringLength(150)]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL de la foto es obligatoria.")]
        [Url(ErrorMessage = "La URL de la foto no es valida.")]
        [JsonPropertyName("photo_url")]
        public string PhotoUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "El telefono es obligatorio.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "El telefono debe contener 9 digitos.")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        public bool isEmpty()
        {
            return (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email)
                && string.IsNullOrEmpty(PhotoUrl) && string.IsNullOrEmpty(Phone));
        }


    }
}
