using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Repositorios
{
    public interface IRepositorioValores
    {
        IEnumerable<Valor> obtenerValores();
    }
}
