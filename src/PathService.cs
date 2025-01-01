using System;

namespace PasswordLab;

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

    public static string GetUniqueFilePath(string path)
    {
        var newPathName = path;

        while (File.Exists(newPathName))
        {
            newPathName = AppendCopySuffixToFileName(newPathName);
        }

        return newPathName;
    }

    private static string AppendCopySuffixToFileName(string path)
    {
        bool hasExtension = GetLastPathComponent(path).Contains('.');
        if (hasExtension)
        {
            return AppendCopyForFileWithExtension(path);
        }
        else
        {
            return AppendCopyForFileWithoutExtension(path);
        }
    }

    private static string AppendCopyForFileWithoutExtension(string path)
    {
        return path + "Copy";
    }

    private static string AppendCopyForFileWithExtension(string path)
    {
            var lastComponent = GetLastPathComponent(path);

            var split = lastComponent.Split(".");

            var name = split[0] + "Copy";

            var newFileName = string.Join('.', name, split[1]);

            var trimmedPath = RemoveLastPathComponent(path);

            return string.Join(Path.DirectorySeparatorChar, trimmedPath, newFileName);
    }

    private static string GetLastPathComponent(string path)
    {
        var splitPaths = path.Split(Path.DirectorySeparatorChar);

        return splitPaths[^1];
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
