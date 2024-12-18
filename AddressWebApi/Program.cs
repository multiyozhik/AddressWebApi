using System.Net.Http;
using System.Text.Json;
using System.Text;
using System;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app);

app.MapGet("/api/getStandartAddress", () => "StandartAddress");

app.Run();


//https://localhost:44317/

//app.MapGet("/getStandartAddress", (...) =>
//{
//    new HttpClient(), await httpClient.PostAsync(uri, addressListContent)
//    return всю модель, потом из нее automapper преобразовать на объект с 5 полями
//});


static void ConfigureServices(IServiceCollection services)
{
	services.AddOpenApi();
}

static void Configure(WebApplication app)
{
	if (app.Environment.IsDevelopment())
	{
		app.MapOpenApi();
	}

	app.UseHttpsRedirection();
}