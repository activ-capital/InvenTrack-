using System.Linq.Expressions;
using Domain.Entities;
using Domain.Filter;

namespace Infrastructure.Repositories.FixedAssetRepositories;

public interface IFixedAssetRepository
{
    Task<List<FixedAsset>> GetAll(FixedAssetFilter filter);
    Task<FixedAsset?> GetFixedAsset(Expression<Func<FixedAsset, bool>>? filter = null);
    Task<int> CreateFixedAsset(FixedAsset request);
    Task<int> UpdateFixedAsset(FixedAsset request);
    Task<int> DeleteFixedAsset(FixedAsset request);
}