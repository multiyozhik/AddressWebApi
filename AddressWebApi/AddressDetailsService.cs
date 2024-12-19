using AutoMapper;

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

		public AddressDetailsService(IDaDataClient client, IMapper mapper)
		{
			this.client = client;
			this.mapper = mapper;
		}

		public async Task<AddressResponse> GetAddressDetails(string addressData)
		{
			var addressList = await client.GetStandartAddress(addressData);
			return mapper.Map<AddressResponse>(addressList[0]);
		}
	}
}
