namespace Domain.Entities;

public class SubDepartment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int? EmployeeId { get; set; } // Начальник подотдела (может отсутствовать)
    public Employee? Employee { get; set; } // Nullable для согласованности
    public List<Employee> Employees { get; set; } = new();
}