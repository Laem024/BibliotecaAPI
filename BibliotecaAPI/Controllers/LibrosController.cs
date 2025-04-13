using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
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
        private readonly IMapper mapper;

        public LibrosController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();

            var libroDTO = mapper.Map<IEnumerable<LibroDTO>>(libros);

            return libroDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroConAutorDTO>> GetById(int id)
        {
            var libro = await context.Libros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            var libroDTO = mapper.Map<LibroConAutorDTO>(libro);

            return Ok(libroDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            var existeAutor = await context.Autores.AnyAsync(a => a.Id == libro.AutorId);

            if (existeAutor == false)
            {
                ModelState.AddModelError(nameof(libro.AutorId), $"El autor de id {libro.AutorId}, no existe");
                return ValidationProblem();
            }

            context.Add(libro);
            await context.SaveChangesAsync();

            var libroDTO = mapper.Map<LibroDTO>(libro);


            return CreatedAtRoute("ObtenerLibro", new { libro.Id } , libroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libro = mapper.Map<Libro>(libroCreacionDTO);

            libro.Id = id;

            var libroPorActualizar = await context.Libros.FirstOrDefaultAsync(l => l.Id == id);

            if (libroPorActualizar == null)
            {
                return NotFound();
            }

            context.Entry(libroPorActualizar).State = EntityState.Detached;

            var existeAutor = await context.Autores.AnyAsync(a => a.Id == libro.AutorId);

            if (existeAutor == false)
            {
                ModelState.AddModelError(nameof(libro.AutorId), $"El autor de id {libro.AutorId}, no existe");
                return ValidationProblem();
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var librosBorrados = await context.Libros.Where(l =>  l.Id == id).ExecuteDeleteAsync();

            if (librosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
