using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalConfiguration.Utility
{
    public class Tree<T> : List<TreeNode<T>>
    {
        protected List<TreeNode<T>> AllNode { private set; get; }

        private readonly List<TreeNode<T>> _tree;

        private readonly Dictionary<int, List<TreeNode<T>>> _depthDict;

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { set; get; }


        public Tree(List<TreeNode<T>> treeNodes)
        {
            AllNode = treeNodes;
            _depthDict = new Dictionary<int, List<TreeNode<T>>>();
            _tree = BuildTree(this.AllNode);
            base.AddRange(_tree);
        }

        public IEnumerable<TreeNode<T>> GetNodeByDepth(int depth)
        {
            return AllNode.Where(x => x.Depth == depth);
        }

        /// <summary>
        /// 查询指定父节点下的特定层节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public IEnumerable<TreeNode<T>> GetNodeByDepth(string parentId, int depth)
        {
            return AllNode.Where(x => x.Depth == depth && x.Path.Contains(parentId));
        }

        public TreeNode<T> Search(string id)
        {
            return AllNode.FirstOrDefault(x => x.Id == id);
        }

        public TreeNode<T> Search(string id, int depth)
        {
            return _depthDict[depth].First(x => x.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        protected List<TreeNode<T>> BuildTree(List<TreeNode<T>> nodeList)
        {
            if (nodeList != null || nodeList.Any())
            {
                List<TreeNode<T>> tree = new List<TreeNode<T>>();
                foreach (var item in nodeList)
                {
                    TreeNode<T> parent = null;
                    if (!string.IsNullOrEmpty(item.ParentId))
                    {
                        parent = nodeList.Where(x => x.Id == item.ParentId && x != item).FirstOrDefault();
                    }
                    if (parent != null)
                    {
                        item.AppendTo(parent);
                    }
                    else
                    {
                        tree.Add(item);
                    }
                    if (Depth < item.Depth)
                    {
                        Depth = item.Depth;
                    }
                    if (!_depthDict.Keys.Contains(item.Depth))
                    {
                        _depthDict[item.Depth] = new List<TreeNode<T>>();
                        _depthDict[item.Depth].Add(item);
                    }
                    else
                    {
                        _depthDict[item.Depth].Add(item);
                    }
                }
                return tree;
            }
            return null;
        }

    }
}
