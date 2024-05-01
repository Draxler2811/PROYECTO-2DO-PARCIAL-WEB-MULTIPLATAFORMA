using DealDex.Api.Dto.Carrito;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Carrito;

namespace Tecnm.Ecommerce1.Api.Services.Carrito;

public class CarritoCategoryServices : ICarritoCategoryServices
{
    private readonly ICarritoCategoryReposioty _carritoCategoryReposioty;
    

    public CarritoCategoryServices(ICarritoCategoryReposioty carritoCategoryReposioty)
    {
        _carritoCategoryReposioty = carritoCategoryReposioty;
    }
    
    
   

    public  async Task<bool> CarritoCategoryExist(int id)
    {
        var carrito = await _carritoCategoryReposioty.GetById(id);
        return (carrito != null);
    }

    public async Task<CarritoCategoryDto> SaveAsyc(CarritoCategoryDto carritoDto)
    {
        var carrito = new CarritoCategory 
        {
            IdProducto = carritoDto.IdProducto,
            IdUser = carritoDto.IdUser,
            Imagen = carritoDto.Image,
            Titulo = carritoDto.Titulo,
            Precio = carritoDto.Precio,
            Cantidad = carritoDto.Cantidad,
            CreatedBy = "Omar",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Omar",
            UpdatedDate = DateTime.Now
        };
        
        carrito = await _carritoCategoryReposioty.SaveAsycn(carrito);
        carritoDto.id = carrito.id;
        return carritoDto;
    }

    public async Task<CarritoCategoryDto> UpdateAsync(CarritoCategoryDto carritoDto)
    {
        var carrito = await _carritoCategoryReposioty.GetById(carritoDto.id);

        if (carrito == null)
            throw new Exception("Product Category Not founf");
        carrito.IdProducto = carritoDto.IdProducto;
        carrito.IdUser = carritoDto.IdUser;
        carrito.Titulo = carritoDto.Titulo;
        carrito.Imagen = carritoDto.Image;
        carrito.Precio = carritoDto.Precio;
        carrito.Cantidad = carritoDto.Cantidad;
       
        
        carrito.UpdatedBy = "Omar";
        carrito.UpdatedDate = DateTime.Now;
        await _carritoCategoryReposioty.UpdateAsync(carrito);
        return carritoDto;
    }

    public async Task<List<CarritoCategoryDto>> GetAllAsync()
    {
        var carrito = await _carritoCategoryReposioty.GetAllAsync();
        var carritoDto = carrito.Select(c => new CarritoCategoryDto(c)).ToList();
        return carritoDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _carritoCategoryReposioty.DeleteAsync(id);    }

    public async Task<CarritoCategoryDto> GetById(int id)
    {
        var carrito = await _carritoCategoryReposioty.GetById(id);
        if (carrito == null)
            throw new Exception("Product category not Found");
        var carritoDto = new CarritoCategoryDto()
        {
            IdProducto = carrito.IdProducto,
            IdUser = carrito.IdUser,
            Image = carrito.Imagen,
            Titulo = carrito.Titulo,
            Precio = carrito.Precio,
            Cantidad = carrito.Cantidad,
        };
        return carritoDto;
    }
}