using System.Net;
using Domain.Dtos.DepartmentDto;
using Domain.Dtos.EmployeeDto;
using Domain.Dtos.SubDepartmentDto;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.SubDepartmentRepositories;
using Infrastructure.Response;

namespace Infrastructure.Services;

public class SubDepartmentService(ISubDepartmentRepository repository) : ISubDepartmentService
{
    public async Task<PaginationResponse<List<GetSubDepartmentDto>>> GetAllSubDepartmentAsync(
        SubDepartmentFilter filter)
    {
        var subDepartment = await repository.GetAll(filter);
        var totalRecords = subDepartment.Count;
        var data = subDepartment
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        var result = subDepartment.Select(s => new GetSubDepartmentDto()
        {
            Id = s.Id,
            Name = s.Name,
            DepartmentId = s.DepartmentId,
            EmployeeId = s.EmployeeId,
            Employees = s.Employees.Select(e=> new GetEmployeeDto()
            {
                Id = e.Id,
                FullName = e.FullName,
                Role = e.Role,    
                PositionId = e.PositionId,
            }).ToList()
        }).ToList();
        return new PaginationResponse<List<GetSubDepartmentDto>>(result, totalRecords, filter.PageNumber,
            filter.PageSize);
    }

    public async Task<ApiResponse<GetSubDepartmentDto>> GetByIdAsync(int id)
    {
        var subDepartment = await repository.GetSubDepartment(s => s.Id == id);
        if (subDepartment == null)
        {
            return new ApiResponse<GetSubDepartmentDto>(HttpStatusCode.NotFound, "SubDepartment not found");
        }

        var result = new GetSubDepartmentDto()
        {
            Id = subDepartment.Id,
            Name = subDepartment.Name,
            DepartmentId = subDepartment.DepartmentId,
            EmployeeId = subDepartment.EmployeeId,
        };
        return new ApiResponse<GetSubDepartmentDto>(result);
    }

    public async Task<ApiResponse<string>> CreateAsync(AddSubDepartmentDto request)
    {
        var subDepartment = new SubDepartment()
        {
            Name = request.Name,
            DepartmentId = request.DepartmentId,
            EmployeeId = request.EmployeeId
        };
        var result = await repository.CreateSubDepartment(subDepartment);

        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateSubDepartmentDto request)
    {
        var department = await repository.GetSubDepartment(q => q.Id == id);
        if (department == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "SubDepartment not found");
        }

        department.Id = request.Id;
        department.Name = request.Name;
        department.DepartmentId = request.DepartmentId;
        department.EmployeeId = request.EmployeeId;
        var result = await repository.UpdateSubDepartment(department);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var department = await repository.GetSubDepartment(q => q.Id == id);
        if (department == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "SubDepartment not found");
        }

        var result = await repository.DeleteSubDepartment(department);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }
}