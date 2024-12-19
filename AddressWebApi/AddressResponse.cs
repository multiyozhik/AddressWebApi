namespace AddressWebApi
{
	//federal_district
	//region_with_type - г Москва
	//city_area":"Северо-восточный"
	//city_district_with_type":"р-н Северное Медведково"
	public class AddressResponse
	{
		public string? PostalCode { get; set; }
		public string? Country { get; set; }
		public string? FederalDistrict { get; set; }
		public string? RegionWithType { get; set; }
		public string? CityDistrictWithType { get; set; }
		public string? Street { get; set; }
		public string? House { get; set; }
		public string? Flat { get; set; }
	}
}
