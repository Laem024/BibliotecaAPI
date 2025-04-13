using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.Full_Nombre, config => 
                    config.MapFrom(autor => MappearNombreYApellidoAutor(autor)));

            CreateMap<Autor, AutorConLibrosDTO>()
                .ForMember(dto => dto.Full_Nombre, config =>
                    config.MapFrom(autor => MappearNombreYApellidoAutor(autor)));

            CreateMap<AutorCreacionDTO, Autor>();

            CreateMap<Autor, AutorPatchDTO>().ReverseMap();

            CreateMap<Libro, LibroDTO>();

            CreateMap<LibroCreacionDTO, Libro>();

            CreateMap<Libro, LibroConAutorDTO>()
                .ForMember(dto => dto.AutorNombre, config =>
                    config.MapFrom(ent => MappearNombreYApellidoAutor(ent.Autor!)));
        }

        private string MappearNombreYApellidoAutor(Autor autor) => $"{autor.Nombres} {autor.Apellidos}";
    }
}
