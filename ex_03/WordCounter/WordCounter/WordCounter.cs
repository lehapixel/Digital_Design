using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace WordCounter
{
    public class WordCounter
    {
        static private char[] exceptionalSymbols = { ' ', ',', '.', '!', '?', ':', ';', '-', '–', '+', '=', '"', '«', '»', '[', ']', '(', ')', '/', '\\', '|', '~', '@', '$', '%', '^', '&', '*', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        static private Dictionary<string, int> Count(string[] text)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (var line in text)
            {
                var words = line.ToLower().Split(exceptionalSymbols, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (!dictionary.ContainsKey(word))
                    {
                        dictionary.Add(word, 1);
                    }
                    else
                    {
                        dictionary[word]++;
                    }
                }
            }
            return dictionary;
        }

        static public ConcurrentDictionary<string, int> MultithreadedCount(string[] text)
        {
            ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(text, line =>
            {
                var words = line.ToLower().Split(exceptionalSymbols, StringSplitOptions.RemoveEmptyEntries);

                Parallel.ForEach(words, word =>
                {
                    dictionary.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                });
            });
            return dictionary;
        }
    }
}