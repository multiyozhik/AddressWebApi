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
		var fullAddressDataString = await api.GetStandartAddress(addressData);
		await context.Response.WriteAsync(fullAddressDataString);

		//ToDo: ��������� ���������
		//ToDo: ������ �����. json-������, � ��� ���� � Address
		//ToDo: ������� AddressResponse ������ � ������� ������
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