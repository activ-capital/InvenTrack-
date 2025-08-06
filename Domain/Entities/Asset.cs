namespace Domain.Entities;

public abstract class Asset
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string InventoryNumber { get; set; } // Инвентарный номер
    public DateTime AcquisitionDate { get; set; } // Дата приобретения или постановки на учет
    public int? EmployeeId { get; set; } // Сотрудник может быть не привязан
    public Employee? Employee { get; set; }
    public DateTime? TransferDate { get; set; } // Дата передачи сотруднику
}