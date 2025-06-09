using Infrastructure.Response;

namespace Infrastructure.Interfaces;

public interface IQrCodeService
{
    Task<ApiResponse<byte[]>> GenerateInventoryItemQrAsync(int inventoryItemId);
    Task<ApiResponse<byte[]>> GenerateFixedAssetQrAsync(int fixedAssetId);
}