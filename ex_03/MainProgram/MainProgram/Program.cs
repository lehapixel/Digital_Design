using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace MainProgram
{
    class Program
    {
        static void Main()
        {
            string pathToFile = "..\\..\\..\\..\\..\\Resources\\WarAndPeaceVolume1.txt";
            string[] text = File.ReadAllLines(pathToFile);

            Stopwatch stopWatchCount = new Stopwatch();

            var type = typeof(WordCounter.WordCounter).Assembly.GetTypes().First(x => x.Name == "WordCounter");

            //Single Threaded Count
            var method = type.GetMethod("Count", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            stopWatchCount.Start();
            dictionary = method.Invoke(null, new object[] { text }) as Dictionary<string, int>;
            stopWatchCount.Stop();
            using (StreamWriter file = new("WordCount.txt"))
            {
                foreach (var row in dictionary.OrderByDescending(_ => _.Value))
                {
                    file.WriteLine($"{row.Key} {row.Value}");
                }
            }
            long timeSpan = stopWatchCount.ElapsedMilliseconds;
            Console.WriteLine("Count Method Run Time: " + timeSpan + " ms");
            Console.WriteLine("File \"WordCount.txt\" has been created! \n");

            //Multithreaded Count
            method = type.GetMethod("MultithreadedCount", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            ConcurrentDictionary<string, int> concurrentDictionary = new ConcurrentDictionary<string, int>(); 
            stopWatchCount.Restart();
            concurrentDictionary = method.Invoke(null, new object[] { text }) as ConcurrentDictionary<string, int>;
            stopWatchCount.Stop();
            using (StreamWriter file = new("MultithreadedWordCount.txt"))
            {
                foreach (var row in concurrentDictionary.OrderByDescending(_ => _.Value))
                {
                    file.WriteLine($"{row.Key} {row.Value}");
                }
            }
            timeSpan = stopWatchCount.ElapsedMilliseconds;
            Console.WriteLine("MultithreadedCount Method Run Time: " + timeSpan + " ms");
            Console.WriteLine("File \"MultithreadedWordCount.txt\" has been created!");

            Console.ReadKey();
        }
    }
}