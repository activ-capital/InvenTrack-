using Domain.Dtos.InventoryItem;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Interfaces;

public interface IInventoryItemService
{
    Task<PaginationResponse<List<GetInventoryItemDto>>> GetAllInventoryItemAsync(InventoryItemFilter filter);
    Task<ApiResponse<GetInventoryItemDto>> GetByIdAsync(int id);
    Task<ApiResponse<string>> CreateAsync(AddInventoryItemDto request);
    Task<ApiResponse<string>> UpdateAsync(int id,UpdateInventoryItemDto request);
    Task<ApiResponse<string>> DeleteAsync(int id);
}