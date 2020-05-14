using RijndaelEncryptDecrypt;

public class RijndaelSettings
{
    static string PassPhrase = "4e7046087d6e8eefe1b41bddf8bde56c";
    static string SaltValue = "36ec281f88fb483b5726ccfc81bae6d9";
    static string HashAlgorithm = "SHA1";
    static int PasswordIterations = 2;
    static string InitVector = "@1B2c3D4e5F6g7H8";
    static int KeySize = 256;

    public static string Encrypt(string PlainText)
    {
        return Rijndael.Encrypt(PlainText, PassPhrase, SaltValue, HashAlgorithm, PasswordIterations, InitVector, KeySize);
    }

    public static string Decrypt(string cipherText)
    {
        return Rijndael.Decrypt(cipherText, PassPhrase, SaltValue, HashAlgorithm, PasswordIterations, InitVector, KeySize);
    }
}