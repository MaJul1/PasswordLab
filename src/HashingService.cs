using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordLab;

public class HashingService
{
    public static byte[] ConvertStringTo256Bits(string input)
    {
        Console.WriteLine("Hashing the Password");
        
        return SHA256.HashData(Encoding.UTF8.GetBytes(input));
    }
}
