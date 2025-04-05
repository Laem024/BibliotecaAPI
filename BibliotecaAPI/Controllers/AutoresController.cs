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

        public AutoresController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Autor>> Get()
        {
            //return new List<Autor>
            //{
            //    new Autor{Id = 1, Nombre= "Miguel de Cervantes"},
            //    new Autor{Id = 1, Nombre= "Franz Kafka"}
            //};

            return await context.Autores.ToListAsync();
        }

        [HttpGet("{id:int}")] // api/autores/id
        public async Task<ActionResult<Autor>> GetByID(int id)
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
