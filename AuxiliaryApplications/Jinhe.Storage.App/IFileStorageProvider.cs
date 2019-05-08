using Jinhe.Storage.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jinhe.Storage
{
    public interface IFileStorageProvider
    {
        /// <summary>
        /// 获取文件存储路径
        /// </summary>
        /// <param name="archive">档案</param>
        /// <param name="sort">sort</param>
        string GetStoragePhysicalPath(IStoredFile archive);


        /// <summary>
        /// 获取文件的流
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        Stream GetFileStream(IStoredFile archive);

        /// <summary>
        /// 获取指定文件存储路径
        /// </summary>
        /// <param name="archive">档案</param>
        /// <param name="sort">sort</param>
        string GetStoragePhysicalPath(IStoredFile archive,FileType fileType);

        /// <summary>
        /// 保存文件为PDF文件
        /// </summary>
        /// <param name="path"></param>
        void SaveFileToPdf(Impl.StoredFile archive, string path);

    }
}
