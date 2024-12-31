using System;
using System.Text.Json;
using PasswordLab.Sample;

namespace PasswordLab;

public class IOService
{
    private static readonly string EncryptedFileExtension = "gz";
    public static void CompressAndSaveSecureFiles(List<EncryptedFileData> encryptedFiles, string fileName, string? outputPath)
    {
        var finalFileOutputPath = outputPath != null ? 
            $@"{outputPath}\{fileName}.{EncryptedFileExtension}" : 
            $@"{Directory.GetCurrentDirectory()}\{fileName}.{EncryptedFileExtension}";

        Console.WriteLine($"Compressing and saving file to {finalFileOutputPath}");

        var json = JsonSerializer.Serialize(encryptedFiles);

        GZipService.CompressStringToFile(json, finalFileOutputPath);
    }

    public static List<EncryptedFileData> LoadEncryptedFileData(string path)
    {
        Console.WriteLine($"Loading {path} for encryption.");

        var json = GZipService.DecompressFileToString(path);

        var securefiles = JsonSerializer.Deserialize<List<EncryptedFileData>>(json) ?? [];

        Console.WriteLine($"Found {securefiles.Count} files to encrypt. Press any key to continue...");
        Console.ReadKey();
        
        return securefiles;
    }
}
