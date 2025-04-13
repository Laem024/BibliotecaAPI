namespace BibliotecaAPI.DTOs
{
    public class LibroConAutorDTO: LibroDTO
    {
        public int AutorID { get; set; }
        public required string AutorNombre { get; set; }
    }
}
