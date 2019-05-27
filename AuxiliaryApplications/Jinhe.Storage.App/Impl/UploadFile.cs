using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Jinhe.CoreFx.Extensions;

namespace Jinhe.Storage.Impl
{
    internal class UploadFile
    {
        public UploadFile(Stream fileStream)
        {
            _fileStream = fileStream;
        }
        private long _length = -1;

        /// <summary>
        /// 文件大小（字节数）
        /// </summary>
        /// <returns></returns>
        public  long GetSize()
        {
            if (_length == -1 && _fileStream != null)
                _length = _fileStream.Length;
            return _length;
        }

        private string _md5;
        private Stream _fileStream;

        public  string GetMd5()
        {
            if (string.IsNullOrEmpty(_md5) && _fileStream != null)
                _md5 = _fileStream.ComputeHash();
            return _md5;
        }


        /// <summary>
        /// 文件保存
        /// </summary>
        /// <param name="fullPath"></param>
        public void SaveAs(string fullPath)
        {
            _fileStream.SaveAsFile(fullPath);
        }
    }
}
