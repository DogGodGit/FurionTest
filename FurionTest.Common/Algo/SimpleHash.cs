namespace FurionTest.Common.Algo;

public class SimpleHash
{
    /* 加法哈希 */
    int AddHash(string key)
    {
        long hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash = (hash + c) % MODULUS;
        }
        return (int)hash;
    }

    /* 乘法哈希 */
    int MulHash(string key)
    {
        long hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash = (31 * hash + c) % MODULUS;
        }
        return (int)hash;
    }

    /* 异或哈希 */
    int XorHash(string key)
    {
        int hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash ^= c;
        }
        return hash & MODULUS;
    }

    /* 旋转哈希 */
    int RotHash(string key)
    {
        long hash = 0;
        const int MODULUS = 1000000007;
        foreach (char c in key)
        {
            hash = ((hash << 4) ^ (hash >> 28) ^ c) % MODULUS;
        }
        return (int)hash;
    }
}
