using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class LoginClient
{
    private readonly HttpClient _client;
    private JsonDocument _sessionResponse;
    private DateTime _sessionCreatedTime;
    private readonly TimeSpan _sessionDuration = TimeSpan.FromMinutes(30);
    private string _username;
    private string _password;
    private string _companyDB;

    public LoginClient()
    {
        _client = CreateHttpClient();
        _client.BaseAddress = new Uri("https://ESONEPC0JFN2T:50000/");
    }

    private static HttpClient CreateHttpClient()
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        return new HttpClient(handler);
    }

    public async Task<JsonDocument> LoginAsync(string username, string password, string companyDB)
    {
        _username = username;
        _password = password;
        _companyDB = companyDB;

        return await CreateNewSession();
    }

    private async Task<JsonDocument> CreateNewSession()
    {
        var loginData = new
        {
            CompanyDB = _companyDB,
            UserName = _username,
            Password = _password
        };

        var content = new StringContent(
            JsonSerializer.Serialize(loginData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("b1s/v2/Login", content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        _sessionResponse = JsonDocument.Parse(jsonResponse);
        _sessionCreatedTime = DateTime.UtcNow;

        // Set the session ID in the headers for subsequent requests
        var sessionId = _sessionResponse.RootElement.GetProperty("SessionId").GetString();
        _client.DefaultRequestHeaders.Remove("Cookie");
        _client.DefaultRequestHeaders.Add("Cookie", $"B1SESSION={sessionId}");

        return _sessionResponse;
    }

    public async Task<JsonDocument> EnsureValidSessionAsync()
    {
        Console.WriteLine("Validating if session is active...");
        if (IsSessionExpired())
        {
            Console.WriteLine("Session expired, creating a new one");
            return await CreateNewSession();
        }
        Console.WriteLine("Session is active");
        return _sessionResponse;
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
    {
        await EnsureValidSessionAsync();

        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        return response;
    }
    public async Task<HttpResponseMessage> PatchAsync<T>(string endpoint, string PrimaryKey, T updatedData)
    {
        await EnsureValidSessionAsync();

        endpoint = endpoint + "('" + PrimaryKey + "')";

        var content = new StringContent(
            JsonSerializer.Serialize(updatedData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PatchAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        return response;

    }
    public async Task<HttpResponseMessage> GetAsync<T>(string endpoint, T PrimaryKey)
    {
        await EnsureValidSessionAsync();

        endpoint = endpoint + "('" + PrimaryKey + "')";

        var response = await _client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return response;

    }

    private bool IsSessionExpired()
    {
        if (_sessionResponse == null) return true;
        return DateTime.UtcNow - _sessionCreatedTime >= _sessionDuration;
    }

    public TimeSpan GetRemainingSessionTime()
    {
        if (_sessionResponse == null) return TimeSpan.Zero;
        var elapsed = DateTime.UtcNow - _sessionCreatedTime;
        return _sessionDuration - elapsed;
    }

    public string GetRawResponse()
    {
        return _sessionResponse?.RootElement.ToString();
    }
}