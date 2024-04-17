using System.Data.Common;

namespace DealDex.Api.DataAccess.Interfaces;

public interface IDbContext
{
    DbConnection Connection { get; }
}