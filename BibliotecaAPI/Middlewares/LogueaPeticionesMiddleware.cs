using Microsoft.AspNetCore.Builder;

namespace BibliotecaAPI.Middlewares
{
    public class LogueaPeticionesMiddleware
    {
        private readonly RequestDelegate next;

        public LogueaPeticionesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"Petición: {contexto.Request.Method} {contexto.Request.Path}");

            await next.Invoke(contexto);

            logger.LogInformation($"Respuesta: {contexto.Response.StatusCode}");
        }
    }

    public static class LogueaPeticionesMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogueaPeticion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogueaPeticionesMiddleware>();
        }
    }
}
