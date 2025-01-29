using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Client
{

    public string CardCode { get; set; }

    public string CardName { get; set; }


    public Client(string cardCode, string cardName, string cardType)
    {
        CardCode = cardCode;
        CardName = cardName;
    }


    public Client() { }
}