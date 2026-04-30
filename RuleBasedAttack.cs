using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RuleBasedAttack
{
    

public class RuleBased
{
    private const double guessesPerSecond = 500000;
    private const string dictionaryPath = "Data/100k-most-used-passwords-NCSC.txt";

    public static double RuleBasedAttackTime(string password)
    {
        List<string> commonPasswords = LoadCommonPasswords(dictionaryPath);

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

        return attempts / guessesPerSecond;
    }

    public static List<string> AnalyzePasswordProblems(string password)
    {
        List<string> problems = new List<string>();

        if (string.IsNullOrEmpty(password))
        {
            problems.Add("Password is empty.");
            return problems;
        }

        if (password.Length < 8)
            problems.Add("Too short (minimum 8 characters recommended).");

        if (!password.Any(char.IsUpper))
            problems.Add("No uppercase letter.");

        if (!password.Any(char.IsLower))
            problems.Add("No lowercase letter.");

        if (!password.Any(char.IsDigit))
            problems.Add("No digit.");

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            problems.Add("No special character.");

        string lower = password.ToLower();

        if (lower.Contains("123") || lower.Contains("1234") || lower.Contains("0000") || lower.Contains("1111"))
            problems.Add("Contains common pattern: numbers sequence (example: 123, 1234, 0000).");

        if (lower.Contains("qwerty") || lower.Contains("password") || lower.Contains("admin") || lower.Contains("user"))
            problems.Add("Contains common word pattern (example: password, admin, qwerty).");

        if (HasRepeatedCharacters(password))
            problems.Add("Contains repeated characters (example: aaaa, 1111).");

        return problems;
    }

    private static bool HasRepeatedCharacters(string password)
    {
        for (int i = 0; i < password.Length - 2; i++)
        {
            if (password[i] == password[i + 1] && password[i] == password[i + 2])
                return true;
        }
        return false;
    }

    private static List<string> LoadCommonPasswords(string filePath)
    {
        List<string> passwords = new List<string>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Dictionary file not found: " + filePath);
            return passwords;
        }

        foreach (string line in File.ReadLines(filePath))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                passwords.Add(line.Trim());
            }
        }

        return passwords;
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