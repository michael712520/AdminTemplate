using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GlobalConfiguration.Utility
{
    public class TreeManager
    {
        public TreeManager()
        {
        }

        /// <summary>
        /// 从列表中生成一个树，需要指定节点的标识字段和父节点的ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="idSelectorExpression"></param>
        /// <param name="parentIdSelectorExpression"></param>
        /// <returns></returns>
        public Tree<T> Generate<T>(List<T> list,
            Expression<Func<T, string>> idSelectorExpression,
            Expression<Func<T, string>> parentIdSelectorExpression)
            where T : class, new()
        {
            if (list == null || idSelectorExpression == null || parentIdSelectorExpression == null)
            {
                return null;
            }
            var type = typeof(T);
            var nameOfParentId = GetMemberValue<T>(type, parentIdSelectorExpression);
            var nameOfId = GetMemberValue<T>(type, idSelectorExpression);
            var propertyByParentId = type.GetProperty(nameOfParentId);
            var propertyById = type.GetProperty(nameOfId);
            var nodeList = new List<TreeNode<T>>();
            foreach (var item in list)
            {
                var parentId = (string)propertyByParentId.GetValue(item);
                var id = (string)propertyById.GetValue(item);
                if (string.IsNullOrEmpty(id))
                {
                    throw new NullReferenceException($"用于生成树的列表，指定的标识字段{nameOfId}存在空值");
                }
                var node = new TreeNode<T>(id, parentId, item);
                nodeList.Add(node);
            }
            var result = new Tree<T>(nodeList);
            return result;
        }


        protected String GetMemberValue<T>(Type type, Expression<Func<T, string>> expression)
        {
            var propertyName = ((MemberExpression)expression.Body).Member.Name;
            return propertyName;
        }
    }
}
