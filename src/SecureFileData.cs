using System;

namespace PasswordLab;

public class SecureFileData
{
    public string FilePath {get; set;} = null!;
    public byte[] IV {get; set;} = [];
    public byte[] EncryptedFile {get; set;} = [];
}
