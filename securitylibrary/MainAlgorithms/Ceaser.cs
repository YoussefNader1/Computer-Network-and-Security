using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {

        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public string Encrypt(string plainText, int key)
        {

            string CT = "";
            int CT_index = 0;

            for (int i = 0; i < plainText.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (plainText[i] == alphabet[j])
                    {
                        CT_index = ((j + key) % 26);
                        CT += char.ToUpper(alphabet[CT_index]);
                        break;
                    }
                }
            }



            return CT;
        }

        public string Decrypt(string cipherText, int key)
        {
            string PT = "";
            int PT_index = 0;
            string lowerCipher = cipherText.ToLower();
            for (int i = 0; i < cipherText.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (lowerCipher[i] == alphabet[j])
                    {
                        PT_index = ((j - key)) % 26;
                        if (PT_index < 0)
                        {
                            PT_index += 26;
                        }

                        PT += alphabet[PT_index];
                    }
                }
            }

            return PT;
        }

        public int Analyse(string plainText, string cipherText)
        {
            int PT_index = 0;
            int CI_index = 0;
            string lowerCipher = cipherText.ToLower();
            char PTchar = plainText[0];
            char CIchar = lowerCipher[0];

            for (int i = 0; i < 26; i++)
            {
                if (PTchar == alphabet[i])
                {
                    PT_index = i;
                }
                if (CIchar == alphabet[i])
                {
                    CI_index = i;
                }
            }

            int key = (CI_index - PT_index)% 26;
            if (key < 0)
            {
                key += 26;
            }

            return key;
        }
    }
}
