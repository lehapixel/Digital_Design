using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace WordCounterService
{
    public class WordCounterService : IWordCounterService
    {
        static private char[] exceptionalSymbols = { ' ', ',', '.', '!', '?', ':', ';', '-', '–', '+', '=', '"', '«', '»', '[', ']', '(', ')', '/', '\\', '|', '~', '@', '$', '%', '^', '&', '*', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        public ConcurrentDictionary<string, int> Count(string[] text)
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