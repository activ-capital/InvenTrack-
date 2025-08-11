using Domain.Dtos.SubDepartmentDto;

namespace Domain.Dtos.DepartmentDto;

public class GetDepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<GetSubDepartmentDto> SubDepartments { get; set; }
}