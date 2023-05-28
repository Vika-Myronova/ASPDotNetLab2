using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;

namespace AspDotNetLab2
{
    public class ServicesListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceCollection _services;

        public ServicesListMiddleware(RequestDelegate next, IServiceCollection services)
        {
            _next = next;
            _services = services;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sb = new StringBuilder();
            sb.Append("<h1>Усі сервіси</h1><table>");
            sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реалізація</th></tr>");
            foreach (var svc in _services)
            {
                sb.Append("<tr>");
                sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                sb.Append($"<td>{svc.Lifetime}</td>");
                sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(sb.ToString());
        }
    }
}
