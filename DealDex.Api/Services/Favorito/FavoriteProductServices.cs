using DealDex.Api.Dto.Favoritos;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Favoritos;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Favorito;

namespace Tecnm.Ecommerce1.Api.Services.Favorito;

public class FavoriteProductServices : IFavoriteProductServices
{
    private readonly IFavoriteProductRepository _favoriteProductRepository;
    

    public FavoriteProductServices(IFavoriteProductRepository favoriteProductRepository)
    {
        _favoriteProductRepository = favoriteProductRepository;
    }


    
    
    public async Task<bool> FavoriteProductExist(int id)
    {
        var carrito = await _favoriteProductRepository.GetById(id);
        return (carrito != null);
    }

    public async Task<FavoriteProductDto> SaveAsyc(FavoriteProductDto favoriteDto)
    {
        var carrito = new FavoriteProduct
        {
            IdProducto = favoriteDto.IdProducto,
            IdUser = favoriteDto.IdUser,
            Imagen = favoriteDto.Image,
            Titulo = favoriteDto.Titulo,
            Precio = favoriteDto.Precio,
            Cantidad = favoriteDto.Cantidad,
            CreatedBy = "Omar",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Omar",
            UpdatedDate = DateTime.Now
        };
        
        carrito = await _favoriteProductRepository.SaveAsycn(carrito);
        favoriteDto.id = carrito.id;
        return favoriteDto;
    }

    public async Task<FavoriteProductDto> UpdateAsync(FavoriteProductDto favoriteDto)
    {
        var carrito = await _favoriteProductRepository.GetById(favoriteDto.id);

        if (carrito == null)
            throw new Exception("Product Category Not founf");
        carrito.IdProducto = favoriteDto.IdProducto;
        carrito.IdUser = favoriteDto.IdUser;
        carrito.Titulo = favoriteDto.Titulo;
        carrito.Imagen = favoriteDto.Image;
        carrito.Precio = favoriteDto.Precio;
        carrito.Cantidad = favoriteDto.Cantidad;
       
        
        carrito.UpdatedBy = "Omar";
        carrito.UpdatedDate = DateTime.Now;
        await _favoriteProductRepository.UpdateAsync(carrito);
        return favoriteDto;
    }

    public async Task<List<FavoriteProductDto>> GetAllAsync()
    {
        var carrito = await _favoriteProductRepository.GetAllAsync();
        var favoriteDto = carrito.Select(c => new FavoriteProductDto(c)).ToList();
        return favoriteDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _favoriteProductRepository.DeleteAsync(id);   

    }

    public async Task<FavoriteProductDto> GetById(int id)
    {
        var carrito = await _favoriteProductRepository.GetById(id);
        if (carrito == null)
            throw new Exception("Product category not Found");
        var favoriteDto = new FavoriteProductDto()
        {
            IdProducto = carrito.IdProducto,
            IdUser = carrito.IdUser,
            Image = carrito.Imagen,
            Titulo = carrito.Titulo,
            Precio = carrito.Precio,
            Cantidad = carrito.Cantidad,
        };
        return favoriteDto;
    }
}