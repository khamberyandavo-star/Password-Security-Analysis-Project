using System;

namespace EntropyNamespace
{
    class Entropy
    {   
        static int charsetSize = 94;

        public static double CalculateEntropy(string password)
        {
            int length = password.Length;
            double entropy = length * Math.Log(charsetSize, 2);
            return entropy;
        }
    }
}