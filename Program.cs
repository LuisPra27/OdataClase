using Microsoft.AspNetCore.OData;
//Permite agregar y configurar OData en ASP.NET Core

using Microsoft.OData.ModelBuilder;
//Configura el modelo OData para definir entidades y sus relaciones

var builder = WebApplication.CreateBuilder(args);

// Configurar nuestro modelo OData
var odataBuilder = new ODataConventionModelBuilder();
//Crea un modelo OData basado en convenciones

odataBuilder.EntitySet<WeatherForecast>("WeatherForecasts");
//Define un conjunto de entidades llamado "WeatherForecasts" que representa la entidad WeatherForecast

var edmModel = odataBuilder.GetEdmModel();
//Obtiene el modelo EDM (Entity Data Model) a partir del ODataConventionModelBuilder

// Agrega servicios
builder.Services.AddControllers()
    .AddOData(opt => opt.AddRouteComponents("odata", edmModel)
                        .Select()
                        .Filter()
                        .OrderBy()
                        .Expand()
                        .SetMaxTop(100)
                        .Count());

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// Register controllers
app.MapControllers();

// Register a simple GET endpoint
app.MapGet("/", () => "API OData funcionando. Visita /odata/WeatherForecasts");

app.Run();
