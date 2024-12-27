using System.Security.Cryptography;

public static class AesService
{
    public static byte[] EncryptBytes_Aes(byte[] plainBytes, byte[] Key, byte[] IV)
    {
        if (plainBytes == null || plainBytes.Length <= 0)
            return [];
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException(nameof(IV));

        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    csEncrypt.FlushFinalBlock();
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        return encrypted;
    }

    public static byte[] DecryptBytes_Aes(byte[] cipherBytes, byte[] Key, byte[] IV)
    {
        if (cipherBytes == null || cipherBytes.Length <= 0)
            return [];
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException(nameof(Key));
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException(nameof(IV));

        byte[] decrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (MemoryStream msPlain = new MemoryStream())
                    {
                        csDecrypt.CopyTo(msPlain);
                        decrypted = msPlain.ToArray();
                    }
                }
            }
        }

        return decrypted;
    }

    public static byte[] GenerageIV()
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.GenerateIV();
        return aesAlg.IV;
    }
}