using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            String str = "";
            int key = 0;
            for (int i = 2; i < plainText.Length; i++)
            {
                str = Encrypt(plainText, i);

                if (str == cipherText)
                {
                    return i;

                }
            }
            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            char[,] cipher = new char[key, cipherText.Length];

            int x = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = i; j < cipherText.Length; j = j + key)
                {
                    cipher[i, j] = cipherText[x];
                    x++;
                    //Console.WriteLine("r: "+i+" c: "+j+" k: "+ cipherText[x]);
                }
            }

            String plainText = "";
            int l = 0;
            while (l < cipherText.Length)
            {
                for (int k = 0; k < key; k++)
                {
                    if (l == cipherText.Length)
                        break;
                    plainText = plainText.Insert(plainText.Length, cipher[k, l].ToString());
                    l++;
                }

            }
            return plainText;
        }

        public string Encrypt(string plainText, int key)
        {

            char[,] cipher = new char[key, plainText.Length];
            int j = 0;
            while (j < (plainText.Length))
            {
                for (int i = 0; i < key; i++)
                {
                    if (j != (plainText.Length))
                    {
                        cipher[i, j] = plainText[j];
                        j++;
                    }
                }

            }

            String cipherText = "";
            for (int i = 0; i < key; i++)
            {
                for (int k = 0; k < plainText.Length; k++)
                {
                    if (cipher[i, k] != ' ' && cipher[i, k] != '\0')
                    {
                        cipherText = cipherText.Insert(cipherText.Length, cipher[i, k].ToString());
                    }
                }
            }
            Console.WriteLine(cipherText);
            return cipherText.ToUpper();
            //throw new NotImplementedException();
        }
    }
}
