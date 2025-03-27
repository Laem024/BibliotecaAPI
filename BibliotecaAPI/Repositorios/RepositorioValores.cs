using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Repositorios
{
    public class RepositorioValores : IRepositorioValores
    {
        public IEnumerable<Valor> obtenerValores()
        {
            return new List<Valor>
            {
                new Valor{ Id = 1, Nombre = "Valor 1"},
                new Valor{ Id = 2, Nombre = "Valor 2"}
            };
        }
    }
}
