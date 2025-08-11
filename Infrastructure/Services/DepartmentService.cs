using System.Net;
using Domain.Dtos;
using Domain.Dtos.DepartmentDto;
using Domain.Dtos.SubDepartmentDto;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.DepartmentRepositories;
using Infrastructure.Response;

namespace Infrastructure.Services;

public class DepartmentService(IDepartmentRepository repository) : IDepartmentService
{
    public async Task<PaginationResponse<List<GetDepartmentDto>>> GetAllDepartmentAsync(DepartmentFilter filter)
    {
        var department = await repository.GetAll(filter);
        var totalRecords = department.Count;
        var data = department
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        var result = data.Select(d => new GetDepartmentDto()
        {
            Id = d.Id,
            Name = d.Name,
            SubDepartments = d.SubDepartments.Select(s => new GetSubDepartmentDto()
            {
                Id = s.Id,
                Name = s.Name,
                DepartmentId = s.DepartmentId,

            }).ToList()
        }).ToList();
        return new PaginationResponse<List<GetDepartmentDto>>(result, totalRecords, filter.PageNumber,
            filter.PageSize);
    }

    public async Task<ApiResponse<GetDepartmentDto>> GetByIdAsync(int id)
    {
        var department = await repository.GetDepartment(q => q.Id == id);
        if (department == null)
        {
            return new ApiResponse<GetDepartmentDto>(HttpStatusCode.NotFound, "Department not found");
        }

        var result = new GetDepartmentDto()
        {
            Id = department.Id,
            Name = department.Name,
           
            SubDepartments = department.SubDepartments.Select(s => new GetSubDepartmentDto()
            {
                Id = s.Id,
                Name = s.Name,
                DepartmentId = s.DepartmentId,
                
            }).ToList()
        };
        return new ApiResponse<GetDepartmentDto>(result);
    }

    public async Task<ApiResponse<string>> CreateAsync(AddDepartmentDto request)
    {
        var department = new Department()
        {
            Name = request.Name,
          
        };
        var result = await repository.CreateDepartment(department);

        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateDepartmentDto request)
    {
        var department = await repository.GetDepartment(q => q.Id == id);
        if (department == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Department not found");
        }

        department.Id = request.Id;
        department.Name = request.Name;
        
        var result = await repository.UpdateDepartment(department);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var department = await repository.GetDepartment(q => q.Id == id);
        if (department == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Department not found");
        }

        var result = await repository.DeleteDepartment(department);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }
}