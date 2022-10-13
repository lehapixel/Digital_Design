using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace WordCounter
{
    public class WordCounter
    {
        static private Dictionary<string, int> Count(string[] text)
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>(); ;
            foreach (var line in text)
            {
                var words = line.ToLower().Split(new char[] { ' ', ',', '.', '!', '?', ':', ';', '-', '–', '+', '=', '"', '«', '»', '[', ']', '(', ')', '/', '\\', '|', '~', '@', '$', '%', '^', '&', '*', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (!keyValuePairs.ContainsKey(word))
                    {
                        keyValuePairs.Add(word, 1);
                    }
                    else
                    {
                        keyValuePairs[word]++;
                    }
                }
            }
            return keyValuePairs;
        }

        static public ConcurrentDictionary<string, int> MultithreadedCount(string[] text)
        {
            ConcurrentDictionary<string, int> keyValuePairs = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(text, line =>
            {
                var words = line.ToLower().Split(new char[] { ' ', ',', '.', '!', '?', ':', ';', '-', '–', '+', '=', '"', '«', '»', '[', ']', '(', ')', '/', '\\', '|', '~', '@', '$', '%', '^', '&', '*', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }, StringSplitOptions.RemoveEmptyEntries);

                Parallel.ForEach(words, word =>
                {
                    keyValuePairs.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                });
            });
            return keyValuePairs;
        }
    }
}