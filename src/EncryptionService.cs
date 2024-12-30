using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using PasswordLab.Sample;
using PasswordLab.src;

namespace PasswordLab;

public class EncryptionService
{
    public static void Encrypt(EncryptOptions options)
    {
        var files = GetFilePathDetails(options.FilePaths);

        var encryptedFiles = EncryptFiles(files, options.Password);

        FileOutputService.CompressAndSaveSecureFiles(encryptedFiles, options.OutputFileName, options.OutputPath);
    }

    private static List<FilePathDetails> GetFilePathDetails(IEnumerable<string> paths)
    {
        var filePathDetails = new List<FilePathDetails>();

        foreach(var path in paths)
        {
            filePathDetails.AddRange(PathService.GetFilePathDetails(path));
        }

        return filePathDetails;
    }
    
    private static List<SecureFileData> EncryptFiles(List<FilePathDetails> files, string password)
    {
        var encryptedFiles = new List<SecureFileData>();

        foreach(var file in files)
        {
            encryptedFiles.Add(EncryptFileByPath(file, password));
        }

        return encryptedFiles;
    }

    private static SecureFileData EncryptFileByPath(FilePathDetails pathInfo, string password)
    {
        var fileBytes = File.ReadAllBytes(pathInfo.FullPath);

        var Iv = AesService.GenerageIV();

        var hashedPassword = HashingService.ConvertStringTo256Bits(password);

        var encryptedBytes = AesService.EncryptBytes_Aes(fileBytes, hashedPassword, Iv);

        SecureFileData encryptedFile = new()
        {
            FilePath = pathInfo.ExtractRelativePath(),
            IV = Iv,
            EncryptedFile = encryptedBytes
        };

        return encryptedFile;
    }


}
