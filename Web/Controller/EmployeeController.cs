using Domain.Dtos;
using Domain.Dtos.EmployeeDto;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controller;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService service) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResponse<List<GetEmployeeDto>>> GetAll([FromQuery] EmployeeFilter filter) =>
        await service.GetAllEmployeeAsync(filter);

    [HttpGet("{id}")]
    public async Task<ApiResponse<GetEmployeeDto>> GetById(int id) => await service.GetByIdAsync(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create([FromForm] AddEmployeeDto request) =>
        await service.CreateAsync(request);

    [HttpPut("{id}")]
    public async Task<ApiResponse<string>> Update([FromRoute]int id, [FromForm] UpdateEmployeeDto request) =>
        await service.UpdateAsync(id, request);

    [HttpDelete("{id}")]
    public async Task<ApiResponse<string>> Delete([FromRoute] int id) => await service.DeleteAsync(id);
}

