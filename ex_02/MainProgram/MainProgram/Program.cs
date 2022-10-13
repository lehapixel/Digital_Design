using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MainProgram
{
    class Program
    {
        static void Main()
        {         
            string pathToFile = "..\\..\\..\\..\\..\\Resources\\WarAndPeaceVolume1.txt";
            string[] text = File.ReadAllLines(pathToFile);

            var type = typeof(WordCounter.WordCounter).Assembly.GetTypes().First(x => x.Name == "WordCounter");
            var method = type.GetMethod("Count", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var dictionary = method.Invoke(null, new object[] { text }) as Dictionary <string, int>;
            using (StreamWriter file = new("wordCounter.txt"))
            {
                foreach (var row in dictionary.OrderByDescending(_ => _.Value))
                {
                    file.WriteLine($"{row.Key} {row.Value}");
                }
            }
            Console.WriteLine("File has been created!");
            Console.ReadKey();
        }
    }
}