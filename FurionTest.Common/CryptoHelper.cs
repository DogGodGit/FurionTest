using System.Security.Cryptography;

namespace FurionTest.Common;

public class CryptoHelper
{
    private readonly ICryptoTransform encryptor;

    private readonly ICryptoTransform decryptor;

    private const int BufferSize = 1024;

    public CryptoHelper(string algorithmName, string key)
    {
        SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(algorithmName);
        symmetricAlgorithm.Key = Encoding.UTF8.GetBytes(key);
        symmetricAlgorithm.IV = new byte[8] { 18, 52, 86, 120, 144, 171, 205, 239 };
        encryptor = symmetricAlgorithm.CreateEncryptor();
        decryptor = symmetricAlgorithm.CreateDecryptor();
    }

    public CryptoHelper(string key)
        : this("TripleDES", key)
    {
    }

    public string Encrypt(string clearText)
    {
        try
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(clearText));
            MemoryStream memoryStream2 = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream2, encryptor, CryptoStreamMode.Write);
            int num = 0;
            byte[] buffer = new byte[1024];
            do
            {
                num = memoryStream.Read(buffer, 0, 1024);
                cryptoStream.Write(buffer, 0, num);
            }
            while (num > 0);
            cryptoStream.FlushFinalBlock();
            buffer = memoryStream2.ToArray();
            return Convert.ToBase64String(buffer);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return string.Empty;
        }
    }

    public string Decrypt(string encryptedText)
    {
        try
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(encryptedText));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
            int num = 0;
            byte[] buffer = new byte[1024];
            do
            {
                num = cryptoStream.Read(buffer, 0, 1024);
                memoryStream.Write(buffer, 0, num);
            }
            while (num > 0);
            buffer = memoryStream.GetBuffer();
            return Encoding.UTF8.GetString(buffer, 0, (int)memoryStream.Length);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return string.Empty;
        }
    }

    public static string Encrypt(string clearText, string key)
    {
        return new CryptoHelper(key).Encrypt(clearText);
    }

    public static string Decrypt(string encryptedText, string key)
    {
        return new CryptoHelper(key).Decrypt(encryptedText);
    }
}
