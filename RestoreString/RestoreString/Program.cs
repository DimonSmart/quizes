using System;
using System.Collections.Generic;
using System.Linq;

namespace RestoreString
{
    public static class Program
    {
        static void Main()
        {
            var dictionary = new List<string>()
            {
               "hello", "hell", "this", "is", "michal", "world"
            };

            var words = Solver.Solve("lolhehistislahcim", dictionary);
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }
    }

    public static class Solver
    {
        public static List<string> Solve(string input, List<string> allowedWords)
        {
            var sortedAllowedWords = allowedWords.Select(i => new KeyValuePair<string, string>(string.Concat(i.OrderBy(c => c)), i)).ToList();
            var split = TrySplit(input, sortedAllowedWords);
            return split.Where(i => i.Value.Length == 0)
                .Select(i => i.Key).ToList();
        }

        static private IEnumerable<KeyValuePair<string, string>> TrySplit(string input, IEnumerable<KeyValuePair<string, string>> sortedAllowedWords)
        {
            for (int i = 1; i <= input.Length; i++)
            {
                var currentWord = input.Substring(0, i);
                var sortedCurrentWord = string.Concat(currentWord.OrderBy(c => c));
                var possibleWords = sortedAllowedWords
                    .Where(d => d.Key == sortedCurrentWord)
                    .Select(d => new KeyValuePair<string, string>(d.Value, input.Substring(i)));
                foreach (var possibleWord in possibleWords)
                {
                    yield return possibleWord;
                    var recs = TrySplit(possibleWord.Value, sortedAllowedWords).ToList();
                    foreach (var rec in recs)
                    {
                        yield return new KeyValuePair<string, string>(possibleWord.Key + " " + rec.Key, rec.Value);
                    }
                }
            }
        }
    }
}

