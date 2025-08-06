namespace Domain.Entities;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
   
    public int? EmployeeId { get; set; }
    public Employee Employee { get; set; } 
    // Подотделы
    public List<SubDepartment> SubDepartments { get; set; }
}
