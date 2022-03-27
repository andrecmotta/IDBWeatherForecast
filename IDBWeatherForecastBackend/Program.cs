using IDBWeatherForecastBackend.Services;
using IDBWeatherForecastBackend.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;


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

//Using this to run the SPA application and the backend on the same app.
var options = new FileServerOptions()
{
    FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), "app")),
    RequestPath = "",

};

options.DefaultFilesOptions.DefaultFileNames.Add("index.html");
app.UseFileServer(options);

app.Run();
