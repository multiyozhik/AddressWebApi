# AddressWebApi


Для получения структуры класса Address, объект которого возвращается из api от Dadata, - 
в методе GetStandartAddress() вначале возвращалась строка
return await response.Content.ReadAsStringAsync(); //исп. для возвр. json строки для конвертера Address класса.
Соответственно в Program.cs выводилась просто строка в response, с помощью конвертера, например, по ссылке https://json2csharp.com/, 
получаем вид класса Address.

app.MapGet(
	"/api/getStandartAddress/{addressData}", async
	(HttpContext context, string addressData) => 
	{
		var fullAddressDataString = await api.GetStandartAddress(addressData);
		var response = context.Response;
		response.Headers.ContentLanguage = "ru-RU";
		response.Headers.ContentType = "text/plain; charset=utf-8";
		await response.WriteAsync(fullAddressDataString, encoding: Encoding.UTF8);
	});