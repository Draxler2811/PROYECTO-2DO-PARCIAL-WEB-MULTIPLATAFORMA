﻿using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.VisualBasic;
using DealDex.Core.Entities;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Api.Repositories.Interfecies;

namespace DealDex.Api.Repositories;

public class ProductCategoryRepositoty : IProductCategoryRepository
{
    private readonly IDbContext _dbContext;

    public ProductCategoryRepositoty(IDbContext context)
    {
        _dbContext = context;
    }
    public  async Task<ProductCategory> SaveAsycn(ProductCategory category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;
    }

    public async Task<ProductCategory> UpdateAsync(ProductCategory category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public  async Task<List<ProductCategory>> GetAllAsync()
    {
        const string sql = "SELECT * FROM ProductCategory WHERE isDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<ProductCategory>(sql);
        return categories.ToList();
    }

    public  async Task<bool> DeleteAsync(int id)
    {
        var category = await GetById(id);
        if (category == null)
            return false;

        category.IsDeleted = true;

        return await _dbContext.Connection.UpdateAsync(category);
    }

    public async Task<ProductCategory> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<ProductCategory>(id);
        if (category == null)
            return null;
        return category.IsDeleted == true ? null : category;


    }

    public  async Task<ProductCategory> GetByName(string name, int id = 0)
    {
        string sql = $"SELECT * FROM ProductCategory WHERE Titulo = '{name}' AND id <> {id}";
        var categories = await _dbContext.Connection.QueryAsync<ProductCategory>(sql);

        return categories.ToList().FirstOrDefault();
    }
}