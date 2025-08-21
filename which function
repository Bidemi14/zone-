namespace wc;

public class Which
{
    public static List<string> Findpath(string name)
    {
        string path = Environment.GetEnvironmentVariable("PATH");
        List<string> results = new List<string>();
        string[] dirs = path.Split(Path.PathSeparator);

        string[] extensions = { "", ".exe", ".bat", ".cmd", ".ps1" };

        foreach (string dir in dirs)
        {
            foreach (string ext in extensions)
            {
                string fpath = Path.Combine(dir, $"{name}{ext}");
                if (File.Exists(fpath))
                {
                    results.Add(fpath);
                }
            }
        }

        return results;
    }

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: which.exe [-a] [-s] <program>");
            Environment.Exit(1);
        }

        bool list = args.Contains("-a");
        string program = args.Last();

        var founfPath = Findpath(program);
        bool found = founfPath.Count > 0;

        if (list)
        {
            if (found)
            {
                foreach (string p in founfPath)
                    Console.WriteLine("Found path: " + p);
            }
            else
            {
                Console.WriteLine("Could not find path: " + program);
            }
        }
        else
        {
            if (found)
            {
                Console.WriteLine("Found path: " + founfPath[0]);
            }
            else
            {
                Console.WriteLine("Could not find path: " + program);
            }
        }
    }
}
