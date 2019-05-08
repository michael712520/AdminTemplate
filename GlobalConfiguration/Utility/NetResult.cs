namespace GlobalConfiguration.Utility
{
    public class NetResult
    {
        public NetResult()
        {
            this.Code = EnumResult.Success;
        }

        public NetResult(string message)
        {
            this.Code = EnumResult.Error;
            this.Message = message;
        }

        public EnumResult? Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class NetResult<T> : NetResult
    {
        public NetResult()
        {
            Code = EnumResult.Success;
        }

        public NetResult(string msg) : base(msg)
        {
            Code = EnumResult.Error;
            Message = msg;
        }

        public new T Data { get; set; }
    }

    public enum EnumResult
    {
        Success = 200,
        Error = 500,
    }
}
