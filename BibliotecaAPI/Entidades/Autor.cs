using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliotecaAPI.Validaciones;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Entidades
{
    [Table("autores")]
    public class Autor : IValidatableObject
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_autor")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        //[PrimeraLetraMayuscula]
        [Column("nombre_autor")]
        public required string Nombre { get; set; }

        //[Range(18, 120)]
        //public int Edad {  get; set; }

        //[CreditCard]
        //public string? TarjetaDeCredito { get; set; }

        //[Url]
        //public string? Url { get; set; }

        public List<Libro> Libros { get; set; } = new List<Libro>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula", 
                        new string[] { nameof( Nombre )});
                }
            }
        }
    }
}
