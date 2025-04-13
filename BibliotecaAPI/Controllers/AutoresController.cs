using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AutorDTO>> Get()
        {
            //return new List<Autor>
            //{
            //    new Autor{Id = 1, Nombre= "Miguel de Cervantes"},
            //    new Autor{Id = 1, Nombre= "Franz Kafka"}
            //};

            var autores = await context.Autores.ToListAsync();
            //var autoresDTO = autores.Select(autor => 
            //    new AutorDTO 
            //    { 
            //        Id = autor.Id, 
            //        Full_Nombre = $"{autor.Nombres} {autor.Apellidos}" 
            //    });

            var autoresDTO = mapper.Map<IEnumerable<AutorDTO>>(autores);

            return autoresDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerAutor")] // api/autores/id
        public async Task<ActionResult<AutorConLibrosDTO>> GetByID(int id)
        {
            var autor = await context.Autores
                .Include(l => l.Libros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorConLibrosDTO>(autor);

            return Ok(autorDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {

            var autor = mapper.Map<Autor>(autorCreacionDTO);

            context.Add(autor);
            await context.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("ObtenerAutor", new {autor.Id}, autorDTO);
        }

        [HttpPut("{id:int}")] // api/autores/id
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO)
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);

            autor.Id = id;

            var autorPorActualizar = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if (autorPorActualizar == null)
            {
                return NotFound();
            }

            context.Entry(autorPorActualizar).State = EntityState.Detached;

            context.Update(autor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<AutorPatchDTO> patchDoc)
        {
            if (patchDoc is null)
            {
                return BadRequest();
            }

            var autorDB = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autorDB is null)
            {
                return NotFound();
            }

            var autorPatchDTO = mapper.Map<AutorPatchDTO>(autorDB);

            patchDoc.ApplyTo(autorPatchDTO, ModelState);

            var esValido = TryValidateModel(autorPatchDTO);

            if (!esValido)
            {
                return ValidationProblem();
            }

            mapper.Map(autorPatchDTO, autorDB);

            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var autoresBorrados = await context.Autores.Where(a => a.Id == id).ExecuteDeleteAsync();

            if (autoresBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
