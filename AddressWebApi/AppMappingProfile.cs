using AutoMapper;
using System;

namespace AddressWebApi
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<Address, AddressResponse>()
				.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Postal_code))
				.ForMember(dest => dest.FederalDistrict, opt => opt.MapFrom(src => src.Federal_district))
				.ForMember(dest => dest.RegionWithType, opt => opt.MapFrom(src => src.Region_with_type));
		}
	}
}
