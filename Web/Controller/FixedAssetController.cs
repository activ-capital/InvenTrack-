using Domain.Dtos.FixedAssetDto;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class FixedAssetController(IFixedAssetService service) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetFixedAssetDto>>> GetAll([FromQuery] FixedAssetFilter filter) =>
        await service.GetAllFixedAssetAsync(filter);

    [HttpGet("{id}")]
    public async Task<ApiResponse<GetFixedAssetDto>> GetById(int id) => await service.GetByIdAsync(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create([FromBody] AddFixedAssetDto request) =>
        await service.CreateAsync(request);

    [HttpPut("{id}")]
    public async Task<ApiResponse<string>> Update([FromRoute] int id, [FromBody] UpdateFixedAssetDto request) =>
        await service.UpdateAsync(id, request);

    [HttpDelete("{id}")]
    public async Task<ApiResponse<string>> Delete([FromRoute] int id) => await service.DeleteAsync(id);
}