using Domain.Dtos.InventoryItem;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class InventoryItemController(IInventoryItemService service) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetInventoryItemDto>>> GetAll([FromQuery] InventoryItemFilter filter) =>
        await service.GetAllInventoryItemAsync(filter);

    [HttpGet("{id}")]
    public async Task<ApiResponse<GetInventoryItemDto>> GetById(int id) => await service.GetByIdAsync(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create([FromBody] AddInventoryItemDto request) =>
        await service.CreateAsync(request);

    [HttpPut("{id}")]
    public async Task<ApiResponse<string>> Update([FromRoute] int id, [FromBody] UpdateInventoryItemDto request) =>
        await service.UpdateAsync(id, request);

    [HttpDelete("{id}")]
    public async Task<ApiResponse<string>> Delete([FromRoute] int id) => await service.DeleteAsync(id);
}