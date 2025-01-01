namespace PasswordLab;

public class EncryptionService
{
    public static void Encrypt(EncryptOptions options)
    {
        Console.WriteLine("Starting Encryption");

        var files = GetFilePathDetails(options.FilePaths);

        var encryptedFiles = EncryptFiles(files, options.Password);

        IOService.CompressAndSaveSecureFiles(encryptedFiles, options.OutputFileName, options.OutputPath);

        Console.WriteLine("Finished Encryption");
    }

    private static List<FilePathDetails> GetFilePathDetails(IEnumerable<string> paths)
    {
        Console.WriteLine("Scanning Files");

        var filePathDetails = new List<FilePathDetails>();

        foreach(var path in paths)
        {
            filePathDetails.AddRange(PathService.GetFilePathDetails(path));
        }

        Console.WriteLine($"Found {filePathDetails.Count} files to encrypt. Press any key to continue...");

        Console.ReadKey();

        return filePathDetails;
    }
    
    private static List<EncryptedFileData> EncryptFiles(List<FilePathDetails> files, string password)
    {
        Console.WriteLine("Encrypting files.");

        var encryptedFiles = new List<EncryptedFileData>();

        foreach(var file in files)
        {
            encryptedFiles.Add(EncryptFileByPath(file, password));
        }

        Console.WriteLine("Encryption finished.");

        return encryptedFiles;
    }

    private static EncryptedFileData EncryptFileByPath(FilePathDetails pathInfo, string password)
    {
        Console.WriteLine($"Encrypting {pathInfo.FullPath}");

        var fileBytes = File.ReadAllBytes(pathInfo.FullPath);

        var Iv = AesService.GenerageIV();

        var hashedPassword = HashingService.ConvertStringTo256Bits(password);

        var encryptedBytes = AesService.EncryptBytes_Aes(fileBytes, hashedPassword, Iv);

        EncryptedFileData encryptedFile = new()
        {
            FilePath = pathInfo.ExtractRelativePath(),
            IV = Iv,
            EncryptedFile = encryptedBytes
        };

        return encryptedFile;
    }
}
