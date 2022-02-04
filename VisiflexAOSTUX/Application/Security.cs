using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VisiflexAOSTUX.Application
{
    public class Security
    {
        /// <summary>
        /// Crea un Hash basado en SHA256 de una cadena especificada
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Hash</returns>
        public static string SHA256Hash(string s)
        {
            StringBuilder sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(s));

                foreach (Byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}