using AdminTemplate.DataBase.Models;
using GlobalConfiguration.@base;
using GlobalConfiguration.Logging;
using GlobalConfiguration.Utility;
using MasRiverManager.Services.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
namespace AdminTemplate.service.BaseServices
{
	public abstract class BaseService
	{
		//public static readonly LoggerFactory MyLoggerFactory
		//	= new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
		public static readonly LoggerFactory LoggerFactory =
			new LoggerFactory(new[] { new DebugLoggerProvider((_, __) => true) });
		protected questionContext DbContext;
		private readonly NetResult _netResult;
		public ILogger Logger { get; } =
			ApplicationLogging.CreateLogger<BaseService>();

		protected BaseService()
		{
			var connectionString = AppConfig.GetConnectionString();//发布时候添加数据库连接配置
			var contextOptions = new DbContextOptionsBuilder<questionContext>().UseMySQL(connectionString).Options;
			DbContext = new questionContext(contextOptions);
			_netResult = new NetResult();
		}
		public NetResult ResponseBodyEntity()
		{
			_netResult.Data = "ok";

			_netResult.Code = EnumResult.Success;
			_netResult.Message = "true";
			return _netResult;
		}
		public NetResult ResponseBodyEntity(object data)
		{
			_netResult.Data = data;

			_netResult.Code = EnumResult.Success;
			_netResult.Message = "true";
			return _netResult;
		}
		public NetResult ResponseBodyEntity(object data, EnumResult enumResult = EnumResult.Success)
		{
			_netResult.Data = data;

			if (_netResult.Code == null)
			{
				_netResult.Code = EnumResult.Success;
			}
			else
			{
				_netResult.Code = enumResult;
			}
			_netResult.Message = "true";
			return _netResult;
		}
		public NetResult ResponseBodyEntity(object data, string message = "true")
		{
			_netResult.Data = data;
			_netResult.Message = message;
			return _netResult;
		}
		public NetResult ResponseBodyEntity(object data, EnumResult enumResult = EnumResult.Success, string message = "true")
		{
			_netResult.Data = data;
			_netResult.Code = _netResult.Code == null ? EnumResult.Success : enumResult;

			_netResult.Message = message;
			return _netResult;
		}
		public NetResult ResponseBodyEntity<T>(List<T> list, long total)
		{
			ListData<T> _list = new ListData<T>();
			_netResult.Code = EnumResult.Success;
			_list.list = list;
			_list.total = total;
			_netResult.Data = _list;
			_netResult.Message = "true";
			return _netResult;
		}
		public NetResult ResponseBodyEntity<T>(List<T> list, long total, EnumResult enumResult = EnumResult.Success)
		{
			ListData<T> _list = new ListData<T>();
			if (_netResult.Code == null)
			{
				_netResult.Code = EnumResult.Success;
			}
			else
			{
				_netResult.Code = enumResult;
			}
			_list.list = list;
			_list.total = total;
			_netResult.Data = _list;
			_netResult.Message = "true";
			return _netResult;
		}

		public NetResult ResponseBodyEntity<T>(List<T> list, long total, EnumResult enumResult = EnumResult.Success, string message = "true")
		{
			ListData<T> _list = new ListData<T>();
			if (_netResult.Code == null)
			{
				_netResult.Code = EnumResult.Success;
			}
			else
			{
				_netResult.Code = enumResult;
			}
			_list.list = list;
			_list.total = total;
			_netResult.Data = _list;
			_netResult.Message = message;
			return _netResult;
		}
	}
}
