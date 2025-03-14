using System;
using System.IO;
using System.Linq;

public static class PathHelper
{
    private static string _projectRoot;

    // Lazy initialization af projektroden
    private static string ProjectRoot
    {
        get
        {
            if (_projectRoot == null)
            {
                _projectRoot = FindProjectRoot(AppDomain.CurrentDomain.BaseDirectory);
            }
            return _projectRoot;
        }
    }

    // Finder projektroden ved at lede efter en .csproj-fil
    private static string FindProjectRoot(string startDirectory)
    {
        string currentDir = startDirectory;
        while (currentDir != null)
        {
            if (Directory.EnumerateFiles(currentDir, "*.csproj").Any())
            {
                return currentDir;
            }
            currentDir = Directory.GetParent(currentDir)?.FullName;
        }
        throw new DirectoryNotFoundException("Kunne ikke finde projektroden med en .csproj-fil.");
    }

    // Den offentlige metode, du kan kalde fra alle klasser
    public static string LocalDirectory(string relativePath)
    {
        // Fjern ledende '/' eller '\', hvis de er der
        relativePath = relativePath.TrimStart('/', '\\');
        // Kombiner projektroden med den relative sti
        return Path.Combine(ProjectRoot, relativePath);
    }
}