using System;
using System.Collections.Generic;

namespace RuleBasedAttack
{
    public class RuleBased
    {
        private const double guessesPerSecond = 1000000; 
        public double RuleBasedAttackTime(string password, IEnumerable<string> dictionary)
        {
            List<string> commonPasswords = new List<string>(dictionary);
 
            long attempts = 0;

            foreach (string baseWord in commonPasswords)
            {
                List<string> variants = GenerateRuleVariants(baseWord);

                foreach (string candidate in variants)
                {
                    attempts++;

                    if (candidate == password)
                    {
                        return attempts / guessesPerSecond;
                    }
                }
            }

            return -1;
            //Changed it to -1, so it's obvious that it didn't manage to break the password
        }
        
        private static List<string> GenerateRuleVariants(string word)
        {
            List<string> variants = new List<string>();

            string lower = word.ToLower();
            string upper = word.ToUpper();
            string capitalized = Capitalize(word);

            variants.Add(word);
            variants.Add(lower);
            variants.Add(upper);
            variants.Add(capitalized);

            variants.Add(word + "1");
            variants.Add(word + "12");
            variants.Add(word + "123");
            variants.Add(word + "2024");
            variants.Add(word + "2025");

            variants.Add(word + "!");
            variants.Add(word + "@");
            variants.Add(word + "#");

            variants.Add(capitalized + "123");
            variants.Add(capitalized + "!");
            variants.Add(capitalized + "2024");

            string replaced = ReplaceLetters(word);
            variants.Add(replaced);
            variants.Add(replaced + "123");
            variants.Add(replaced + "!");
            variants.Add(replaced + "2024");

            return variants;
        }

        private static string Capitalize(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            if (word.Length == 1)
                return word.ToUpper();

            return char.ToUpper(word[0]) + word.Substring(1).ToLower();
        }

        private static string ReplaceLetters(string word)
        {
            return word.Replace("a", "@")
                    .Replace("A", "@")
                    .Replace("s", "$")
                    .Replace("S", "$")
                    .Replace("o", "0")
                    .Replace("O", "0")
                    .Replace("i", "1")
                    .Replace("I", "1")
                    .Replace("e", "3")
                    .Replace("E", "3");
        }
    }
}