using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Client
{
    [JsonPropertyName("ItemCode")]
    public string CardCode { get; set; }

    [JsonPropertyName("ItemName")]
    public string CardName { get; set; }

    public Client(string cardCode, string cardName)
    {
        CardCode = cardCode;
        CardName = cardName;
    }
    public Client() { }
}