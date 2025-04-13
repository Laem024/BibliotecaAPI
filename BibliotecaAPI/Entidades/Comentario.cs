using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaAPI.Entidades
{
    [Table("comentarios")]
    public class Comentario
    {
        [Key]
        [Required]
        [Column("id_comentario")]
        public Guid Id { get; set; }

        [Required]
        [Column("cuerpo_comentario")]
        public required string Cuerpo { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Column("fecha_publicacion_comentario")]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        [Column("id_libro")]
        public int LibroId { get; set; }

        public Libro? Libro { get; set; }
    }
}
