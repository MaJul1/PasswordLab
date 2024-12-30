using PasswordLab;
using CommandLine;
using System.Security.Cryptography;

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
        catch (ArgumentException e)
        {
            ConsoleWriterService.WriteError(e.Message);
        }
        catch (InvalidDataException e)
        {
            ConsoleWriterService.WriteError(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            ConsoleWriterService.WriteError(e.Message);
        }
    }

    private static void ParseCommands(string[] args)
    {
        Parser.Default.ParseArguments<EncryptOptions, DecryptOptions>(args)
        .MapResult( (EncryptOptions opts) => RunEncryptAndReturnExitCode(opts),
                    (DecryptOptions opts) => DecryptAndReturnExitCode(opts),
                    errs => 1 );
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