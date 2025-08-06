namespace Domain.Entities;

public class InventoryItem : Asset
{
    public string Unit { get; set; } // например: "штук" "метр"
}