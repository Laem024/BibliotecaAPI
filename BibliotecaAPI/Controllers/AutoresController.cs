using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDBContext context, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        [HttpGet("/listado-de-autores")]
        public async Task<IEnumerable<Autor>> Get()
        {
            //return new List<Autor>
            //{
            //    new Autor{Id = 1, Nombre= "Miguel de Cervantes"},
            //    new Autor{Id = 1, Nombre= "Franz Kafka"}
            //};

            logger.LogTrace("Obteniendo el listado de autores.");
            logger.LogDebug("Obteniendo el listado de autores.");
            logger.LogInformation("Obteniendo el listado de autores.");
            logger.LogWarning("Obteniendo el listado de autores.");
            logger.LogError("Obteniendo el listado de autores.");
            logger.LogCritical("Obteniendo el listado de autores.");
            return await context.Autores.ToListAsync();
        }

        [HttpGet("{id:int}")] // api/autores/id?incluirLibros=true
        public async Task<ActionResult<Autor>> GetByID(int id, [FromQuery] bool incluirLibros)
        {
            var autor = await context.Autores
                .Include(l => l.Libros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        [HttpGet("{nombre:alpha}")]
        public async Task<IEnumerable<Autor>> GetNombre(String nombre)
        {
            return await context.Autores.Where(a => a.Nombre.ToUpper().Contains(nombre.ToUpper())).ToListAsync();
        }

        [HttpGet("{parametro1}/{parametro2?}")]
        public ActionResult GetDosParametros(String parametro1, String parametro2 = "Valor por defecto")
        {
            return Ok(new { parametro1, parametro2 });
        }

        [HttpGet("primero")]
        public async Task<Autor> GetPrimerAutor()
        {
            return await context.Autores.FirstAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpPut("{id:int}")] // api/autores/id
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id) 
            {
                return BadRequest("Los IDs deben de coincidir");
            }

            var autorPorActualizar = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if (autorPorActualizar == null)
            {
                return NotFound();
            }

            context.Entry(autorPorActualizar).State = EntityState.Detached;

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var autoresBorrados = await context.Autores.Where(a => a.Id == id).ExecuteDeleteAsync();

            if (autoresBorrados == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
