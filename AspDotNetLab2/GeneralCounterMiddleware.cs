using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspDotNetLab2
{
    public class GeneralCounterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GeneralCounterService _counterService;

        public GeneralCounterMiddleware(RequestDelegate next, GeneralCounterService counterService)
        {
            _next = next;
            _counterService = counterService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Збільшити лічильник передаваної URL-адреси
            _counterService.IncrementCounter(context.Request.Path);

            // Продовжити обробку запиту
            await _next(context);
        }
    }
}
