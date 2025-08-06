using System.Net;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;
[ApiController]
[Route("api/[controller]")]
public class QrCodeController(IQrCodeService service) : ControllerBase
{
    [HttpGet("inventory/{inventoryItemId}")]
    public async Task<IActionResult> GenerateInventoryItemQr(int inventoryItemId)
    {
        var response = await service.GenerateInventoryItemQrAsync(inventoryItemId);

        return File(response.Data, "image/png", $"inventory_qr_{inventoryItemId}.png");
    }

    [HttpGet("fixed-asset/{fixedAssetId}")]
    public async Task<IActionResult> GenerateFixedAssetQr(int fixedAssetId)
    {
        var response = await service.GenerateFixedAssetQrAsync(fixedAssetId);

        return File(response.Data, "image/png", $"fixed_asset_qr_{fixedAssetId}.png");
    }
}