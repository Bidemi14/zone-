using System.Text.RegularExpressions;

if (args.Length != 2 )
{
    Console.WriteLine("Usage: bcc -c|-l|-w|-m <filename>");
    return;
}
    
string option = args[0];
string filename = args[1];

try
{
    if(option == "-c") 
    {
        byte[] fileBytes;
        fileBytes = File.ReadAllBytes(filename);
        Console.WriteLine(fileBytes.Length);
                    
    }
    else if (option == "-l")
    {
        string[] lines = File.ReadAllLines(filename);
        Console.WriteLine(lines.Length);
    }
                
    else if (option == "-w")//step 3
    {
        string text = File.ReadAllText(filename);
        text=Regex.Replace(text, @"\s+", " ");
        int words = 0;
        foreach (string word in text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            words++; 
        }
        Console.WriteLine(words);
    }
                
    else if (option == "-m")//step 4 
    {
        string text = File.ReadAllText(filename);
        Console.WriteLine(text.Length);
    }
                
}
catch (FileNotFoundException)
{
    Console.WriteLine($"Error: File '{filename}' not found.");
}