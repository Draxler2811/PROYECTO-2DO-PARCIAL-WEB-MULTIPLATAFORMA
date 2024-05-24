using DealDex.WebSite.Services;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5268);
    options.ListenAnyIP(7292, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReseñaService, ReseñaService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();