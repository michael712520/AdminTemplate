using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminTemplate.service.BaseServices;
using GlobalConfiguration.Utility;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AdminTemplate.service.Services
{
    public class WeChatService : BaseService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <param name="js_code"></param>
        /// <param name="grant_type"></param>
        /// <returns></returns>
        public NetResult jscode2session(string appid, string secret, string js_code, string grant_type = "authorization_code")
        {
            var result = HttpHelper.HttpGet($"https://api.weixin.qq.com/sns/jscode2session", $"appid={appid}&secret={secret}&js_code={js_code}&grant_type={grant_type}");
            var data = JsonConvert.DeserializeObject<dynamic>(result);
            return ResponseBodyEntity(data);
        }

        public NetResult UpdateQtDetail(string qtDetailId, string batchNumber, string openid)
        {
            var qtDetail = DbContext.QtDetail.FirstOrDefault(p => p.Id.Equals(qtDetailId));
            if (qtDetail != null)
            {
                qtDetail.Openid = qtDetailId;
                DbContext.QtDetail.Update(qtDetail);

            }

            var qtDetailbatch = DbContext.QtDetailbatch.FirstOrDefault(p =>
                   p.QtDetailId.Equals(qtDetailId) && p.BatchNumber.Equals(batchNumber));
            if (qtDetailbatch != null)
            {
                qtDetailbatch.Openid = openid;
                DbContext.QtDetailbatch.Update(qtDetailbatch);

            }


            DbContext.SaveChanges();
            return ResponseBodyEntity();
        }
        public NetResult ListQtDetailbatch(string openid, PaginationStartAndLengthFilter filter)
        {

            var query = DbContext.QtDetail.Include(o => o.QtDetailbatch).Where(p => p.Openid.Equals(openid)).OrderByDescending(o=>o.CreateTime);


            var count=query.Count();
            var list = query.Skip(filter.Start).Take(filter.Length).ToList();
            return ResponseBodyEntity(list, count);
        }
    }
}
