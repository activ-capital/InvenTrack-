namespace Domain.Entities;

public class FixedAsset : Asset
{
    public string SerialNumber { get; set; } // Серийный номер
    public int UsefulLifeYears { get; set; } // Срок полезного использования (в годах)
}