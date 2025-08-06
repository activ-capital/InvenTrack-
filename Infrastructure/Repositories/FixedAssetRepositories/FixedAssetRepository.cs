using System.Linq.Expressions;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.FixedAssetRepositories;

public class FixedAssetRepository(DataContext context, ILogger<FixedAssetRepository> logger) : IFixedAssetRepository
{
    public async Task<List<FixedAsset>> GetAll(FixedAssetFilter filter)
    {
        var query = context.FixedAssets.AsQueryable();

        var fixedAssets = await query.ToListAsync();
        return fixedAssets;
    }

    public async Task<FixedAsset?> GetFixedAsset(Expression<Func<FixedAsset, bool>>? filter = null)
    {
        var query = context.FixedAssets.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> CreateFixedAsset(FixedAsset request)
    {
        try
        {
            await context.FixedAssets.AddAsync(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> UpdateFixedAsset(FixedAsset request)
    {
        try
        {
            context.FixedAssets.Update(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> DeleteFixedAsset(FixedAsset request)
    {
        try
        {
            context.FixedAssets.Remove(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }
}