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
        private static RNGCryptoServiceProvider _cryptop = new RNGCryptoServiceProvider();
        private int _saltSize = 25;


        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        //static string GetString(byte[] bytes)
        //{
        //    char[] chars = new char[bytes.Length / sizeof(char)];
        //    System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        //    return new string(chars);
        //}


        //public string GetSalt()
        //{
        //    byte[] saltBytes = new byte[_saltSize];
        //    _cryptop.GetNonZeroBytes(saltBytes);

        //    string mySalt = GetString(saltBytes);

        //    return mySalt;
        //}

        public string GetPassword(string message)
        {
            SHA256 shaPass = new SHA256CryptoServiceProvider();
            byte[] dataBytes = GetBytes(message);
            byte[] resaultBytes = shaPass.ComputeHash(dataBytes);

            return Convert.ToBase64String(resaultBytes);
        }

        public string GeneratePassword(string password)
        {
            //salt = GetSalt();
            string finalPass = password;

            return GetPassword(finalPass);
        }

        public bool ValidatePassword(string password,string hash)
        {
            string finalPass = password;
            return hash == GetPassword(finalPass);
        }
    }
}
