using Dapper.Contrib.Extensions;
using DealDex.Api.DataAccess;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Api.Repositories;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Repositories.Carrito;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Reseñas;
using Tecnm.Ecommerce1.Api.Repositories.Reseñas;
using Tecnm.Ecommerce1.Api.Services.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Reseñas;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Users;
using Tecnm.Ecommerce1.Api.Services.Reseñas;
using Tecnm.Ecommerce1.Api.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<IProductCategoryRepository, InMemoryProductCategoryRepositoty>();

//patron de diseño 
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepositoty>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryServices>();
builder.Services.AddScoped<IDbContext, DbContext>();

builder.Services.AddScoped<IUsersCategoryRepository, UsersCategoryRepository>();
builder.Services.AddScoped<IUsersCategoryService, UsersCategoryService>();

builder.Services.AddScoped<ICarritoCategoryReposioty, CarritoCategoryReposioty>();
builder.Services.AddScoped<ICarritoCategoryServices, CarritoCategoryServices>();

builder.Services.AddScoped<IReseñaCategoryRepository, ReseñaCategoryRepository>();
builder.Services.AddScoped<IReseñasCategoryServices, ReseñasCategoryServices>();

var app = builder.Build();

SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.ToString();
    if (name.Contains("DealDex.Core.Entities."))
        name = name.Replace("DealDex.Core.Entities.", "");
    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string(letters);
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();