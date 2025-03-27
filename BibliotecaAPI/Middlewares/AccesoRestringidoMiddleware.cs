namespace BibliotecaAPI.Middlewares
{
    public class AccesoRestringidoMiddleware
    {
        private readonly RequestDelegate next;

        public AccesoRestringidoMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            if (contexto.Request.Path == "/bloqueado")
            {
                contexto.Response.StatusCode = 403;
                await contexto.Response.WriteAsync("Acceso denegado");
            }
            else
            {
                await next.Invoke(contexto);
            }
        }
    }

    public static class AccesoRestringidoMiddlewareExtensions
    {
        public static IApplicationBuilder UseAccesoRestringido(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccesoRestringidoMiddleware>();
        }
    }
}
