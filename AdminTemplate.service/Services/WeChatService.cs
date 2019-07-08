using System;
using System.Collections.Generic;
using System.Text;
using AdminTemplate.service.BaseServices;
using GlobalConfiguration.Utility;
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
		public NetResult jscode2session(string appid, string secret, string js_code,string grant_type= "authorization_code")
		{
			var result = HttpHelper.HttpGet($"https://api.weixin.qq.com/sns/jscode2session", $"appid={appid}&secret={secret}&js_code={js_code}&grant_type={grant_type}");
			var data = JsonConvert.DeserializeObject<dynamic>(result);
			return ResponseBodyEntity(data);
		}
	}
}
