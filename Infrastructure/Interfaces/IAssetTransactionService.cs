using Domain.Dtos.AssetTransactionDto;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Interfaces;

public interface IAssetTransactionService
{
    Task<PaginationResponse<List<GetAssetTransactionDto>>> GetAllAssetTransactionAsync(AssetTransactionFilter filter);
    Task<ApiResponse<GetAssetTransactionDto>> GetByIdAsync(int id);
    Task<ApiResponse<string>> CreateAsync(AddAssetTransactionDto request);
    Task<ApiResponse<string>> UpdateAsync(int id,UpdateAssetTransactionDto request);
    Task<ApiResponse<string>> DeleteAsync(int id);
}