using AdminTemplate.DataBase.Models;
using AdminTemplate.service.Dto.baseReEntity;
using AdminTemplate.service.Dto.LatitudeDetail;
using AdminTemplate.service.Dto.MbDetail;
using AutoMapper;

namespace AdminTemplate.service.ConfigServices
{
    public class ServiceProfiles : Profile
    {
        public ServiceProfiles()
        {
            CreateMap<MbDetailDto, MbDetail>();
            CreateMap<MbDetailItemDto, MbDetailItem>();
            CreateMap<LatitudeDetailDto, LatitudeDetail>();
            CreateMap<LatitudeDetail, PairChildrenReEntity>()
                .ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
                .ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id))
                .ForMember(f => f.Children, opt => opt.MapFrom(f => f.MbDetailItem));
            CreateMap<LatitudeDetail, PairReEntity>()
                .ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
                .ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));
        }
    }
}
