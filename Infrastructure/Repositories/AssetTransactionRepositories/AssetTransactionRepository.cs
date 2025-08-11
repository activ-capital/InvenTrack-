using System.Linq.Expressions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.AssetTransactionRepositories;

public class AssetTransactionRepository(DataContext context, ILogger<AssetTransactionRepository> logger)
    : IAssetTransactionRepository
{
    public async Task<List<AssetTransaction>> GetAll(AssetTransactionFilter filter)
    {
        var query = context.AssetTransactions.AsQueryable();

        var departments = await query.ToListAsync();
        return departments;
    }

    public async Task<AssetTransaction?> GetAssetTransaction(Expression<Func<AssetTransaction, bool>>? filter = null)
    {
        var query = context.AssetTransactions.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> CreateAssetTransaction(AssetTransaction request)
    {
        try
        {
            await context.AssetTransactions.AddAsync(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> UpdateAssetTransaction(AssetTransaction request)
    {
        try
        {
            context.AssetTransactions.Update(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }
 
    public async Task<int> DeleteAssetTransaction(AssetTransaction request)
    {
        try
        {
            context.AssetTransactions.Remove(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }
}