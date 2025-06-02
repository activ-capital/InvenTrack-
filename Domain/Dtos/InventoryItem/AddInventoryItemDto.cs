namespace Domain.Dtos.InventoryItem;

public class   AddInventoryItemDto
{
    public string Name { get; set; }
    public string InventoryNumber { get; set; } // Инвентарный номер
    public DateTime AcquisitionDate { get; set; }          // Дата (например, дата приобретения или постановки на учёт)
    public int EmployeeId { get; set; }
    public string Unit { get; set; }            // например: "штук" "метр"
}