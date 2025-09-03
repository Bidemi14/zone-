using System;
using System.IO;
using System.Text.RegularExpressions;

namespace wc;

public class GrepFunction
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run -- <pattern> <file-or-dir> [-r] [-v]");
            return 1;
        }

        string pattern = args[0];
        string filepath = args.Length > 1 && !args[1].StartsWith("-") ? args[1] : null;

        bool recursive = args.Contains("-r");
        bool invert = args.Contains("-v");
        bool optionCase = args.Contains("-i");

        if (string.IsNullOrEmpty(filepath))
        {
            filepath = "/Users/bidemielelu/Downloads/challenge-grep";
        }

        if (File.Exists(filepath))
        {
            foreach (string line in File.ReadLines(filepath))
            {
                PrintMatch(line, pattern, filepath, invert,optionCase);
            }
        }
        else if (Directory.Exists(filepath))
        {
       
            var option = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (string file in Directory.GetFiles(filepath, "*.txt", option))
            {
                foreach (string line in File.ReadLines(file))
                {
                    PrintMatch(line, pattern, file, invert, optionCase);
                }
            }
        }
        else
        {
            Console.WriteLine($"Error: {filepath} is not a valid file or directory.");
            return 1;
        }

        return 0;
    }

    static void PrintMatch(string line, string pattern, string file, bool invert,bool optionCase)
    {
        var options=RegexOptions.Multiline;
        if (optionCase)
            options |= RegexOptions.IgnoreCase;
        bool match = Regex.IsMatch(line, pattern, RegexOptions.Multiline);
        if ((match && !invert) || (!match && invert))
        {
            Console.WriteLine(line);
        }
    }

}