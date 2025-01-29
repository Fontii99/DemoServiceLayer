using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Item
{
    public string ItemCode { get; set; }

    public string ItemName { get; set; }

    public Item(string itemCode, string itemName)
    {
        ItemCode = itemCode;
        ItemName = itemName;
    }
    public Item() { }
}