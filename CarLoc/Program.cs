using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarLoc.Data;
var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<CarLocContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("CarLocContext")));
}
else
{
     builder.Services.AddDbContext<CarLocContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionCarLocContext")));
}


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
