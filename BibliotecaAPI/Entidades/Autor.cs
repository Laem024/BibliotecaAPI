using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaAPI.Validaciones;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Entidades
{
    [Table("autores")]
    public class Autor
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_autor")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [PrimeraLetraMayuscula]
        [Column("nombre_autor")]
        public required string Nombre { get; set; }

        public List<Libro> Libros { get; set; } = new List<Libro>();
    }
}
