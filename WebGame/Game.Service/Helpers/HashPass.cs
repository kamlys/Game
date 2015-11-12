using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Helpers
{
    public class HashPass : IHashPass
    {
        public string GeneratePassword(string password)
        {
            string finalString = password;

            return GetPassword(finalString);
        }

        public string GetPassword(string message)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = Encoding.Unicode.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return Convert.ToBase64String(resultBytes);
        }

        
        public bool ValidationPassword(string password, string hashPassword)
        {
            string finalString = password;
            return hashPassword == GetPassword(finalString);
        }
    }
}
