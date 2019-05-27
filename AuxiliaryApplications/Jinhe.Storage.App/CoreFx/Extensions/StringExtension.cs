
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Jinhe.CoreFx
{
    public static class StringExtension
    {
        public static string ComputeHash(this string content)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            var data = md5Hasher.ComputeHash(bytes);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string TrimEnd(this string str, char c, int length)
        {
            if (string.IsNullOrEmpty(str) || length <= 0)
            {
                return str;
            }
            if (length >= str.Length)
            {
                return str.TrimEnd(c);
            }
            while (length > 0)
            {
                if (str.EndsWith(c))
                {
                    str = str.Remove(str.Length - 1, 1);
                    length--;
                }
                else
                {
                    return str;
                }
            }
            return str;
        }
    }
}