using System;
using System.Collections.Generic;
using System.IO;

namespace HybAttack
{
    class HybridAttack
    {
        static string filePath = "data/100k-most-used-passwords-NCSC.txt";
        static int DictSize = 100000;
        static int TrySpeed = 1000000;
        static int VarSize = 100;
        static double AvgSpeed = ((DictSize*VarSize)/2)/TrySpeed;

        public static double HybrAttack(string password){
            using(StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                password = password.Trim().ToLower();
                string normPassword = normalisePass(password);
                int extraMargine = 2;
                int mistakesAllowed = 3;
                while((line = reader.ReadLine()) != null)
                {
                    if((isCommonPassPresent(password, line.Trim().ToLower()) >= line.Length - mistakesAllowed && password.Length <= line.Length + extraMargine) || (isCommonPassPresent(normPassword, line.Trim().ToLower()) >= line.Length - mistakesAllowed && normPassword.Length <= line.Length + extraMargine))
                    {
                        /*
                        Console.WriteLine(isCommonPassPresent(password, line.Trim().ToLower()));
                        Console.WriteLine(isCommonPassPresent(normPassword, line.Trim().ToLower()));
                        Console.WriteLine(line);
                        */
                        return AvgSpeed;
                    }
                }
            }
            return -1;
        }
        private static string normalisePass(string password)
        {
            string normalisedPassword = password.Replace('@', 'a').Replace('4', 'a').
            Replace('3', 'e').Replace('1', 'i').Replace('!', 'i').
            Replace('0', 'o').Replace('$', 's').Replace('5', 's').Replace('7', 't');
          
            return normalisedPassword;
        }
        private static int isCommonPassPresent(string password, string line)
        {
            int maxMatch = 0;

            for (int start = 0; start <= password.Length - 1; start++)
            {
            
                int matchCount = 0;

                for (int i = 0; i < line.Length && (start + i) < password.Length; i++)
                {
                    if (password[start + i] == line[i]){
                        matchCount++;
                    }
                    else{
                        break;
                    }
                }

                if (matchCount > maxMatch){
                    maxMatch = matchCount;
                }
            }

            if (maxMatch >= line.Length * 0.8){
                return maxMatch;
            }
            else{
                return -1;
            } 
        }
    }
}