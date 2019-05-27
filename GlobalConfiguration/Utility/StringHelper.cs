using IdentityServer4.Models;
using System;

namespace GlobalConfiguration.Utility
{
    public class StringHelper
    {
        public static string NewID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string HashHelper(string input)
        {
            //return input;
            return CryptUtils.MD5(input);
            //return BCrypt.Net.BCrypt.HashPassword(input);
        }

        public static string ShaHelper(string input)
        {
            return input.Sha256();
        }
    }
}
