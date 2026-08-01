using Microsoft.EntityFrameworkCore;
using CarLoc.Data;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados
builder.Services.AddDbContext<CarLocContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("CarLocContext")
    )
);

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// IMPORTANTE:
// Não usamos UseHttpsRedirection() no Render.
// O HTTPS é tratado pelo próprio Render.

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
