using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace WordCounterClient
{
    public class Program
    {
        static void Main()
        {
            string pathToFile = "..\\..\\..\\..\\Resources\\WarAndPeaceVolume1.txt";
            string[] text = File.ReadAllLines(pathToFile);
            var message = new ServerCommunication();
            message.SendingText(text);
        }
    }
    public class ServerCommunication
    {
        public void SendingText(string[] text)
        {
            var type = typeof(WordCounterService.WordCounterServiceClient).Assembly.GetTypes().First(x => x.Name == "WordCounterServiceClient");
            var client = new WordCounterService.WordCounterServiceClient("BasicHttpBinding_IWordCounterService");
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            var method = type.GetMethod("Count", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            dictionary = method.Invoke(client, new object[] { text }) as Dictionary<string, int>;

            using (StreamWriter file = new StreamWriter("WordСount.txt"))
            {
                foreach (var row in dictionary.OrderByDescending(_ => _.Value))
                {
                    file.WriteLine($"{row.Key} {row.Value}");
                }
            }
            client.Close();
            Console.WriteLine("File \"WordСount.txt\" has been created!");
            Console.ReadKey();   
        }
    }
}