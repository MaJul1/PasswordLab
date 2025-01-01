namespace PasswordLab;

public class FilePathDetails
{
    public string FullPath {get; set;} = null!;
    public int CommonAncestorDepth {get; set;}

    public string ExtractRelativePath()
    {
        var array = FullPath.Split(Path.DirectorySeparatorChar);

        var trimed = array.Skip(CommonAncestorDepth);

        return string.Join(Path.DirectorySeparatorChar, trimed);
    }
}
