using System;
using System.Collections.Generic;
using System.Text;
using AdminTemplate.service.BaseServices;
using GlobalConfiguration.Utility;

namespace AdminTemplate.service.Services
{
    public class LatitudeCategoryService : BaseService
    {
        public NetResult List()
        {
            return ResponseBodyEntity();
        }
    }
}
