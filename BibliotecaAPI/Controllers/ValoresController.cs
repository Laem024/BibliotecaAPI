using BibliotecaAPI.Ejemplos;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/valores")]
    public class ValoresController : ControllerBase
    {
        private readonly IRepositorioValores repositoriValores;
        private readonly ServicioTransient transient1;
        private readonly ServicioTransient transient2;
        private readonly ServicioScoped scoped1;
        private readonly ServicioScoped scoped2;
        private readonly ServicioSingleton singleton1;

        public ValoresController(IRepositorioValores repositoriValores, 
            ServicioTransient transient1,
            ServicioTransient transient2,
            ServicioScoped scoped1,
            ServicioScoped scoped2,
            ServicioSingleton singleton1
            )
        {
            this.repositoriValores = repositoriValores;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton1 = singleton1;
        }

        [HttpGet("tiempo-de-vida")]
        public IActionResult GetServicioTiempoDeVida() 
        {
            return Ok(new
            {
                Transients = new
                {
                    transient1 = transient1.ObtenerGuid,
                    transient2 = transient2.ObtenerGuid
                },
                Scopeds = new
                {
                    scoped1 = scoped1.ObtenerGuid,
                    scoped2 = scoped2.ObtenerGuid
                },
                Singleton = singleton1.ObtenerGuid
            });
        }

        [HttpGet]
        public IEnumerable<Valor> Get()
        {
            return repositoriValores.obtenerValores();
        }
    }
}
