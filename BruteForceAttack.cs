using System;

namespace BruteForceAttack
{
    class BruteForce
    {
        static int charsetSize = 94;
        static double guessesPerSecond = 1e6;
        public static double BruteForceAttackTime(string password)
        {
            int length = password.Length;

            double searchSpace = Math.Pow(charsetSize, length);
            double averageAttempts = searchSpace / 2.0;
            double timeSeconds = averageAttempts / guessesPerSecond;

            return timeSeconds;
        }
        public static string FormatTime(double seconds)
        {
            if (seconds < 60)
                return $"{seconds:F2} seconds";

            double minutes = seconds / 60;
            if (minutes < 60)
                return $"{minutes:F2} minutes";

            double hours = minutes / 60;
            if (hours < 24)
                return $"{hours:F2} hours";

            double days = hours / 24;
            if (days < 365)
                return $"{days:F2} days";

            double years = days / 365;
            return $"{years:F2} years";
        }
    }
}