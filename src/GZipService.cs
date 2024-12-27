using System;
using System.IO.Compression;
using System.Text;

namespace PasswordLab.Sample;

public class GZipService
{
    public static void CompressStringToFile(string str, string outputFilePath)
    {
        byte[] stringBytes = Encoding.UTF8.GetBytes(str);

        using (FileStream fileStream = new FileStream(outputFilePath, FileMode.Create))
        {
            using (GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress))
            {
                compressionStream.Write(stringBytes, 0, stringBytes.Length);
            }
        }
    }

    public static string DecompressFileToString(string inputFilePath)
    {
        using (FileStream fileStream = new FileStream(inputFilePath, FileMode.Open))
        {
            using (GZipStream decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    decompressionStream.CopyTo(memoryStream);
                    byte[] decompressedBytes = memoryStream.ToArray();
                    return Encoding.UTF8.GetString(decompressedBytes);
                }
            }
        }
    }
}
