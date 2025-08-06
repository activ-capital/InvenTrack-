using System.Net;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.FixedAssetRepositories;
using Infrastructure.Repositories.InventoryItemRepositories;
using Infrastructure.Response;
using QRCoder;


namespace Infrastructure.Services;

public class QrCodeService(
    IInventoryItemRepository inventoryItemRepository,
    IFixedAssetRepository fixedAssetRepository)
    : IQrCodeService
{
    private readonly IInventoryItemRepository _inventoryItemRepository = 
        inventoryItemRepository ?? throw new ArgumentNullException(nameof(inventoryItemRepository));

    private readonly IFixedAssetRepository _fixedAssetRepository =
        fixedAssetRepository ?? throw new ArgumentNullException(nameof(fixedAssetRepository));

    public async Task<ApiResponse<byte[]>> GenerateInventoryItemQrAsync(int inventoryItemId)
    {
        if (inventoryItemId <= 0)
            return new ApiResponse<byte[]>(HttpStatusCode.BadRequest, "Invalid inventory item ID");

        var item = await _inventoryItemRepository.GetInventoryItem(x => x.Id == inventoryItemId);
        if (item == null)
            return new ApiResponse<byte[]>(HttpStatusCode.NotFound, "Inventory item not found");

        var qrText =
            $"InventoryItem:\nName: {item.Name}\nInventoryNumber: {item.InventoryNumber}\nDate: {item.AcquisitionDate}\nUnit: {item.Unit}\nEmployeeId: {item.EmployeeId}";
        var qrImage = GenerateQrImage(qrText);
        return new ApiResponse<byte[]>(qrImage);
    }

    public async Task<ApiResponse<byte[]>> GenerateFixedAssetQrAsync(int fixedAssetId)
    {
        if (fixedAssetId <= 0)
            return new ApiResponse<byte[]>(HttpStatusCode.BadRequest, "Invalid fixed asset ID");

        var asset = await _fixedAssetRepository.GetFixedAsset(x => x.Id == fixedAssetId);
        if (asset == null)
            return new ApiResponse<byte[]>(HttpStatusCode.NotFound, "Fixed asset not found");

        var qrText =
            $"FixedAsset:\nName: {asset.Name}\nInventoryNumber #: {asset.InventoryNumber}\nDate: {asset.AcquisitionDate}\nUnit: {asset.SerialNumber}\nEmployeeId: {asset.EmployeeId}";
        var qrImage = GenerateQrImage(qrText);
        return new ApiResponse<byte[]>(qrImage);
    }

    private byte[] GenerateQrImage(string qrText)
    {
        if (string.IsNullOrWhiteSpace(qrText))
            throw new ArgumentException("QR text cannot be null or empty.", nameof(qrText));

        try
        {
            using var generator = new QRCodeGenerator();
            using var qrCodeData =
                generator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.H); // Высокий уровень коррекции
            using var pngQrCode = new PngByteQRCode(qrCodeData);
            return pngQrCode.GetGraphic(30); // Размер пикселя 30 для читаемости
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to generate QR code.", ex);
        }
    }
}