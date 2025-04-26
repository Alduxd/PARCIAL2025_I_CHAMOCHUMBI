using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FutbolPeruano.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

// Configurar la base de datos
// Para desarrollo local
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<FutbolPeruanoContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    // Para producción - utiliza la cadena de conexión de PostgreSQL en Render
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    builder.Services.AddDbContext<FutbolPeruanoContext>(options =>
        options.UseNpgsql(connectionString));
}

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Migrate database on startup in production
if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<FutbolPeruanoContext>();
        db.Database.Migrate();
    }
}

app.Run();
