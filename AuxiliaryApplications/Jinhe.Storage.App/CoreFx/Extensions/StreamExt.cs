using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Jinhe.CoreFx.Extensions
{
    public static class StreamExt
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string ComputeHash(this Stream inputStream)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            var bytes = md5Hasher.ComputeHash(inputStream);
            var md5 = BitConverter.ToString(bytes);
            md5Hasher.Clear();
            md5 = md5.Replace("-", "");
            inputStream.Position = 0;
            return md5;

        }

        /// <summary>
        /// 新建并保存文件
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="fullPath"></param>
        public static void SaveAsFile(this Stream inputStream, string fullPath)
        {
            const int bufferSize = 1024 * 16; //自定义缓冲区大小16K  
            var directoryPath = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                var buffer = new byte[bufferSize];
                inputStream.Seek(0, SeekOrigin.Begin);
                var bw = new BinaryWriter(fs);
                while (inputStream.Read(buffer, 0, bufferSize) > 0)
                {
                    bw.Write(buffer);
                }
                bw.Close();
                fs.Close();
            }
        }
    }
}
