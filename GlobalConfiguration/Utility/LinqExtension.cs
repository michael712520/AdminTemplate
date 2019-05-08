using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace GlobalConfiguration.Utility
{
    public static class LinqExtension
    {
        /// <summary>
        /// 按指定字段排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <param name="field"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> q, string field, OrderDirection direction)
        {
            if (string.IsNullOrEmpty(field))
                return q;
            var queryType = typeof(T);
            field = GetField(queryType, field);
            var param = Expression.Parameter(queryType, "p");
            var prop = Expression.Property(param, field);
            var exp = Expression.Lambda(prop, param);
            string method = direction == OrderDirection.Asc ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }

        /// <summary>
        /// 按指定字段排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <param name="field"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> q, string field, OrderDirection direction)
        {
            if (string.IsNullOrEmpty(field))
                return q;
            var queryType = typeof(T);
            field = GetField(queryType, field);
            var param = Expression.Parameter(queryType, "p");
            var prop = Expression.Property(param, field);
            var exp = Expression.Lambda(prop, param);
            string method = direction == OrderDirection.Asc ? "ThenBy" : "ThenByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce) as IOrderedQueryable<T>;
        }

        /// <summary>
        /// 获取主键字段名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<String> GetPrimaryKey<T>()
        {
            return typeof(T).GetProperties().Where(x => x.GetCustomAttributes(typeof(KeyAttribute), true).Any()).Select(x => x.Name);
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            if (type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }

        /// <summary>
        /// 获取类型的字段名称（支持按首字母小写检索到首字母大写的字段）,=
        /// </summary>
        /// <param name="type"></param>
        /// <param name="field"></param>
        /// <returns>返回null，说明不存在这个名称的属性</returns>
        public static string GetField(Type type, string field)
        {
            // 如果没有同名的属性，则将首字母转为大写重新查询
            if (type.GetProperties().All(x => x.Name != field))
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                TextInfo text = cultureInfo.TextInfo;
                var firstChar = field.Substring(0, 1).ToUpper();
                field = $"{firstChar}{field.Substring(1)}";
                field = type.GetProperties().FirstOrDefault(x => x.Name == field).Name;
            }
            return field;
        }
    }
}
