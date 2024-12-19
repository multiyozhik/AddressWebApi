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
	private readonly HttpClient httpClient;

	private readonly DaData daData;

	public DaDataClient(IOptions<DaData> options)
	{
		daData = options.Value;

		httpClient = new HttpClient();
	}

	public async Task<List<Address>> GetStandartAddress(string addressData)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, new Uri(daData.ApiUrl));

		request.Headers.Authorization = new AuthenticationHeaderValue("Token", daData.Token);
		request.Headers.Add("X-Secret", daData.Secret);

		var addressList = new List<string>() { addressData };
		string addressListJson = JsonSerializer.Serialize(addressList);
		var contentList = new StringContent(addressListJson);

		contentList.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		request.Content = contentList;

		var response = await httpClient.SendAsync(request);

		return await response.Content.ReadFromJsonAsync<List<Address>>();
	}
}