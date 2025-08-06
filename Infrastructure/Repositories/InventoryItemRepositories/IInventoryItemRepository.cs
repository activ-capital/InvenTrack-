using System.Linq.Expressions;
using Domain.Entities;
using Domain.Filter;

namespace Infrastructure.Repositories.InventoryItemRepositories;

public interface IInventoryItemRepository
{
    Task<List<InventoryItem>> GetAll(InventoryItemFilter filter);
    Task<InventoryItem?> GetInventoryItem(Expression<Func<InventoryItem, bool>>? filter = null);
    Task<int> CreateInventoryItem(InventoryItem request);
    Task<int> UpdateInventoryItem(InventoryItem request);
    Task<int> DeleteInventoryItem(InventoryItem request);
}

