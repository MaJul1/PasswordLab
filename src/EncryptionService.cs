using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using PasswordLab.Sample;

namespace PasswordLab;

public class EncryptionService
{
    public static readonly string EncryptedFileExtension = "gz";
    public static void Encrypt(EncryptOptions options)
    {
        Console.WriteLine("Starting Encryption");
        var files = new List<PathInfo>();

        Console.WriteLine("Scanning File Paths");
        foreach(var path in options.FilePaths)
        {
            Console.WriteLine("Scanning " + path);
            files.AddRange(GetAllFilesByPath(path));
        }

        Console.WriteLine($"Found {files.Count} files to encrypt.");
        Console.WriteLine("Encrypting files.");
        var encryptedFiles = new List<SecureFileData>();

        foreach(var file in files)
        {
            Console.WriteLine($"Encrypting {file.FullPath}");
            encryptedFiles.Add(EncryptFileByPath(file, options.Password));
        }
        Console.WriteLine("Encrypting all files done.");

        CompressAndExportEncryptedFiles(encryptedFiles, options);
        Console.WriteLine("Encryption Finished.");
    }

    private static void CompressAndExportEncryptedFiles(List<SecureFileData> encryptedFiles, EncryptOptions options)
    {
        var outputPath = options.OutputPath != null ? 
            $@"{options.OutputPath}\{options.OutputFileName}.{EncryptedFileExtension}" : 
            $@"{Directory.GetCurrentDirectory()}\{options.OutputFileName}.{EncryptedFileExtension}";
        
        Console.WriteLine($"Exporting file to {outputPath}");

        var json = JsonSerializer.Serialize(encryptedFiles);
        GZipService.CompressStringToFile(json, outputPath);

    }

    private static SecureFileData EncryptFileByPath(PathInfo pathInfo, string password)
    {
        var fileBytes = File.ReadAllBytes(pathInfo.FullPath);

        var Iv = AesService.GenerageIV();

        var hashedPassword = HashingService.ConvertStringTo256Bits(password);

        var encryptedBytes = AesService.EncryptBytes_Aes(fileBytes, hashedPassword, Iv);

        SecureFileData encryptedFile = new()
        {
            FilePath = pathInfo.GetPath(),
            IV = Iv,
            EncryptedFile = encryptedBytes
        };

        return encryptedFile;
    }

    private static PathInfo[] GetAllFilesByPath(string path)
    {
        var isDirectory = Directory.Exists(path);

        var isFile = File.Exists(path);

        if(isDirectory)
        {
            return GetAllFilesInDirectory(path);
        }
        else if (isFile)
        {
            return [GetFilePathInfoByPath(path, GetIndexOfLastDirectory(path))];
        }

        throw new ArgumentException("{path} cannot be found", path);
    }

    private static int GetIndexOfLastDirectory(string path)
    {
        var array = path.Split(Path.DirectorySeparatorChar);

        return array.Length - 1;
    }

    private static PathInfo[] GetAllFilesInDirectory(string path)
    {
        var filesPathInfos = new List<PathInfo>();
        
        var fullFilePath = Path.GetFullPath(path);

        var filePaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        var rootDirectoryDepth = GetIndexOfLastDirectory(fullFilePath);

        foreach(var filePath in filePaths)
        {
            filesPathInfos.Add(GetFilePathInfoByPath(filePath, rootDirectoryDepth));
        }

        return [.. filesPathInfos];
    }

    private static PathInfo GetFilePathInfoByPath(string filePath, int rootDirectoryDepth)
    {
        var fullFilePath = Path.GetFullPath(filePath);

        return new PathInfo()
        {
            FullPath = fullFilePath,
            DirectoryRootDepth = rootDirectoryDepth
        };
    }
}
