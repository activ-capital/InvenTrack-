using Domain.Dtos.EmployeeDto;

namespace Domain.Dtos.SubDepartmentDto;

public class AddSubDepartmentDto
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    // Начальник подотдела
    public int? EmployeeId { get; set; }

}