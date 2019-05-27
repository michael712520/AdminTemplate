using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Primitives;

namespace Jinhe.Storage.Impl
{
    public class StoredFile : IStoredFile
    {
        private PathResolver _pathResolver = new PathResolver();
        public StoredFile(StringSegment fileName, string md5)
        {
            RawName = System.IO.Path.GetFileNameWithoutExtension(fileName).ToString();
            ExtensionName = System.IO.Path.GetExtension(fileName).ToString().ToLower();
            if (!ExtensionName.StartsWith("."))
            {
                ExtensionName = $".{ExtensionName}";
            }
            MimeType = MimeType.GetMimeByExt(ExtensionName);
            DateString = DateTime.Now.ToString("yyMMdd");
            Md5 = md5;
            FileId = $"{GetStorageZone()}{ DateString }{FormatMimeIndex(MimeType.Index)}{md5}";
            RelativePath = FileId;
        }

        public StoredFile(string fileId)
        {
            FileId = fileId;
            Md5 = _pathResolver.GetMd5(FileId);
            DateString = _pathResolver.GetDateString(FileId);
            var index = _pathResolver.GetMimeIndex(FileId);
            MimeType = MimeType.GetMimeByIndex(int.Parse(index));
            ExtensionName = MimeType.Ext;
            RelativePath = fileId;
        }

        public MimeType MimeType { set; get; }

        public string FileId { set; get; }

        public string Md5 { set; get; }

        /// <summary>
        /// 原始的文件名称（含扩展名）
        /// </summary>
        public string RawName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            get
            {
                return $"{GetStorageZone()}/{ DateString }/{FormatMimeIndex(MimeType.Index)}/{Md5}{MimeType.Ext}";
            }
        }

        public string ExtensionName { set; get; }

        /// <summary>
        /// yyMMdd
        /// </summary>
        public string DateString { get; protected set; }

        /// <summary>
        /// 相对路径== fileId
        /// </summary>
        public string RelativePath { set; get; }


        /// <summary>
        /// 存储区域 6位
        /// </summary>
        /// <param name="archive"></param>
        /// <returns></returns>
        public string GetStorageZone()
        {
            return $"Z00000";
        }

        protected string FormatMimeIndex(int index)
        {
            return index.ToString().PadLeft(3, '0');
        }

    }
}
