using PasswordLab;
using CommandLine;
using System.Security.Cryptography;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            ParseCommands(args);
        }
        catch (CryptographicException)
        {
            ConsoleWriterService.WriteError("Wrong Password");
        }
        catch (FileNotFoundException e)
        {
            ConsoleWriterService.WriteError($"'{e.FileName}' cannot be found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void ParseCommands(string[] args)
    {
        if (IsArgsInvokedVersionCommand(args))
        {
            Console.WriteLine(GetCurrentVersion());
            return;
        }

        Parser.Default.ParseArguments<EncryptOptions, DecryptOptions>(args)
        .MapResult( (EncryptOptions opts) => RunEncryptAndReturnExitCode(opts),
                    (DecryptOptions opts) => DecryptAndReturnExitCode(opts),
                    errs => 1 );
    }

    private static bool IsArgsInvokedVersionCommand(string[] args)
    {
        return args.Contains("--version") || args.Contains("version");
    }

    private static string GetCurrentVersion()
    {
        var versionAttribute = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        return versionAttribute?.InformationalVersion.Split('+')[0] ?? "1.0.0";
    }

    private static int DecryptAndReturnExitCode(DecryptOptions opts)
    {
        DecryptionService.Decrypt(opts);
        return 0;
    }

    private static int RunEncryptAndReturnExitCode(EncryptOptions opts)
    {
        EncryptionService.Encrypt(opts);
        return 0;
    }
}