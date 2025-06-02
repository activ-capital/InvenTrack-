using Domain.Dtos.PositionDto;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class PositionController(IPositionService service) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetPositionDto>>> GetAll([FromQuery] PositionFilter filter) =>
        await service.GetAllPositionAsync(filter);

    [HttpGet("{id}")]
    public async Task<ApiResponse<GetPositionDto>> GetById(int id) => await service.GetByIdAsync(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create([FromBody] AddPositionDto request) =>
        await service.CreateAsync(request);

    [HttpPut("{id}")]
    public async Task<ApiResponse<string>> Update([FromRoute] int id, [FromBody] UpdatePositionDto request) =>
        await service.UpdateAsync(id, request);

    [HttpDelete("{id}")]
    public async Task<ApiResponse<string>> Delete([FromRoute] int id) => await service.DeleteAsync(id);
}