using AutoMapper;
using System;

namespace AddressWebApi
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<Address, AddressResponse>()
				.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.postal_code))
				.ForMember(dest => dest.FederalDistrict, opt => opt.MapFrom(src => src.federal_district))
				.ForMember(dest => dest.RegionWithType, opt => opt.MapFrom(src => src.region_with_type))
				.ForMember(dest => dest.CityDistrictWithType, opt => opt.MapFrom(src => src.city_district_with_type));
		}
	}
}
