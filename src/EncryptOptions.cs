using System.ComponentModel;
using CommandLine;

namespace PasswordLab;

[Verb("encrypt", HelpText = "Encrypt a file.")]
public class EncryptOptions
{
    [Option('f', "file", Required = true, HelpText ="File path of the file to encrypt.")]
    public IEnumerable<string> FilePaths {get; set;} = null!;

    [Option('o', "output", Required = false, HelpText = "Output file path, use current directory if not set.")]
    public string? OutputPath{get; set;}

    [Option('n', "name", Required = true, HelpText = "File name of .encrypt file.")]
    public string OutputFileName {get; set;} = null!;

    [Option(longName: "password", Required = true, HelpText = "File password encryption")]
    public string Password {get; set;} = null!;
}