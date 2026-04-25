using System;
using System.Collections.Generic;

namespace DicAttack
{
    public class DictionaryAttack
    { 
        static int DictSize = 100000;
        static int TrySpeed = 1000000;
        static double AvgSpeed = (double)DictSize/(2*TrySpeed);         
        public double DictAttack(string password, IEnumerable<string> dictionary){
//             Console.WriteLine("DICT SIZE: " + dictionary.Count());
// Console.WriteLine("FIRST ITEM: " + dictionary.FirstOrDefault());
        
            password = password.ToLower();

            foreach(var line in dictionary){
                if(line.Trim().ToLower() == password){
                    /*
                    Console.WriteLine(line.Trim().ToLower());
                    */
                    return AvgSpeed;
                }    
            }
            
            return -1;
        }
    }
}