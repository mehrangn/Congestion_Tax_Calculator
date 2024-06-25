using AutoMapper;
using Congestion_Tax_Calculator.Domain;
using Congestion_Tax_Calculator.WebApi.Resource;

namespace Congestion_Tax_Calculator.WebApi.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehiclePassingDTO, VehiclePassing>()
                .ForMember(dest => dest.RegisterTime, opt => opt.MapFrom(src => src.RegisterTime))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle));

            CreateMap<VehicleDTO, Vehicle>()
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType))
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.PlateNumber));
            
            CreateMap<TollRecordDTO, TollRecord>().ReverseMap();
            

        }
    }
}
