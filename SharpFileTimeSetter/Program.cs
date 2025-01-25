using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || args[0].Equals("--help", StringComparison.OrdinalIgnoreCase))
        {
            ShowHelp();
            return;
        }

        try
        {
            string filePath = null;
            bool getFileTime = false;
            DateTime? creationTime = null;
            DateTime? modificationTime = null;
            DateTime? accessTime = null;
            string sourceFilePath = null;
            string targetFilePath = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--file":
                        filePath = args[++i];
                        break;
                    case "--creation":
                        creationTime = DateTime.Parse(args[++i]);
                        break;
                    case "--modification":
                        modificationTime = DateTime.Parse(args[++i]);
                        break;
                    case "--access":
                        accessTime = DateTime.Parse(args[++i]);
                        break;
                    case "--get":
                        getFileTime = true;
                        break;
                    case "--sync":
                        sourceFilePath = args[++i];
                        targetFilePath = args[++i];
                        break;
                    default:
                        Console.WriteLine($"Unknown argument: {args[i]}");
                        ShowHelp();
                        return;
                }
            }

            if (string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(sourceFilePath) && !getFileTime)
            {
                Console.WriteLine("Error: --file or --sync parameter is required.");
                ShowHelp();
                return;
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Error: File '{filePath}' does not exist.");
                    return;
                }

                if (getFileTime)
                {
                    Console.WriteLine($"File: {filePath}");
                    Console.WriteLine($"Creation Time: {File.GetCreationTime(filePath)}");
                    Console.WriteLine($"Modification Time: {File.GetLastWriteTime(filePath)}");
                    Console.WriteLine($"Access Time: {File.GetLastAccessTime(filePath)}");
                    return;
                }

                if (creationTime.HasValue)
                {
                    File.SetCreationTime(filePath, creationTime.Value);
                    Console.WriteLine($"Creation time set to {creationTime.Value}.");
                }

                if (modificationTime.HasValue)
                {
                    File.SetLastWriteTime(filePath, modificationTime.Value);
                    Console.WriteLine($"Modification time set to {modificationTime.Value}.");
                }

                if (accessTime.HasValue)
                {
                    File.SetLastAccessTime(filePath, accessTime.Value);
                    Console.WriteLine($"Access time set to {accessTime.Value}.");
                }

                Console.WriteLine("File time properties updated successfully.");
            }

            if (!string.IsNullOrEmpty(sourceFilePath) && !string.IsNullOrEmpty(targetFilePath))
            {
                if (!File.Exists(sourceFilePath))
                {
                    Console.WriteLine($"Error: Source file '{sourceFilePath}' does not exist.");
                    return;
                }

                if (!File.Exists(targetFilePath))
                {
                    Console.WriteLine($"Error: Target file '{targetFilePath}' does not exist.");
                    return;
                }

                SyncFileTimes(sourceFilePath, targetFilePath);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SyncFileTimes(string sourceFilePath, string targetFilePath)
    {
        try
        {
            // 获取源文件的时间
            DateTime creationTime = File.GetCreationTime(sourceFilePath);
            DateTime modificationTime = File.GetLastWriteTime(sourceFilePath);
            DateTime accessTime = File.GetLastAccessTime(sourceFilePath);

            // 同步目标文件的时间属性
            File.SetCreationTime(targetFilePath, creationTime);
            File.SetLastWriteTime(targetFilePath, modificationTime);
            File.SetLastAccessTime(targetFilePath, accessTime);

            Console.WriteLine($"Synced times from source '{sourceFilePath}' to target '{targetFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while syncing file times: {ex.Message}");
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("Usage: SharpSharpFileTimeSetter --file <path> [--get] [--creation <date>] [--modification <date>] [--access <date>]");
        Console.WriteLine("       SharpFileTimeSetter --sync <sourceFile> <targetFile>");
        Console.WriteLine("Example:");
        Console.WriteLine("  SharpFileTimeSetter --file test.txt --creation \"2025-01-01 12:00:00\" --modification \"2025-01-02 14:00:00\"");
        Console.WriteLine("  SharpFileTimeSetter --file test.txt --get");
        Console.WriteLine("  SharpFileTimeSetter --sync source.txt target.txt");
        Console.WriteLine();
        Console.WriteLine("Arguments:");
        Console.WriteLine("  --file          Path to the target file (required).");
        Console.WriteLine("  --get           Get the current creation, modification, and access time of the file.");
        Console.WriteLine("  --creation      Creation time to set (optional, format: yyyy-MM-dd HH:mm:ss).");
        Console.WriteLine("  --modification  Modification time to set (optional, format: yyyy-MM-dd HH:mm:ss).");
        Console.WriteLine("  --access        Access time to set (optional, format: yyyy-MM-dd HH:mm:ss).");
        Console.WriteLine("  --sync          Synchronize time properties from source file to target file.");
        Console.WriteLine("  <sourceFile>    Source file whose time will be copied.");
        Console.WriteLine("  <targetFile>    Target file whose time will be updated.");
    }
}
