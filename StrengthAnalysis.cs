using System;

namespace StrengthAnalysis
{
    public class PasswordStrengthEvaluator
    {
        public static void Evaluate(double entropy, double bruteForceTime, double dictionaryTime, double hybridTime, double ruleBasedTime)
        {
            double score = 0;

            score += EntropyScore(entropy);

            score += AttackScore(bruteForceTime, 1.0);
            score += AttackScore(dictionaryTime, 3.0);
            score += AttackScore(hybridTime, 2.0);
            score += AttackScore(ruleBasedTime, 3.0);

            score -= Penalty(dictionaryTime, ruleBasedTime, hybridTime);

            score = Clamp(score, 0, 200);

            string label = GetLabel(score);

            Print(score, label);
        }

        static double EntropyScore(double entropy)
        {
            if (entropy < 20) return 5;
            if (entropy < 30) return 10;
            if (entropy < 40) return 20;
            if (entropy < 50) return 35;
            if (entropy < 60) return 50;
            if (entropy < 80) return 65;
            return 80;
        }

        static double AttackScore(double time, double weight)
        {
            if (time < 0) return 0;
            if (time < 0.1) return 80 * weight;
            if (time < 1) return 60 * weight;
            if (time < 60) return 40 * weight;
            if (time < 3600) return 20 * weight;
            if (time < 86400) return 10 * weight;
            return 5 * weight;
        }

        static double Penalty(double dict, double rule, double hybrid)
        {
            double p = 0;

            if (dict >= 0 && dict < 0.5) p += 80;
            else if (dict < 1) p += 60;

            if (rule >= 0 && rule < 0.5) p += 50;
            else if (rule < 1) p += 30;

            if (hybrid >= 0 && hybrid < 0.5) p += 30;
            else if (hybrid < 1) p += 15;

            return p;
        }

        static double Clamp(double v, double min, double max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }

        static string GetLabel(double score)
        {
            if (score < 40) return "VERY WEAK";
            if (score < 80) return "WEAK";
            if (score < 120) return "MEDIUM";
            if (score < 160) return "STRONG";
            return "VERY STRONG";
        }

        static void Print(double score, string label)
        {

            Console.WriteLine(" PASSWORD STRENGTH ANALYSIS ");

            Console.WriteLine($"Score: {score:F1} / 200");
            Console.WriteLine($"Strength: {label}");

            Console.WriteLine("\nResult:");

            if (label == "VERY WEAK")
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (label == "WEAK")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (label == "MEDIUM")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (label == "STRONG")
                Console.ForegroundColor = ConsoleColor.Blue;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(GetMessage(label));
            Console.ResetColor();
        }

        static string GetMessage(string label)
        {
            return label switch
            {
                "VERY WEAK" => "Иreakable.",
                "WEAK" => "Weak password.",
                "MEDIUM" => "Moderate strength.",
                "STRONG" => "Good security level.",
                "VERY STRONG" => "The best password",
                _ => ""
            };
        }
    }
}
