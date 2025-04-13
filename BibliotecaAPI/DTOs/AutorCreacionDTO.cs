using BibliotecaAPI.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public required string Apellidos { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string? Identificacion { get; set; }
    }
}
