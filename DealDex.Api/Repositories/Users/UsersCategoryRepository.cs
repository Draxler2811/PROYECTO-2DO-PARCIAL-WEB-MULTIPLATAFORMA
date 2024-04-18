using Dapper;
using Dapper.Contrib.Extensions;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Core.Entities;

namespace DealDex.Api.Repositories;

public class UsersCategoryRepository : IUsersCategoryRepository
{
    private readonly IDbContext _dbContext;

    public UsersCategoryRepository(IDbContext context)
    {
        _dbContext = context;
    }


public  async Task<UsersCategory> SaveAsycn(UsersCategory category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;
    }

    public  async Task<UsersCategory> UpdateAsync(UsersCategory category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public async Task<List<UsersCategory>> GetAllAsync()
    {
        const string sql = "SELECT * FROM UsersCategory WHERE isDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<UsersCategory>(sql);
        return categories.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await GetById(id);
        if (category == null)
            return false;

        category.IsDeleted = true;

        return await _dbContext.Connection.UpdateAsync(category);
    }

    public async Task<UsersCategory> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<UsersCategory>(id);
        if (category == null)
            return null;
        return category.IsDeleted == true ? null : category;


    }
}