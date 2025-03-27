using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDBContext context;

        public LibrosController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Libro>> Get()
        {
            return await context.Libros.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> GetById(int id)
        {
            var libro = await context.Libros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null)
            {
                return NotFound($"No existe ningun libro con ID {id}");
            }
            return Ok(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {

            var existeAutor = await context.Autores.AnyAsync(a => a.Id == libro.AutorId);

            if (existeAutor == false)
            {
                ModelState.AddModelError(nameof(libro.AutorId), $"El autor de id {libro.AutorId}, no existe");
                return ValidationProblem();
            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok(libro);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest("Las IDs deben coincidir");
            }

            var libroPorActualizar = await context.Libros.FirstOrDefaultAsync(l => l.Id == id);

            if (libroPorActualizar == null)
            {
                return NotFound($"No existe ningun libro con ID {id}");
            }

            context.Entry(libroPorActualizar).State = EntityState.Detached;

            var existeAutor = await context.Autores.AnyAsync(a => a.Id == libro.AutorId);

            if (existeAutor == false)
            {
                return BadRequest($"El autor de id {libro.AutorId}, no existe");
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok(libro);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var librosBorrados = await context.Libros.Where(l =>  l.Id == id).ExecuteDeleteAsync();

            if (librosBorrados == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
