using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Domain.Entities;

namespace disasterApi.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionDto, Region>().ReverseMap();
            CreateMap<RegionForCreationDto, Region>().ReverseMap();

            CreateMap<AlertSettingForCreationDto, AlertSetting>()
                .ReverseMap();

            CreateMap<AlertDto, Alert>().ReverseMap();
        }


    }
}
