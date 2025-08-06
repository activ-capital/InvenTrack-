namespace Domain.Dtos.AssetTransactionDto;

public class AddAssetTransactionDto
{
    public int? FixedAssetId { get; set; }            // Внешний ключ для FixedAsset
    public int? InventoryItemId { get; set; }         // Внешний ключ для InventoryItem
    public string TransactionType { get; set; }       // Например, "Arrival", "Transfer", "WriteOff"
    public DateTime TransactionDate { get; set; }
    public int? FromEmployeeId { get; set; }          // От кого (если передача)
    public int? ToEmployeeId { get; set; }            // Кому (если передача)
}