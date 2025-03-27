using System.Text.Json.Serialization;
using BibliotecaAPI.Datos;
using BibliotecaAPI.Ejemplos;
using BibliotecaAPI.Middlewares;
using BibliotecaAPI.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Inicio del area de servicios

builder.Services.AddControllers().AddJsonOptions(opciones => 
    opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDBContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddTransient<IRepositorioValores, RepositorioValores>();

//ejemplo tiempos de vida
builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

// Fin del area de servicios

var app = builder.Build();

// Inicio del area de middlewares

app.UseLogueaPeticion();

app.UseAccesoRestringido();

app.MapControllers();

// Inicio del area de middlewares

app.Run();
