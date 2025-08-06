using System.Linq.Expressions;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.InventoryItemRepositories;

public class InventoryItemRepository(DataContext context, ILogger<InventoryItemRepository> logger)
    : IInventoryItemRepository
{
    public async Task<List<InventoryItem>> GetAll(InventoryItemFilter filter)
    {
        var query = context.InventoryItems.AsQueryable();

        var inventoryItems = await query.ToListAsync();
        return inventoryItems;
    }

    public async Task<InventoryItem?> GetInventoryItem(Expression<Func<InventoryItem, bool>>? filter = null)
    {
        var query = context.InventoryItems.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> CreateInventoryItem(InventoryItem request)
    {
        try
        {
            await context.InventoryItems.AddAsync(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> UpdateInventoryItem(InventoryItem request)
    {
        try
        {
            context.InventoryItems.Update(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> DeleteInventoryItem(InventoryItem request)
    {
        try
        {
            context.InventoryItems.Remove(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }
}