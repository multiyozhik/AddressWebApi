using AutoMapper;
using Serilog;

namespace AddressWebApi
{
	interface IAddressDetailsService
	{
		Task<AddressResponse> GetAddressDetails(string address);
	}
	class AddressDetailsService : IAddressDetailsService
	{
		private readonly IDaDataClient client;

		private readonly IMapper mapper;

		private readonly ILogger<AddressDetailsService> logger;

		public AddressDetailsService(IDaDataClient client, IMapper mapper, ILogger<AddressDetailsService> logger)
		{
			this.client = client;
			this.mapper = mapper;
			this.logger = logger;
		}

		public async Task<AddressResponse> GetAddressDetails(string addressData)
		{
			logger.LogInformation($"!!! Request of details for address has been sent");
			var addressList = await client.GetStandartAddress(addressData);
			var addressResponse = mapper.Map<AddressResponse>(addressList[0]);
			logger.LogInformation($"!!! Response was received");
			return addressResponse;
		}
	}
}
