using System.Net.Http;
using System.Text.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.MapPost("/getStandartAddress", (...) =>
//{
//    new HttpClient(), await httpClient.PostAsync(uri, addressListContent)
//    return ��� ������, ����� �� ��� automapper ������������� �� ������ � 5 ������
//});

app.Run();
