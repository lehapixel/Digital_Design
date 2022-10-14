using System;
using System.Collections.Generic;

namespace WordCounter
{
    public class WordCounter
    {
        static private char[] exceptionalSymbols = { ' ', ',', '.', '!', '?', ':', ';', '-', '–', '+', '=', '"', '«', '»', '[', ']', '(', ')', '/', '\\', '|', '~', '@', '$', '%', '^', '&', '*', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        
        static private Dictionary<string, int> Count(ref string[] text)
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
    }
}