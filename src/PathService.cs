using System;

namespace PasswordLab.src;

public class PathService
{
    public static FilePathDetails[] GetFilePathDetails(string path)
    {
        var isDirectory = Directory.Exists(path);

        var isFile = File.Exists(path);

        if (isDirectory)
        {
            return CollectFileDetailsFromDirectory(path);
        }
        else if (isFile)
        {
            return [CreateFilePathDetails(path, GetLastDirectoryIndex(path))];
        }

        throw new ArgumentException("{path} cannot be found", path);
    }

    private static FilePathDetails[] CollectFileDetailsFromDirectory(string path)
    {
        var filePathDetails = new List<FilePathDetails>();

        var fullPath = Path.GetFullPath(path);

        var filePaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        var commonAncestorDirectoryDepth = GetLastDirectoryIndex(fullPath);

        foreach (var filePath in filePaths)
        {
            filePathDetails.Add(CreateFilePathDetails(filePath, commonAncestorDirectoryDepth));
        }

        return [.. filePathDetails];
    }

    private static int GetLastDirectoryIndex(string path)
    {
        var array = path.Split(Path.DirectorySeparatorChar);

        return array.Length - 1;
    }

    private static FilePathDetails CreateFilePathDetails(string filePath, int rootDirectoryDepth)
    {
        var fullFilePath = Path.GetFullPath(filePath);

        return new FilePathDetails()
        {
            FullPath = fullFilePath,
            CommonAncestorDepth = rootDirectoryDepth
        };
    }

}
