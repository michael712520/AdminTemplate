using AdminTemplate.DataBase.Models;
using AdminTemplate.service.Dto.baseReEntity;
using AdminTemplate.service.Dto.LatitudeDetail;
using AdminTemplate.service.Dto.MbDetail;
using AutoMapper;
using Newtonsoft.Json;

namespace AdminTemplate.service.ConfigServices
{
    public class ServiceProfiles : Profile
    {
        public ServiceProfiles()
        {
            CreateMap<MbDetailDto, MbDetail>();
            CreateMap<MbDetailItemDto, MbDetailItem>()
                .ForMember(o => o.LatitudeDetailIds, opt => opt.MapFrom(o => o.LatitudeDetailIds.Count > 0 ? JsonConvert.SerializeObject(o.LatitudeDetailIds) : null));
            CreateMap<LatitudeDetailDto, LatitudeDetail>();
            CreateMap<LatitudeDetail, PairChildrenReEntity>()
                .ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
                .ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));
            CreateMap<LatitudeDetail, PairReEntity>()
               .ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
               .ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));

            CreateMap<LatitudeDetailDto, LatitudeDetailTwo>();
            CreateMap<LatitudeDetailTwo, PairChildrenReEntity>()
                .ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
                .ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id))
              ;
            CreateMap<LatitudeDetailTwo, PairReEntity>()
                .ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
                .ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));
        }
    }
}
