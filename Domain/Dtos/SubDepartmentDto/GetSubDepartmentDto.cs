using Domain.Dtos.EmployeeDto;

namespace Domain.Dtos.SubDepartmentDto;

public class GetSubDepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    // Начальник подотдела
    public int? EmployeeId { get; set; }
    public List<GetEmployeeDto> Employees { get; set; } = new();

}