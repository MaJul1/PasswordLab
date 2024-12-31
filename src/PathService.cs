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

    public static string ChangeNameIfFilePathExists(string path)
    {
        var newPathName = path;

        while (File.Exists(path))
        {
            var split = path.Split(".");
            var name = split[0] + "Copy";
            var extension = split[1];
            newPathName = string.Concat(name, ".", extension);
        }

        return newPathName;
    }

    public static void CreateDirectoriesIfNotExits(string path)
    {
        path = RemoveLastPathComponent(path);

        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
    }

    private static string RemoveLastPathComponent(string path)
    {
        var split = path.Split(Path.DirectorySeparatorChar);

        List<string> newSplitPath = [];

        for (int i = 0; i < split.Length - 1; i++)
        {
            newSplitPath.Add(split[i]);
        }

        var newPath = string.Join(Path.DirectorySeparatorChar, newSplitPath);

        return newPath;
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

    private static bool IsPathAFile(string path)
    {
        var split = path.Split(Path.DirectorySeparatorChar);

        if (split[^1].Contains('.'))
        {
            return true;
        }

        return false;
    }

}
