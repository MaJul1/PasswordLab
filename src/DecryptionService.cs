using System;
using System.Text.Json;
using PasswordLab.Sample;

namespace PasswordLab;

public class DecryptionService
{
    public static void Decrypt(DecryptOptions options)
    {
        Console.WriteLine("Starting Decryption.");

        Console.WriteLine($"Decompressing {options.FilePath}");
        var json = GZipService.DecompressFileToString(options.FilePath);

        Console.WriteLine("Deserializing Json");
        var securefiles = JsonSerializer.Deserialize<List<SecureFileData>>(json);
        
        Console.WriteLine("Hashing the Password");
        var hashedPassword = HashingService.ConvertStringTo256Bits(options.Password);

        var output = options.OutputPath ?? Directory.GetCurrentDirectory();

        foreach (var securefile in securefiles!)
        {
            Console.WriteLine($"Decrypting {securefile.FilePath}");
            var outputDirectory = $@"{output}\{securefile.FilePath}";

            while (File.Exists(outputDirectory))
            {
                var split = outputDirectory.Split(".");
                var name = split[0] + "Copy";
                var extension = split[1];
                outputDirectory = string.Concat(name ,"." , extension);
            }

            var directories = outputDirectory.Split(Path.DirectorySeparatorChar);

            string directoryPath = "";
            for (int i = 0; i < directories.Length - 1; i++)
            {
                directoryPath += directories[i] + Path.AltDirectorySeparatorChar;
            }

            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileBytes = AesService.DecryptBytes_Aes(securefile.EncryptedFile, hashedPassword, securefile.IV);

            File.WriteAllBytes(outputDirectory, fileBytes);
        }

        Console.WriteLine("Decryption finished.");
    }
}
