using AddressWebApi;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;


interface IDaDataClient
{
	Task<List<Address>> GetStandartAddress(string addressData);
}

class DaDataClient: IDaDataClient
{
	private readonly IHttpClientFactory httpClientFactory;

	private readonly AddressApiConfig addressApiConfig;

	public DaDataClient(IHttpClientFactory httpClientFactory, IOptions<AddressApiConfig> options)
	{
		this.httpClientFactory = httpClientFactory;

		addressApiConfig = options.Value;
	}

	public async Task<List<Address>> GetStandartAddress(string addressData)
	{
		//throw new Exception("ошибка");

		var request = new HttpRequestMessage(HttpMethod.Post, new Uri(addressApiConfig.ApiUrl));

		request.Headers.Authorization = new AuthenticationHeaderValue("Token", addressApiConfig.Token);
		request.Headers.Add("X-Secret", addressApiConfig.Secret);

		var addressList = new List<string>() { addressData };
		string addressListJson = JsonSerializer.Serialize(addressList);
		var contentList = new StringContent(addressListJson);

		contentList.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		request.Content = contentList;

		var httpClient = httpClientFactory.CreateClient();
		var response = await httpClient.SendAsync(request);
		
		return await response.Content.ReadFromJsonAsync<List<Address>>();
	}
}