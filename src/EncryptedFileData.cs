namespace PasswordLab;

public class EncryptedFileData
{
    public string FilePath {get; set;} = null!;
    public byte[] IV {get; set;} = [];
    public byte[] EncryptedFile {get; set;} = [];
}
