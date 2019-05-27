using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jinhe.Storage.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class PathResolver
    {
        /// <summary>
        /// 
        /// </summary>
        public string BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload");

    

 

        public string GetFileName(string fileId)
        {
            var mimeIndex = Convert.ToInt32(GetMimeIndex(fileId));
            // 获取文件扩展名
            var ext = MimeType.GetMimeByIndex(mimeIndex);
            var fileName = $"{fileId}{ext}";
            return fileName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public string GetDateString(string fileId)
        {
            try
            {
                return fileId.Substring(6, 6);
            }
            catch
            {
                throw new FormatException("提供的参数id的值为非法格式，无法解析文件的DateString值");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetMimeIndex(string id)
        {
            try
            {
                return id.Substring(12, 3);
            }
            catch
            {
                throw new FormatException("提供的参数id的值为非法格式，无法解析文件的MimeIndex值");
            }
        }

        public string GetMd5(string id)
        {
            try
            {
                return id.Substring(15, 32);
            }
            catch
            {
                throw new FormatException("提供的参数id的值为非法格式，无法解析文件的MD5值");
            }
        } 

        #region 内部工具方法
        private static string PadLeft(int i, int total, char pad)
        {
            return i.ToString().PadLeft(total, pad);
        }

        private static string FormatCollectionIndex(int index)
        {
            return PadLeft(index, 6, '0');
        }

        private static string FormatMimeIndex(int index)
        {
            return PadLeft(index, 3, '0');
        }
        #endregion
    }
}
