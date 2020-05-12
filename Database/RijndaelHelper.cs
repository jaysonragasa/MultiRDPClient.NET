using System;
using System.Text;
using RijndaelEncryptDecrypt;

    public class RijndaelSettings
    {
        public static string PassPhrase = "4e7046087d6e8eefe1b41bddf8bde56c";
        public static string SaltValue = "36ec281f88fb483b5726ccfc81bae6d9";
        public static string HashAlgorithm = "SHA1";
        public static int PasswordIterations = 2;
        public static string InitVector = "@1B2c3D4e5F6g7H8";
        public static int KeySize = 256;

        public static string Encrypt(string PlainText)
        {
            return Rijndael.Encrypt(PlainText, PassPhrase, SaltValue, HashAlgorithm, PasswordIterations, InitVector, KeySize);
        }

        public static string Decrypt(string cipherText)
        {
            return Rijndael.Decrypt(cipherText, PassPhrase, SaltValue, HashAlgorithm, PasswordIterations, InitVector, KeySize);
        }
    }
