var builder = WebApplication.CreateBuilder(args);

// HttpClient for interacting with other services
builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/", async () =>
{
    using var client = new HttpClient();

    try
    {
        // Directly call the LanguageService endpoint
        var languages = await client.GetFromJsonAsync<List<string>>("http://languageservice:8080/language");

        return Results.Ok(languages);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Unable to fetch supported languages at this time. {ex.Message}");
    }
});

app.MapGet("/HelloWorld/{language}", async (string language) =>
{
    var client = new HttpClient();

    // Fetch greeting from GreetingService
    var greetingResponse = await client.GetStringAsync($"http://greetingservice:8080/greeting/{language}");
    if (string.IsNullOrEmpty(greetingResponse)) 
        return Results.NotFound($"Greeting not found for language: {language}");

    // Fetch planet from PlanetService
    var planetResponse = await client.GetStringAsync($"http://planetservice:8080/planet/{language}");
    if (string.IsNullOrEmpty(planetResponse)) 
        return Results.NotFound($"Planet not found for language: {language}");

    return Results.Ok($"{greetingResponse}, {planetResponse}!");
});

app.Run();