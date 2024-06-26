﻿using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;

public interface ICarritoCategoryReposioty
{
    
    Task<CarritoCategory> SaveAsycn(CarritoCategory category);
    
    Task<CarritoCategory> UpdateAsync(CarritoCategory category);
    
    Task<List<CarritoCategory>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<CarritoCategory> GetById(int id);
    //Metodo para obtebner una categoria por nombre 
    Task<CarritoCategory> GetByName(string name, int id = 0);
}