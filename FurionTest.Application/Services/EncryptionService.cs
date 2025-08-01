using Furion.JsonSerialization;

namespace FurionTest.Application.Services
{
    public class EncryptionService : IDynamicApiController
    {
        public string Md5Encrypt(string input, bool uppercase = false)
        {
            // 测试 MD5 加密，比较
            var md5Hash = MD5Encryption.Encrypt(input, uppercase);  // 加密
            var isEqual = MD5Encryption.Compare(input, md5Hash); // 比较
            return JSON.Serialize(new { md5Hash, isEqual });
        }

        public string DESCDecrypt(string input, string skey = "Furion")
        {
            // 测试 DESC 加解密
            var descHash = DESCEncryption.Encrypt(input, skey); // 加密
            var str = DESCEncryption.Decrypt(descHash, skey);  // 解密
            return JSON.Serialize(new { descHash, str });
        }

        public string AesEncrypt(string input)
        {
            // 测试 AES 加解密
            var key = Guid.NewGuid().ToString("N"); // 密钥，长度必须为24位或32位

            var aesHash = AESEncryption.Encrypt(input, key); // 加密
            var str2 = AESEncryption.Decrypt(aesHash, key); // 解密
            return JSON.Serialize(new { aesHash, str2 });
        }

        public string RSAEncrypt(string input)
        {
            // 测试 RSA 加密
            var (publicKey, privateKey) = RSAEncryption.GenerateSecretKey(2048);  //生成 RSA 秘钥 秘钥大小必须为 2048 到 16384，并且是 8 的倍数
            var basestring = RSAEncryption.Encrypt(input, publicKey);  // 加密
            var str2 = RSAEncryption.Decrypt(basestring, privateKey); // 解密
            return JSON.Serialize(new { basestring, str2 });
        }
    }
}