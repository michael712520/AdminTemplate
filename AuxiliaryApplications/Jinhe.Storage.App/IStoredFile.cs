using System;
using System.Collections.Generic;
using System.Text;

namespace Jinhe.Storage
{
    public interface IStoredFile
    {
        /// <summary>
        /// 编号(内部
        /// </summary>
        string FileId {  get; }

        string ExtensionName { set; get; }

        /// <summary>
        /// 相当于上传目录的相对路径
        /// </summary>
        string Path {  get; }

        /// <summary>
        /// 文件的MD5
        /// </summary>
        string Md5 {  get; }

        /// <summary>
        /// 相对路径，相对于服务地址
        /// </summary>
        string RelativePath {  get; }

    }
}
