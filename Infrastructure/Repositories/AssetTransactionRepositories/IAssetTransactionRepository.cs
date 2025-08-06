using System.Linq.Expressions;
using Domain.Entities;

namespace Infrastructure.Repositories.AssetTransactionRepositories;

public interface IAssetTransactionRepository
{
    Task<List<AssetTransaction>> GetAll(AssetTransactionFilter filter);
    Task<AssetTransaction?> GetAssetTransaction(Expression<Func<AssetTransaction, bool>>? filter = null);
    Task<int> CreateAssetTransaction(AssetTransaction request);
    Task<int> UpdateAssetTransaction(AssetTransaction request);
    Task<int> DeleteAssetTransaction(AssetTransaction request);
}