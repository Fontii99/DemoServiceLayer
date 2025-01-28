
Console.WriteLine("DEMO SERVICE LAYER");

var client = new LoginClient();

try
{
   
    var loginResponse = await client.LoginAsync(
        username: "manager",
        password: "seidor",
        companyDB: "SBODemoES2"
    );
    Console.WriteLine($"Login successful! Response: {client.GetRawResponse()}");

    ////PATCH

    //var itemUpdated = new Item
    //{
    //    ItemName = "Test Item Updated"
    //};

    //var response = await client.PatchAsync("b1s/v2/Items", "ITEM001", itemUpdated);

    //var responseContent = await response.Content.ReadAsStringAsync();
    //Console.WriteLine($"Item updated successfully: {responseContent}");

    //PATCH END

    ////GET

    //var item = new Item
    //{
    //    ItemCode = "ITEM001",
    //    ItemName = "Test Item"
    //};

    //var response = await client.GetAsync("b1s/v2/Items", item.ItemCode);

    //var responseContent = await response.Content.ReadAsStringAsync();
    //Console.WriteLine($"Item created successfully: {responseContent}");

    //GET END

    //POST

    //var item = new Item
    //{
    //    ItemCode = "ITEM002",
    //    ItemName = "Test Item"
    //};

    var newClient = new Client
    {
        CardCode = "CLI001",
        CardName = "Test Client",
        CardType = "cCustomer"
    };

    //var response = await client.PostAsync("b1s/v2/Items", item);

    //var responseContent = await response.Content.ReadAsStringAsync();
    //Console.WriteLine($"Item created successfully: {responseContent}");

    var response = await client.PostAsync("b1s/v2/BusinessPartners", newClient);

    var responseContent = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Client created successfully: {responseContent}");

    //POST END
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Error occurred: {ex.Message}");
}