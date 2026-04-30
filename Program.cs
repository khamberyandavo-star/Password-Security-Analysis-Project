using System;
using System.Collections.Generic;
using DicAttack;
using HybAttack;
using EntropyNamespace;
using BruteForceAttack;
using RuleBasedAttack;
using StrengthAnalysis;

namespace PrS2
{
    class Program
    {
        static void Main(string[] args){
          
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            double entropy = Entropy.CalculateEntropy(password);
            double bruteForceTime = BruteForce.BruteForceAttackTime(password);
            double dictionaryTime = DictionaryAttack.DictAttack(password);
            double hybridTime = HybridAttack.HybrAttack(password);
            double ruleBasedTime = RuleBased.RuleBasedAttackTime(password);

            PasswordStrengthEvaluator.Evaluate(entropy, bruteForceTime, dictionaryTime, hybridTime, ruleBasedTime);

        }
    }
}
