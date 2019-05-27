using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalConfiguration.Utility
{
    public class LoopParentNodeException : Exception
    {
        public LoopParentNodeException(string message) : base(message)
        {
        }
    }

    public class TreeNode<T>
    {
        public TreeNode(string id, string parentId, T t)
        {
            Id = id;
            ParentId = parentId;
            NodeValue = t;
        }

        public string Id { set; get; }

        public string ParentId { set; get; }

        public TreeNode<T> Parent { private set; get; }

        public T NodeValue { set; get; }

        public List<TreeNode<T>> Children { private set; get; }

        private List<string> _path;
        public List<string> Path
        {
            private set
            {
                _path = value;
            }
            get
            {
                if (_path == null)
                {
                    return new List<string>() { Id };
                }
                return _path;
            }
        }

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { private set; get; }

        private string _pathString = String.Empty;
        public string PathString
        {
            get
            {
                return string.Join(",", Path);
            }
        }

        /// <summary>
        /// 将节点附加到父节点的Children列表
        /// </summary>
        /// <param name="parent"></param>
        public void AppendTo(TreeNode<T> parent)
        {
            if (parent == null)
            {
                return;
            }
            if (parent.Parent == this || parent.Path.Contains(Id))
            {
                throw new LoopParentNodeException("存在两个节点相互为对方父节点（圈引用）");
            }
            Parent = parent;
            Path = parent.Path.Concat(new List<string>() { Id }).ToList();
            Depth = parent.Depth + 1;
            Parent.Include(this);
            RefreshChildren();
        }

        /// <summary>
        /// 包含子节点
        /// </summary>
        /// <param name="childNode"></param>
        public void Include(TreeNode<T> childNode)
        {
            if (Children == null)
            {
                Children = new List<TreeNode<T>>();
            }
            if (Children.Any(x => x == childNode))
            {
                return;
            }
            Children.Add(childNode);
        }

        public void MoveLeft()
        {
        }

        /// <summary>
        /// 设置子节点列表
        /// </summary>
        /// <param name="children"></param>
        public void SetChildren(IEnumerable<TreeNode<T>> children)
        {
            if (children == null)
            {
                return;
            }
            foreach (var item in children)
            {
                item.AppendTo(this);
            }
        }

        /// <summary>
        /// 更新子节点的关系
        /// </summary>
        protected void RefreshChildren()
        {
            if (Children != null && Children.Any())
            {
                foreach (var item in Children)
                {
                    item.AppendTo(this);
                }
            }
        }
    }
}
