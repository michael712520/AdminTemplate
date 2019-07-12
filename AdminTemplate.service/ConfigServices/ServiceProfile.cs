using System;
using AdminTemplate.DataBase.Models;
using AdminTemplate.service.Dto.baseReEntity;
using AdminTemplate.service.Dto.LatitudeDetail;
using AdminTemplate.service.Dto.LatitudeDetailItem;
using AdminTemplate.service.Dto.MbDetail;
using AdminTemplate.service.Dto.QtDetailItem;
using AutoMapper;

namespace AdminTemplate.service.ConfigServices
{
	public class ServiceProfiles : Profile
	{
		public ServiceProfiles()
		{
			CreateMap<MbDetailDto, MbDetail>()
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());
			CreateMap<MbDetailItemDto, MbDetailItem>()
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());
			CreateMap<LatitudeDetailDto, LatitudeDetail>()
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());
			CreateMap<LatitudeDetail, PairChildrenReEntity>()
				.ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
				.ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));
			CreateMap<LatitudeDetail, PairReEntity>()
			   .ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
			   .ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));

			CreateMap<LatitudeDetailItemDto, LatitudeDetailItem>()
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());
			CreateMap<LatitudeDetailItem, PairChildrenReEntity>()
				.ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
				.ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));
			CreateMap<LatitudeDetailItem, PairReEntity>()
				.ForMember(f => f.Label, opt => opt.MapFrom(f => f.Name))
				.ForMember(f => f.Value, opt => opt.MapFrom(f => f.Id));
			CreateMap<MbDetail, QtDetail>().ForMember(f => f.MbDetailId, opt => opt.MapFrom(f => f.Id))
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());
			CreateMap<MbDetailItem, QtDetailItem>()
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());
			CreateMap<QtDetailItemDto, QtDetailItem>()
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());
			CreateMap<QtDetail, QtDetailbatch>()
				.ForMember(f => f.CreateTime, opt => opt.Ignore())
				.ForMember(f => f.UpdateTime, opt => opt.Ignore());


		}
	}
}
