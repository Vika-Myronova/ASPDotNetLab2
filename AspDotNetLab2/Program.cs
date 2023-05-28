using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AspDotNetLab2;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TimerService>();
builder.Services.AddTransient<RandomService>();
builder.Services.AddTransient<GeneralCounterService>();
var app = builder.Build();
var services = builder.Services;


app.MapGet("/", () => "Hello World!");
app.UseMiddleware<GeneralCounterMiddleware>();

app.UseRouting();


app.MapGet("/services/list", async context =>
{
    var services = builder.Services;
    var middleware = new ServicesListMiddleware(null, services);
    await middleware.InvokeAsync(context);
});

app.MapGet("/services/timer", async context =>
{
    var timerService = app.Services.GetService<TimerService>();
    await context.Response.WriteAsync(
           $"Date and Time: {timerService?.GetCurrentDateTime()}");
});

app.MapGet("/services/random", async context =>
{
    //var timerService = app.Services.GetService<TimerService>();
    var randomNumber = app.Services.GetService<RandomService>();
    var num = randomNumber?.GetRandomNumber();
    await context.Response.WriteAsync(
           $"Random number: 1 - {num} 2 - {num}");
});

app.Map("/services/general-counter", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var counterService = context.RequestServices.GetRequiredService<GeneralCounterService>();
        var counters = counterService.GetCounters();

        await context.Response.WriteAsync("Counters:\n");
        foreach (var counter in counters)
        {
            await context.Response.WriteAsync($"{counter.Key}: {counter.Value}\n");
        }
    });
});

app.Run();
