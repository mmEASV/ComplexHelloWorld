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

Dictionary<string, List<string>> planetsDictionary = new Dictionary<string, List<string>>
{
    { "English", new List<string> { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" } },
    { "Spanish", new List<string> { "Mercurio", "Venus", "Tierra", "Marte", "Júpiter", "Saturno", "Urano", "Neptuno" } },
    { "French", new List<string> { "Mercure", "Vénus", "Terre", "Mars", "Jupiter", "Saturne", "Uranus", "Neptune" } },
    { "German", new List<string> { "Merkur", "Venus", "Erde", "Mars", "Jupiter", "Saturn", "Uranus", "Neptun" } },
    { "Italian", new List<string> { "Mercurio", "Venere", "Terra", "Marte", "Giove", "Saturno", "Urano", "Nettuno" } },
    { "Japanese", new List<string> { "水星 (Suisei)", "金星 (Kinsei)", "地球 (Chikyuu)", "火星 (Kasei)", "木星 (Mokusei)", "土星 (Dosei)", "天王星 (Tennousei)", "海王星 (Kaiousei)" } },
    { "Chinese", new List<string> { "水星 (Shuǐxīng)", "金星 (Jīnxīng)", "地球 (Dìqiú)", "火星 (Huǒxīng)", "木星 (Mùxīng)", "土星 (Tǔxīng)", "天王星 (Tiānwángxīng)", "海王星 (Hǎiwángxīng)" } },
    { "Russian", new List<string> { "Меркурий (Merkuriy)", "Венера (Venera)", "Земля (Zemlya)", "Марс (Mars)", "Юпитер (Yupiter)", "Сатурн (Saturn)", "Уран (Uran)", "Нептун (Neptun)" } },
    { "Arabic", new List<string> { "عطارد (Utared)", "الزهرة (Al-Zuhara)", "الأرض (Al-Ard)", "المريخ (Al-Merikh)", "المشتري (Al-Mushtari)", "زحل (Zuhal)", "أورانوس (Uranuus)", "نبتون (Neptun)" } },
    { "Hindi", new List<string> { "बुध (Budh)", "शुक्र (Shukra)", "पृथ्वी (Prithvi)", "मंगल (Mangal)", "बृहस्पति (Brihaspati)", "शनि (Shani)", "अरुण (Arun)", "वरुण (Varun)" } }
};

app.MapGet("/Planet/{language}", (string language) =>
{
    if (planetsDictionary.TryGetValue(language, out var planets))
    {
        Random rn = new Random();
        return Results.Ok(planets.OrderBy(p => rn.Next()).First());
    }

    return Results.NotFound($"Planets not found for language: {language}");
});


app.Run();
