using System;
using System.Collections.Generic;

namespace WordCounter
{
    public class WordCounter
    {
        static private Dictionary<string, int> Count(ref string[] text)
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
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
    }
}