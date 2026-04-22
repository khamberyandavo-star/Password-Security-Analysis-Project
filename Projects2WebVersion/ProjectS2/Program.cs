using System;
using System.Collections.Generic;
using DicAttack;
using HybAttack;
using EntropyNamespace;
using BruteForceAttack;
using RuleBasedAttack;

namespace PrS2
{
    class Program
    {
        /*
        static void Main(string[] args){
          
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            double entropy = Entropy.CalculateEntropy(password);
            double bruteForceTime = BruteForce.BruteForceAttackTime(password);
            double dictionaryTime = DictionaryAttack.DictAttack(password);
            double hybridTime = HybridAttack.HybrAttack(password);
            double ruleBasedTime = RuleBased.RuleBasedAttackTime(password);

            Console.WriteLine("\n--- Password Security Analysis ---");
            Console.WriteLine($"Entropy: {entropy:F2} bits");
            Console.WriteLine($"Estimated brute-force time: {BruteForce.FormatTime(bruteForceTime)}");
            if(dictionaryTime > 0)
            {
                Console.WriteLine("Your password was broken by dictionary attack in " + dictionaryTime + "s");
            }
            else
            {
                Console.WriteLine("Dictionary attack has failed");
            }
            
            if(hybridTime > 0)
            {
                Console.WriteLine("Your password was broken by Hybrid attack in " + hybridTime + "s");
            }
            else
            {
                Console.WriteLine("Hybrid attack has failed");
            }
            
            if(ruleBasedTime > 0)
            {
                Console.WriteLine("Your password was broken by Rule-Based attack in " + ruleBasedTime + "s");
            }
            else
            {
                Console.WriteLine("Rule-Based attack has failed");
            }
        }
        */
    }
}
