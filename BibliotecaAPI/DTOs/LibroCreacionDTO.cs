using BibliotecaAPI.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class LibroCreacionDTO
    {
        [Required(ErrorMessage = "El campo {1} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "El campo {1} es requerido")]
        public required int AutorId { get; set; }
    }
}
