using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaAPI.Validaciones;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BibliotecaAPI.Entidades
{
    [Table("libros")]
    public class Libro
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_libro")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {1} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [PrimeraLetraMayuscula]
        [Column("titulo_libro")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "El campo {1} es requerido")]
        [Column("id_autor")]
        public required int AutorId {  get; set; }

        public Autor? Autor { get; set; }
        
    }
}
