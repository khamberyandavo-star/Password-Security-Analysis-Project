using System;
using System.Collections.Generic;
using System.IO;

namespace DicAttack
{
    class DictionaryAttack
    {
        static string filePath = "data/100k-most-used-passwords-NCSC.txt";
        static int DictSize = 100000;
        static int TrySpeed = 1000000;
        static double AvgSpeed = (double)DictSize/(2*TrySpeed); 
        public static double DictAttack(string password){
            using(StreamReader reader = new StreamReader(filePath)){
                string? line;
                password = password.ToLower();

                while((line = reader.ReadLine()) != null){
                    if(line.Trim().ToLower() == password){
                        /*
                        Console.WriteLine(line.Trim().ToLower());
                        */
                        return AvgSpeed;
                    }    
                }
            }
            return -1;
        }
    }
}