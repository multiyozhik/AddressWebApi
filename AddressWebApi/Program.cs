using AddressWebApi;
using System.Text;
using System.Text.Json;

//https://localhost:44317/api/getStandartAddress/��� �������� 11/-89

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app);

var baseUrl = new Uri("https://localhost:44317");

var api = new StandartAddressApi(baseUrl, app.Configuration);


app.MapGet(
	"/api/getStandartAddress/{addressData}", async
	(HttpContext context, string addressData) => 
	{

		var address = await api.GetStandartAddress(addressData);
		var response = context.Response;
		response.Headers.ContentType = "application/json";
		await response.WriteAsJsonAsync(address);


		//��� ���� ������������� ��� ��������� ����������� Address ������ �� ���������� json ������
		//var fullAddressDataString = await api.GetStandartAddress(addressData);
		//var response = context.Response;
		//response.Headers.ContentLanguage = "ru-RU";
		//response.Headers.ContentType = "text/plain; charset=utf-8";
		//await response.WriteAsync(fullAddressDataString, encoding: Encoding.UTF8);


		//ToDo: � ���. AutoMapper ��������������
		//ToDo: ��-�����, ���-�� � ���������� �������������� ���� ������������ �������
	});

app.Run();


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