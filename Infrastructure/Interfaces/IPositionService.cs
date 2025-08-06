using Domain.Dtos.PositionDto;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Interfaces;

public interface IPositionService
{
    Task<PaginationResponse<List<GetPositionDto>>> GetAllPositionAsync(PositionFilter filter);
    Task<ApiResponse<GetPositionDto>> GetByIdAsync(int id);
    Task<ApiResponse<string>> CreateAsync(AddPositionDto request);
    Task<ApiResponse<string>> UpdateAsync(int id,UpdatePositionDto request);
    Task<ApiResponse<string>> DeleteAsync(int id);
}