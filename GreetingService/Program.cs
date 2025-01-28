var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

Dictionary<string, List<string>> greetingDictionary = new Dictionary<string, List<string>>
{
    { "English", new List<string> { "Hello", "Hi", "Hey", "Greetings", "Good Day" } },
    { "Spanish", new List<string> { "Hola", "Buenos días", "Buenas tardes", "Saludos", "Qué tal" } },
    { "French", new List<string> { "Bonjour", "Salut", "Coucou", "Allô", "Bonsoir" } },
    { "German", new List<string> { "Hallo", "Guten Tag", "Guten Morgen", "Servus", "Grüß Gott" } },
    { "Italian", new List<string> { "Ciao", "Salve", "Buongiorno", "Buonasera", "Ehilà" } },
    { "Japanese", new List<string> { "こんにちは (Konnichiwa)", "やあ (Yaa)", "おはよう (Ohayou)", "こんばんは (Konbanwa)", "もしもし (Moshi Moshi)" } },
    { "Chinese", new List<string> { "你好 (Nǐ hǎo)", "嗨 (Hāi)", "您好 (Nín hǎo)", "早上好 (Zǎoshang hǎo)", "下午好 (Xiàwǔ hǎo)" } },
    { "Russian", new List<string> { "Здравствуйте (Zdravstvuyte)", "Привет (Privet)", "Добрый день (Dobryy den')", "Добрый вечер (Dobryy vecher)", "Здорова (Zdorova)" } },
    { "Arabic", new List<string> { "مرحبا (Marhaba)", "أهلا (Ahlan)", "السلام عليكم (As-salamu alaykum)", "صباح الخير (Sabah al-khayr)", "مساء الخير (Masa' al-khayr)" } },
    { "Hindi", new List<string> { "नमस्ते (Namaste)", "नमस्कार (Namaskar)", "सुप्रभात (Suprabhat)", "शुभ संध्या (Shubh Sandhya)", "हैलो (Hello)" } }
};


app.MapGet("/Greeting/{language}", (string language) =>
{
    if (greetingDictionary.TryGetValue(language, out var greetings))
    {
        Random rn = new Random();
        return Results.Ok(greetings.OrderBy(p => rn.Next()).First());
    }

    return Results.NotFound($"Greeting not found for language: {language}");
});


app.Run();