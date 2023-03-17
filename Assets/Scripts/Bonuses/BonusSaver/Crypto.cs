using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Oliasca.Maze
{
    public static class Crypto
    {
        public static string CryptoXOR(string text, int key = 115)
        {
            string result = String.Empty;
            foreach (char simbol in text)
            {
                result += (char)(simbol ^ key);
            }
            return result;
        }
    }

}
