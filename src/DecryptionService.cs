namespace PasswordLab;

public class DecryptionService
{
    public static void Decrypt(DecryptOptions options)
    {
        Console.WriteLine("Starting Decryption");

        var encryptedFiles = IOService.LoadEncryptedFileData(options.FilePath);

        var hashedPassword = HashingService.ConvertStringTo256Bits(options.Password);

        var output = options.OutputPath ?? Directory.GetCurrentDirectory();

        DecryptEncryptedFiles(encryptedFiles, hashedPassword, output);

        Console.WriteLine("Decryption Finished");
    }

    private static void DecryptEncryptedFiles(List<EncryptedFileData> encryptedFiles, byte[] hashedPassword, string outputPath)
    {
        Console.WriteLine("Starting decryption.");

        foreach (var securefile in encryptedFiles!)
        {
            DecryptEncryptedFile(securefile, hashedPassword, outputPath);
        }
    }

    private static void DecryptEncryptedFile(EncryptedFileData securefile, byte[] hashedPassword, string outputPath)
    {
        var outputFilePath = $@"{outputPath}\{securefile.FilePath}";
     
        Console.WriteLine($"Decrypting {outputFilePath}");

        outputFilePath = PathService.ChangeNameIfFilePathExists(outputFilePath);

        PathService.CreateDirectoriesIfNotExits(outputFilePath);

        var decryptedFile = AesService.DecryptBytes_Aes(securefile.EncryptedFile, hashedPassword, securefile.IV);

        File.WriteAllBytes(outputFilePath, decryptedFile);
    }
}
