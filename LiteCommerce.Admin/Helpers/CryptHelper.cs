using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// các hàm dùng cho mã hóa/ giải mã
    /// </summary>
    public class CryptHelper
    {
        /// <summary>
        /// Mã hóa md5 chuỗi text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string  Md5(string text)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}