using System.Text.Json;
using Bigamer.Application.Common.Exceptions.Abstractions;
using Bigamer.Application.Extensions;
using Bigamer.Infrastructure.Extensions;
using Bigamer.Infrastructure.Models;
using Bigamer.Persistence.Extensions;
using Bigamer.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExceptionHandlingMiddleware>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(builder.Configuration.GetSection("EmailSettings").Get<EmailSenderConfiguration>()!);

builder.Services.AddApplicationLayer()
    .AddPersistenceLayer(builder.Configuration)
    .AddInfrastructureLayer();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetRequiredService<EmailSenderConfiguration>();
Console.WriteLine("----------------------------------------------------------------------");
Console.WriteLine(JsonSerializer.Serialize(service));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();