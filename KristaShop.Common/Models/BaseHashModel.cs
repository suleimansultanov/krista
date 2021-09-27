﻿using System.Security.Cryptography;
using System.Text;

namespace KristaShop.Common.Models
{
    public class BaseHashModel
    {
        public string HashCode { get; set; }
    }

    public static class HashRequestData
    {
        public static string ComputeSha256Hash(this string data)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data + GlobalConstant.SHA256_SALT));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}