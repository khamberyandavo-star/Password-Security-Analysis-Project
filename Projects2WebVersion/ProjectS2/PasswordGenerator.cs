using System;
using System.Collections.Generic;

namespace PassGenerator
{
    public class Generator
    {
        public static string GenPass()
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string symbols = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            var random = new Random();
            var password = new List<char>();
        
            password.Add(lower[random.Next(lower.Length)]);
            password.Add(upper[random.Next(upper.Length)]);
            password.Add(digits[random.Next(digits.Length)]);
            password.Add(symbols[random.Next(symbols.Length)]);
            password.Add(lower[random.Next(lower.Length)]);
            password.Add(upper[random.Next(upper.Length)]);
            password.Add(digits[random.Next(digits.Length)]);
            password.Add(symbols[random.Next(symbols.Length)]);
            
            string all = lower + upper + digits + symbols;
            
            for (int i = password.Count; i < 16; i++)
            {
                password.Add(all[random.Next(all.Length)]);
            }
                
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }
    }
}