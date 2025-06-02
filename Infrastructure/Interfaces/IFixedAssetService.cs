using Domain.Dtos.FixedAssetDto;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Interfaces;

public interface IFixedAssetService
{
    
    Task<PaginationResponse<List<GetFixedAssetDto>>> GetAllFixedAssetAsync(FixedAssetFilter filter);
    Task<ApiResponse<GetFixedAssetDto>> GetByIdAsync(int id);
    Task<ApiResponse<string>> CreateAsync(AddFixedAssetDto request);
    Task<ApiResponse<string>> UpdateAsync(int id,UpdateFixedAssetDto request);
    Task<ApiResponse<string>> DeleteAsync(int id);
}