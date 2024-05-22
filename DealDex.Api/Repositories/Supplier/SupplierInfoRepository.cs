using Dapper;
using Dapper.Contrib.Extensions;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Supplier;

namespace Tecnm.Ecommerce1.Api.Repositories.Supplier;

public class SupplierInfoRepository : ISupplierInfoRepository
{
    
    private readonly IDbContext _dbContext;

    public SupplierInfoRepository(IDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<SupplierInfo> SaveAsycn(SupplierInfo category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;   
    }

    public async Task<SupplierInfo> UpdateAsync(SupplierInfo category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public async Task<List<SupplierInfo>> GetAllAsync()
    {
        const string sql = "SELECT * FROM SupplierInfo WHERE isDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<SupplierInfo>(sql);
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

    public async Task<SupplierInfo> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<SupplierInfo>(id);
        if (category == null)
            return null;
        return category.IsDeleted == true ? null : category;

    }

    public async Task<SupplierInfo> GetByName(string name, int id = 0)
    {
        string sql = $"SELECT * FROM SupplierInfo WHERE Correo = '{name}' AND id <> {id}";
        var categories = await _dbContext.Connection.QueryAsync<SupplierInfo>(sql);

        return categories.ToList().FirstOrDefault();
    }
}