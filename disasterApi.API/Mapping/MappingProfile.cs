using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Domain.Entities;

namespace disasterApi.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionDto, Region>();
            CreateMap<Region, RegionDto>()
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.LocationCoordinates.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForPath(dest => dest.LocationCoordinates.Longitude, opt => opt.MapFrom(src => src.Longitude));

            CreateMap<RegionForCreationDto, Region>().ReverseMap();

            CreateMap<AlertSettingForCreationDto, AlertSetting>()
                .ReverseMap();

            CreateMap<AlertDto, Alert>().ReverseMap();
            
            CreateMap<AlertSetting, AlertSettingDto>()
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId))
                .ForMember(dest => dest.DisasterType, opt => opt.MapFrom(src => src.DisasterType))
                .ForMember(dest => dest.ThresholdScore, opt => opt.MapFrom(src => (int)src.ThresholdScore));
        }


    }
}
