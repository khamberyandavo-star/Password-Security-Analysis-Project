using System;

namespace StrengthAnalysis
{
    public class PasswordStrengthEvaluator
    {
        public static string Evaluate(double entropy, double bruteForceTime, double dictionaryTime, double hybridTime, double ruleBasedTime)
        {
            int score = (int)Math.Clamp(entropy, 0, 100);

            int penalty = 0;

            penalty += AttackPenalty(dictionaryTime, 80);
            penalty += AttackPenalty(ruleBasedTime, 60);
            penalty += AttackPenalty(hybridTime, 40);
            penalty += AttackPenalty(bruteForceTime < 1 ? 1 : -1, 30);

            score -= penalty;

            if (score < 0) score = 0;
            if (score > 100) score = 100;

            string label = GetLabel(score);

            //PrintResult(score, label);
            return $"Score: {score}/100" + " " + $"Strength: {label}";
        }

        static int AttackPenalty(double time, int weight)
        {
            if (time >= 0 && time < 1)
                return weight;
            if (time >= 0 && time < 60)
                return weight / 2;
            return 0;
        }

        static string GetLabel(int score)
        {
            if (score < 20) return "VERY WEAK";
            if (score < 40) return "WEAK";
            if (score < 60) return "MEDIUM";
            if (score < 80) return "STRONG";
            return "VERY STRONG";
        }

        static void Print(int score, string label, double dict, double hybrid, double rule)
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine(" PASSWORD STRENGTH ANALYSIS ");
            Console.WriteLine("==================================");

            Console.WriteLine($"Score: {score}/100");
            Console.WriteLine($"Strength: {label}");

            Console.WriteLine("\nAttack Results:");

            Console.WriteLine($"Dictionary: {(dict >= 0 ? dict.ToString("F2") + " sec" : "FAILED")}");
            Console.WriteLine($"Hybrid: {(hybrid >= 0 ? hybrid.ToString("F2") + " sec" : "FAILED")}");
            Console.WriteLine($"Rule-Based: {(rule >= 0 ? rule.ToString("F2") + " sec" : "FAILED")}");

            Console.WriteLine("\nConclusion:");

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
                "VERY WEAK" => "Password is very weak.",
                "WEAK" => "Password is weak.",
                "MEDIUM" => "Password is medium.",
                "STRONG" => "Password is strong.",
                "VERY STRONG" => "Password is very strong.",
                _ => ""
            };
        }
    }
}
