using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GlobalConfiguration.Utility
{
    public class CryptUtils
    {
        #region 对称加密 AES，DES 等

        /// <summary>
        /// 初始化向量
        /// </summary>
        private const string IV = "1234567890123456";

        /// <summary>
        /// 加密，解密key
        /// </summary>
        private const string Key = "1234567890123456";

        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="txt">明文字符串</param>  
        /// <returns>返回加密后的字符串</returns>  
        public static string AESEncrypt(string txt)
        {
            //分组加密算法  
            SymmetricAlgorithm des = Rijndael.Create();
            des.IV = Encoding.UTF8.GetBytes(IV);
            des.Key = Encoding.UTF8.GetBytes(Key);

            byte[] inputByteArray = Encoding.UTF8.GetBytes(txt);
            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                byte[] cipherBytes = ms.ToArray();
                cs.Close();
                ms.Close();
                return Convert.ToBase64String(cipherBytes);
            }
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="txt">密文字符串</param>  
        /// <returns>返回解密后的字符串</returns>  
        public static string AESDecrypt(string txt)
        {
            //分组加密算法  
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(Key);
            des.IV = Encoding.UTF8.GetBytes(IV);

            byte[] cipherText = Convert.FromBase64String(txt);
            byte[] decryptBytes = new byte[cipherText.Length];
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
                cs.Read(decryptBytes, 0, decryptBytes.Length);
                cs.Close();
                ms.Close();
                return System.Text.Encoding.UTF8.GetString(decryptBytes);
            }
        }

        #endregion

        #region 非对称加密 RAS等

        /// <summary>
        /// 创建RSA公钥私钥
        /// </summary>
        /// <param name="privateKeyPath"></param>
        /// <param name="publicKeyPath"></param>
        public static void CreateRSAKey(string privateKeyPath, string publicKeyPath)
        {
            //创建RSA对象
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //生成RSA[公钥私钥]
            string privateKey = rsa.ToXmlString(true);
            string publicKey = rsa.ToXmlString(false);
            //将密钥写入指定路径
            File.WriteAllText(privateKeyPath, privateKey);//文件内包含公钥和私钥
            File.WriteAllText(publicKeyPath, publicKey);//文件内只包含公钥
        }

        /// <summary>
        /// 使用RSA实现加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="data">加密后数据</param>
        /// <returns></returns>
        public static string RSAEncrypt(string publicKey, string data)
        {
            //C#默认只能使用[公钥]进行加密(想使用[公钥解密]可使用第三方组件BouncyCastle来实现)
            //创建RSA对象并载入[公钥]
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
            rsaPublic.FromXmlString(publicKey);
            //对数据进行加密
            byte[] publicValue = rsaPublic.Encrypt(Encoding.UTF8.GetBytes(data), false);
            string publicStr = Convert.ToBase64String(publicValue);//使用Base64将byte转换为string
            return publicStr;
        }

        /// <summary>
        /// 使用RSA实现解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string RSADecrypt(string privateKey, string data)
        {
            //C#默认只能使用[私钥]进行解密(想使用[私钥加密]可使用第三方组件BouncyCastle来实现)
            //创建RSA对象并载入[私钥]
            RSACryptoServiceProvider rsaPrivate = new RSACryptoServiceProvider();
            rsaPrivate.FromXmlString(privateKey);
            //对数据进行解密
            byte[] privateValue = rsaPrivate.Decrypt(Convert.FromBase64String(data), false);//使用Base64将string转换为byte
            string privateStr = Encoding.UTF8.GetString(privateValue);
            return privateStr;
        }

        #endregion

        #region 摘要加密(Hash加密) MD5，SHA1等

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                //转成大写
                sb.Append(hashBytes[i].ToString("X2"));

                //或者 转成小写
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }

        /// <summary>  
        /// 获取文件的MD5码  
        /// </summary>  
        /// <param name="fileName">传入的文件名（含路径及后缀名）</param>  
        /// <returns></returns>  
        public static string FileMD5(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("获取文件MD5失败，" + ex.Message);
            }
        }

        /// <summary>
        /// Sha1加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Sha1(string input)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] dataToHash = System.Text.Encoding.Default.GetBytes(input);
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            string hashed = BitConverter.ToString(dataHashed).Replace("-", "");
            return hashed;
        }

        #endregion
    }
}
