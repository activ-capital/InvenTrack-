using Domain.Dtos;
using Domain.Dtos.DepartmentDto;
using Domain.Filter;
using Infrastructure.Response;

namespace Infrastructure.Interfaces;

public interface IDepartmentService
{
    Task<PaginationResponse<List<GetDepartmentDto>>> GetAllDepartmentAsync(DepartmentFilter filter);
    Task<ApiResponse<GetDepartmentDto>> GetByIdAsync(int id);
    Task<ApiResponse<string>> CreateAsync(AddDepartmentDto request);
    Task<ApiResponse<string>> UpdateAsync(int id,UpdateDepartmentDto request);
    Task<ApiResponse<string>> DeleteAsync(int id);
}