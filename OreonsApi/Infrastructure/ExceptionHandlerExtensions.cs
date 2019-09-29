using Microsoft.AspNetCore.Builder;

namespace OreonsApi.Infrastructure
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandler>();
        }
    }
}
