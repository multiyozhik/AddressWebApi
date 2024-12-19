using AddressWebApi;
using System.Net.Http.Headers;
using System.Text.Json;

internal class StandartAddressApi
{
	private readonly HttpClient httpClient;

	private readonly string? token;

	private readonly string? secret;

	private readonly string? apiUrl;

	public StandartAddressApi(Uri baseUrl, IConfiguration config)
	{		
		token = config["Token"];
		secret = config["Secret"];
		apiUrl = config["ApiUrl"];

		httpClient = new HttpClient() {  BaseAddress = baseUrl 	};
	}

	public async Task<Address> GetStandartAddress(string addressData)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, new Uri(apiUrl));

		request.Headers.Authorization = new AuthenticationHeaderValue("Token", token);
		request.Headers.Add("X-Secret", secret);

		var addressList = new List<string>() { addressData };
		string addressListJson = JsonSerializer.Serialize(addressList);
		var contentList = new StringContent(addressListJson);

		contentList.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		request.Content = contentList;

		var response = await httpClient.SendAsync(request);

		var addresses = await response.Content.ReadFromJsonAsync<List<Address>>();
		return addresses[0];


		//return await response.Content.ReadAsStringAsync(); //исп. для возвр. json строки для конвертера Address класса
	}
}