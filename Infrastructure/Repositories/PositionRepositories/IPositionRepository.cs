using System.Linq.Expressions;
using Domain.Entities;
using Domain.Filter;

namespace Infrastructure.Repositories.PositionRepositories;

public interface IPositionRepository
{
    Task<List<Position>> GetAll(PositionFilter filter);
    Task<Position?> GetPosition(Expression<Func<Position, bool>>? filter = null);
    Task<int> CreatePosition(Position request);
    Task<int> UpdatePosition(Position request);
    Task<int> DeletePosition(Position request);
}