using IDBWeatherForecastBackend.Services;
using IDBWeatherForecastBackend.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<AccuweatherOptions>((serviceProvider) =>
{
    AccuweatherOptions options = new();
    builder.Configuration.GetSection(AccuweatherOptions.Accuweather).Bind(options);
    return options;
});
builder.Services.AddScoped<IWeatherForecastServices, AccuWeatherService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
