namespace Domain.Dtos.FixedAssetDto;

public class AddFixedAssetDto
{
    public string Name { get; set; }
    public string InventoryNumber { get; set; } // Инвентарный номер
    public DateTime AcquisitionDate { get; set; }          // Дата (например, дата приобретения или постановки на учёт)
    public int EmployeeId { get; set; }
    public string SerialNumber { get; set; }    // Серийный номер
    public int UsefulLifeYears { get; set; }    // Срок полезного использования (в годах)
}