using System;
using DicAttack;
using HybAttack;
using EntropyNamespace;
using BruteForceAttack;
using RuleBasedAttack;
using StrengthAnalysis;

namespace ProjectS2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            double entropy = Entropy.CalculateEntropy(password);
            double bruteForceTime = BruteForce.BruteForceAttackTime(password);
            double dictionaryTime = DictionaryAttack.DictAttack(password);
            double hybridTime = HybridAttack.HybrAttack(password);
            double ruleBasedTime = RuleBased.RuleBasedAttackTime(password);

            Console.WriteLine(" PASSWORD SECURITY ANALYSIS SYSTEM ");

            Console.WriteLine($"Entropy: {entropy:F2} bits");
            Console.WriteLine($"Brute Force: {BruteForce.FormatTime(bruteForceTime)}");
            Console.WriteLine($"Dictionary: {(dictionaryTime > 0 ? "NOT SAFE" : "SAFE")}");
            Console.WriteLine($"Hybrid: {(hybridTime > 0 ? "NOT SAFE" : "SAFE")}");
            Console.WriteLine($"Rule-Based: {(ruleBasedTime > 0 ? "NOT SAFE" : "SAFE")}\n");

            PasswordStrengthEvaluator.Evaluate(entropy, bruteForceTime, dictionaryTime, hybridTime, ruleBasedTime);
        }
    }
}
