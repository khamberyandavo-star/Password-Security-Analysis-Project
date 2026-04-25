using System;

namespace StrengthAnalysis
{
    public class PasswordStrengthEvaluator
    {
        public static void Evaluate(
            double entropy,
            double bruteForceTime,
            double dictionaryTime,
            double hybridTime,
            double ruleBasedTime)
        {
            int score = 0;

            score += EntropyScore(entropy);
            score += AttackScore(bruteForceTime);
            score += AttackScore(dictionaryTime) * 3;
            score += AttackScore(hybridTime) * 2;
            score += AttackScore(ruleBasedTime) * 3;

            int penalty = WeaknessPenalty(dictionaryTime, ruleBasedTime, hybridTime);

            score -= penalty;

            if (score < 0) score = 0;
            if (score > 150) score = 150;

            string label = GetLabel(score);

            PrintResult(score, label);
        }

        static int EntropyScore(double entropy)
        {
            if (entropy < 30) return 5;
            if (entropy < 50) return 15;
            if (entropy < 70) return 25;
            return 30;
        }

        static int AttackScore(double time)
        {
            if (time < 0) return 30;
            if (time < 1) return 5;
            if (time < 3600) return 10;
            if (time < 86400) return 20;
            return 30;
        }

        static int WeaknessPenalty(double dictionaryTime, double ruleBasedTime, double hybridTime)
        {
            int penalty = 0;

            if (dictionaryTime > 0 && dictionaryTime < 1)
                penalty += 60;

            if (ruleBasedTime > 0 && ruleBasedTime < 1)
                penalty += 40;

            if (hybridTime > 0 && hybridTime < 1)
                penalty += 30;

            return penalty;
        }

        static string GetLabel(int score)
        {
            if (score < 40) return "WEAK";
            if (score < 70) return "MEDIUM";
            if (score < 90) return "STRONG";
            return "VERY STRONG";
        }

        static void PrintResult(int score, string label)
        {

            Console.WriteLine(" PASSWORD STRENGTH ANALYSIS ");

            Console.WriteLine($"Score: {score}/150");
            Console.WriteLine($"Strength: {label}");

            if (label == "WEAK")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (label == "MEDIUM")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\nConclusion:");
            Console.WriteLine(GetMessage(label));

            Console.ResetColor();
        }

        static string GetMessage(string label)
        {
            return label switch
            {
                "WEAK" => "Password is easily breakable.",
                "MEDIUM" => "there is a risk of unsafety.",
                "STRONG" => "Good password.",
                "VERY STRONG" => "The best password.",
                _ => ""
            };
        }
    }
}
