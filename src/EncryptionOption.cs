using CommandLine;

namespace PasswordLab;

[Verb("encrypt", HelpText = "Encrypt a file.")]
public class EncryptOptions
{
    [Value(0, Required = true, HelpText ="File path/s of the files to encrypt.")]
    public IEnumerable<string> FilePaths {get; set;} = null!;

    [Option('o', "output", Required = false, HelpText = "Output file path, current directory is default.")]
    public string? OutputPath{get; set;}

    [Option('n', "name", Required = true, HelpText = "File name of .encrypt file.")]
    public string OutputFileName {get; set;} = null!;

    [Option(longName: "password", Required = true, HelpText = "File password encryption")]
    public string Password {get; set;} = null!;
}