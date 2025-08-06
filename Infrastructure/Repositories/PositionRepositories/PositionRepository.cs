using System.Linq.Expressions;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.PositionRepositories;

public class PositionRepository(DataContext context, ILogger<PositionRepository> logger) : IPositionRepository
{
    public async Task<List<Position>> GetAll(PositionFilter filter)
    {
        var query = context.Positions.Include(e=>e.Employees).AsQueryable();

        var positions = await query.ToListAsync();
        return positions;
    }

    public async Task<Position?> GetPosition(Expression<Func<Position, bool>>? filter = null)
    {
        var query = context.Positions.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> CreatePosition(Position request)
    {
        try
        {
            await context.Positions.AddAsync(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> UpdatePosition(Position request)
    {
        try
        {
            context.Positions.Update(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }

    public async Task<int> DeletePosition(Position request)
    {
        try
        {
            context.Positions.Remove(request);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return 0;
        }
    }
}