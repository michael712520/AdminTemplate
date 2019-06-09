﻿using AdminTemplate.DataBase.Models;
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

        }
    }
}
