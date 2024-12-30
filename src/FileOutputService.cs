using System;
using System.Text.Json;
using PasswordLab.Sample;

namespace PasswordLab.src;

public class FileOutputService
{
    private static readonly string EncryptedFileExtension = "gz";
    public static void CompressAndSaveSecureFiles(List<SecureFileData> encryptedFiles, string fileName, string? outputPath)
    {
        var fileOutputPath = outputPath != null ? 
            $@"{outputPath}\{fileName}.{EncryptedFileExtension}" : 
            $@"{Directory.GetCurrentDirectory()}\{fileName}.{EncryptedFileExtension}";

        var json = JsonSerializer.Serialize(encryptedFiles);

        GZipService.CompressStringToFile(json, fileOutputPath);
    }
}
