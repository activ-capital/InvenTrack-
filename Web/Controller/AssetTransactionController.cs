using Domain.Dtos.AssetTransactionDto;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;
[ApiController]
[Route("api/[controller]")]
public class AssetTransactionController(IAssetTransactionService service) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetAssetTransactionDto>>> GetAll([FromQuery]AssetTransactionFilter filter) => await service.GetAllAssetTransactionAsync(filter);
    
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetAssetTransactionDto>> GetById(int id) => await service.GetByIdAsync(id);
    
    [HttpPost]
    public async Task<ApiResponse<string>> Create([FromBody]AddAssetTransactionDto request) => await service.CreateAsync(request);
    
    [HttpPut("{id}")]
    public async Task<ApiResponse<string>> Update([FromRoute]int id,[FromBody]UpdateAssetTransactionDto request) => await service.UpdateAsync(id,request);
    
    [HttpDelete("{id}")]
    public async Task<ApiResponse<string>> Delete([FromRoute]int id) => await service.DeleteAsync(id);
}