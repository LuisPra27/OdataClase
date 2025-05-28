using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configura modelo OData
var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<WeatherForecast>("WeatherForecasts");
var edmModel = odataBuilder.GetEdmModel();

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
