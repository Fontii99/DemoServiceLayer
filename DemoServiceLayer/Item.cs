using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Item
{
    [JsonPropertyName("ItemCode")]
    public string ItemCode { get; set; }

    [JsonPropertyName("ItemName")]
    public string ItemName { get; set; }

    public Item(string itemCode, string itemName)
    {
        ItemCode = itemCode;
        ItemName = itemName;
    }
    public Item() { }
}