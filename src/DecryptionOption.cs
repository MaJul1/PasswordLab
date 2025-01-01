using CommandLine;

namespace PasswordLab;

[Verb("decrypt", HelpText = "Decrypt a encrypted file.")]
public class DecryptOptions
{
    [Option('f', "file", Required = true, HelpText ="File path of the file to decrypt.")]
    public string FilePath {get; set;} = null!;

    [Option('o', "output", Required = false, HelpText = "Output file path, use current directory if not set.")]
    public string? OutputPath{get; set;}

    [Option(longName: "password", Required = true, HelpText = "File password for decryption.")]
    public string Password {get; set;} = null!;
}
