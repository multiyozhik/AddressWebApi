using AddressWebApi;
using AutoMapper;

//https://localhost:44317/getStandartAddress/мск сухонска 11-89

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

Configure(app);


app.MapGet(
	"/getStandartAddress/{addressData}", async
	(HttpContext context, IAddressDetailsService service, string addressData) => 
	{		
		var addressResponse = await service.GetAddressDetails(addressData);
		var response = context.Response;
		response.Headers.ContentType = "application/json";
		await response.WriteAsJsonAsync(addressResponse);
	});

app.Run();


static void ConfigureServices(IHostApplicationBuilder builder)
{
	builder.Configuration.AddJsonFile("dadata.json");
	var services = builder.Services;
	services.Configure<DaData>(builder.Configuration);
	services.AddOpenApi();
	services.AddAutoMapper(typeof(AppMappingProfile));
	services.AddScoped<IDaDataClient, DaDataClient>();
	services.AddScoped<IAddressDetailsService, AddressDetailsService>();
}

static void Configure(WebApplication app)
{
	if (app.Environment.IsDevelopment())
	{
		app.MapOpenApi();
	}

	app.UseHttpsRedirection();
	
}