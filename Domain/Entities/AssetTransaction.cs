namespace Domain.Entities;

public class AssetTransaction
{
    public int Id { get; set; }
    public int? FixedAssetId { get; set; }            // Внешний ключ для FixedAsset
    public FixedAsset? FixedAsset { get; set; }       // Навигационное свойство
    public int? InventoryItemId { get; set; }         // Внешний ключ для InventoryItem
    public InventoryItem? InventoryItem { get; set; } // Навигационное свойство
    public string TransactionType { get; set; }       // Например, "Arrival", "Transfer", "WriteOff"
    public DateTime TransactionDate { get; set; }
    public int? FromEmployeeId { get; set; }          // От кого (если передача)
    public Employee? FromEmployee { get; set; }
    public int? ToEmployeeId { get; set; }            // Кому (если передача)
    public Employee? ToEmployee { get; set; }
}