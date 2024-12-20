using AddressWebApi;
using Microsoft.OpenApi.Models;
using Serilog;

//https://localhost:44317/getStandartAddress/мск сухонска 11-89



var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("AddressApiConfig.json");

ConfigureServices(builder.Services, builder.Configuration);

builder.Host.UseSerilog((context, logConfig) => logConfig
	.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

Configure(app, app.Environment);


app.MapGet(
	"/getStandartAddress/{addressData}", 
	async (HttpContext context, IAddressDetailsService service, string addressData) => 
	{		
		var addressResponse = await service.GetAddressDetails(addressData);
		var response = context.Response;
		response.Headers.ContentType = "application/json";
		await response.WriteAsJsonAsync(addressResponse);
	});

app.Run();


static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	services.Configure<AddressApiConfig>(config);
	services.AddHttpClient();
	services.AddAutoMapper(typeof(AppMappingProfile));
	services.AddScoped<IDaDataClient, DaDataClient>();
	services.AddScoped<IAddressDetailsService, AddressDetailsService>();

	services.AddOpenApi();
	services.AddEndpointsApiExplorer();
	services.AddSwaggerGen(options =>
	{
		options.SwaggerDoc("v1", new OpenApiInfo
		{
			Version = "v1",
			Title = "AddressWebApi",
			Description = "An ASP.NET Core Web API to get only the required address fields"
		});
	});
	services.AddCors();
}

static void Configure(WebApplication app, IHostEnvironment env)
{
	app.UseExceptionHandler(exceptionHandlerApp =>
			exceptionHandlerApp.Run(async context =>
				await Results.Problem().ExecuteAsync(context)));

	if (env.IsDevelopment())
	{
		app.MapOpenApi();
		app.UseSwagger(options =>
		{
			options.SerializeAsV2 = true;
		});
		app.UseSwaggerUI(options => 
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			options.RoutePrefix = string.Empty;
		});
	}

	app.UseStatusCodePages();
	app.UseHttpsRedirection();
	app.UseCors(builder => builder.AllowAnyOrigin());
}