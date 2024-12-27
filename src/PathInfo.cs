using System;

namespace PasswordLab;

public class PathInfo
{
    public string FullPath {get; set;} = null!;
    public int DirectoryRootDepth {get; set;}

    public string GetPath()
    {
        var array = FullPath.Split(Path.DirectorySeparatorChar);

        var trimed = array.Skip(DirectoryRootDepth);

        return string.Join(Path.DirectorySeparatorChar, trimed);
    }
}
