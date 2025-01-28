using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Client
{
    [JsonPropertyName("CardCode")]
    public string CardCode { get; set; }

    [JsonPropertyName("CardName")]
    public string CardName { get; set; }

    [JsonPropertyName("CardType")]
    public string CardType { get; set; }

    public Client(string cardCode, string cardName, string cardType)
    {
        CardCode = cardCode;
        CardName = cardName;
        CardType = cardType;
    }
    public Client() { }
}