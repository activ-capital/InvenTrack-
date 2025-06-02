using Domain.Dtos;
using Domain.Dtos.DepartmentDto;
using Domain.Enum;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController(IDepartmentService service) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetDepartmentDto>>> GetAll([FromQuery]DepartmentFilter filter) => await service.GetAllDepartmentAsync(filter);
    
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetDepartmentDto>> GetById(int id) => await service.GetByIdAsync(id);
    
    [HttpPost]
    public async Task<ApiResponse<string>> Create([FromBody]AddDepartmentDto request) => await service.CreateAsync(request);
    
    [HttpPut("{id}")]
    public async Task<ApiResponse<string>> Update([FromRoute]int id,[FromBody]UpdateDepartmentDto request) => await service.UpdateAsync(id,request);
    
    [HttpDelete("{id}")]
    public async Task<ApiResponse<string>> Delete([FromRoute]int id) => await service.DeleteAsync(id);
}