using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GlobalConfiguration.Utility
{
    public class PaginationStartAndLengthFilter
    {
        public virtual int Start { set; get; }

        public virtual int Length { set; get; }

        public string Keywords { set; get; }

        public string OrderBy { set; get; }

        public OrderDirection OrderByDirection { set; get; } = OrderDirection.Asc;

        public string ThenBy { set; get; }
        public OrderDirection ThenByDirection { set; get; } = OrderDirection.Asc;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IOrderedQueryable<TSource> GetOrderedQuery<TSource>(IQueryable<TSource> query)
        {
            if (String.IsNullOrEmpty(OrderBy))
            {
                OrderBy = LinqExtension.GetPrimaryKey<TSource>().First();
            }
            if (!String.IsNullOrEmpty(OrderBy))
            {
                query = query.OrderBy(OrderBy, OrderByDirection);
                if (!String.IsNullOrEmpty(ThenBy))
                {
                    query = (query as IOrderedQueryable<TSource>).ThenBy(ThenBy, ThenByDirection);
                }
            }
            return query as IOrderedQueryable<TSource>;
        }
    }

    /// <summary>
    /// 强制分页过滤
    /// </summary>
    public class RequiredPaginationFilter : PaginationStartAndLengthFilter
    {
        [Required]
        public override int Start { set; get; }

        [Required]
        public override int Length { set; get; }
    }
}
