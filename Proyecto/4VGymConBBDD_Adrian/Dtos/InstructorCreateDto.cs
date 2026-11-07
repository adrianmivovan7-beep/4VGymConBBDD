using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _4VGymEF.Models
{
    public class InstructorCreateDto
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "4|El nombre del monitor es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "4|El nombre debe tener entre 2 y 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "5|El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "5|El email no tiene un formato valido.")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("photo_url")]
        [Required(ErrorMessage = "6|La URL de la foto es obligatoria.")]
        [Url(ErrorMessage = "6|La URL de la foto no es valida.")]
        public string PhotoUrl { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        [Required(ErrorMessage = "7|El telefono es obligatorio.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "7|El telefono debe contener exactamente 9 digitos.")]
        public string Phone { get; set; } = string.Empty;

        public InstructorCreateDto()
        {
        }

        public InstructorCreateDto(int id, string name, string email, string photoUrl, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            PhotoUrl = photoUrl;
            Phone = phone;
        }

        public bool isEmpty()
        {
            return (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Email)
                && string.IsNullOrEmpty(PhotoUrl) && string.IsNullOrEmpty(Phone));
        }


    }
}
