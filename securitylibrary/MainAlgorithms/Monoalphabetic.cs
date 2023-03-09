using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {    
        string alphabet = "abcdefghijklmnopqrstuvwxyz";

   
        public string Analyse(string plainText, string cipherText)
        {
            string keyval = "";
            string lowerCT = cipherText.ToLower();
            string PPT = "";
            string PCT = "";
            HashSet<char> FPT = new HashSet<char>();
            HashSet<char> FCT = new HashSet<char>();
            for (int i = 0; i < plainText.Length; i++)
            {
                FPT.Add(plainText[i]);
                FCT.Add(lowerCT[i]);
            }
            for (int i = 0; i < 26; i++)
            {
                FPT.Add(alphabet[i]);
                FCT.Add(alphabet[i]);
            }

            Dictionary<char, char> key = new Dictionary<char, char>();
            foreach (var item in FPT)
            {
                PPT += item;
            }            
            foreach (var item in FCT)
            {
                PCT += item;
            }

            for (int i = 0; i < 26; i++)
            {
                key[PPT[i]] = PCT[i];

            }
            foreach (KeyValuePair<char, char> sortedDict in key.OrderBy(i => i.Key))
            {
                keyval += sortedDict.Value;
            }
            





            return keyval;
        }

        public string Decrypt(string cipherText, string key)
        {
            string PT = "";
            string lowerCipher = cipherText.ToLower();
            Dictionary<char, char> dic = new Dictionary<char, char>();
            for (int i = 0; i < key.Length; i++)
            {
                dic.Add(key[i] , alphabet[i]);
            }
            for (int i = 0; i < cipherText.Length; i++)
            {
                PT += dic[lowerCipher[i]];
            }
            return PT;
        }

        public string Encrypt(string plainText, string key)
        {
            string CT = "";
            Dictionary<char, char> dic = new Dictionary<char, char>();
            for (int i = 0; i < key.Length; i++)
            {
                dic.Add(alphabet[i], key[i]);
            }
            for (int i = 0; i < plainText.Length; i++)
            {
                CT += dic[plainText[i]];
            }
            return CT;
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            throw new NotImplementedException();
        }
    }
}
