using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MyNamespace
{
    class Wc
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 )
            {
                Console.WriteLine("Usage: ccwc -c|-l|-w|-m <filename>");
                return;
            }
    
            string option = args[0];
            string filename = args[1];

            try
            {
                if(option == "-c")//step 1 
                {
                    byte[] fileBytes;
                    fileBytes = File.ReadAllBytes(filename);
                    Console.WriteLine(fileBytes.Length);
                    
                }
                else if (option == "-l")//step 2
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
        }
    }
}


































/*using System;
using System.IO;

namespace MyNamespace
{
    class Program
    {
        static void Wc()
        {
            string readText = File.ReadAllText("/Users/bidemielelu/Downloads/test.txt"); // Read filee
            int words = 0;
            int currentchar=0;
            foreach (char x in readText)
            {
                if (char.IsLetter(x) ||  char.IsDigit(x))
                {
                    currentchar = currentchar + 1;
                    //Console.WriteLine(x);
                }

                else
                {
                    //currentchar = currentchar + 1;
                    continue;
                    words++;
                    //continue;


                }


            }
            Console.WriteLine(words);
            }

       static void Main(string[] args)
        {
            Wc();
        }
    }
}*/
