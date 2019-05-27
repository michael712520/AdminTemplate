namespace GlobalConfiguration.Utility
{
    /// <summary>
    /// 排序方向
    /// </summary>
    public enum OrderDirection
    {
        Asc,
        Desc
    }

    /// <summary>
    /// 查询参数
    /// </summary>
    public class QueryParameter
    {


        public string OrderField { set; get; }

        public OrderDirection OrderDirection { set; get; }

        public int State { set; get; }

        /// <summary>
        /// 从1开始
        /// </summary>
        public int Page { set; get; }

        public int PageSize { set; get; }

        public string Keywords { set; get; }

        /// <summary>
        /// 从0开始
        /// </summary>
        public int PageIndex => Page > 0 ? Page - 1 : 0;

        /// <summary>
        /// 存在分页参数
        /// </summary>
        public bool HasPageParams => Page >= 1 && PageSize >= 1;
    }
}
