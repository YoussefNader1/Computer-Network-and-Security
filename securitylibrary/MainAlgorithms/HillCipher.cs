using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

        public List<int> Decrypt2(List<int> cipherText, List<int> key)
        {
            List<int> PT = new List<int>();
            int coff = 1 / ((key[0] * key[3]) - (key[1] * key[2]));
            if (coff == 0)
                throw new InvalidAnlysisException();
            List<int> keyInverse = new List<int>();
            keyInverse.Add(key[3] * coff);
            keyInverse.Add(key[1] * -coff);
            keyInverse.Add(key[2] * -coff);
            keyInverse.Add(key[0] * coff);

            PT.AddRange(Encrypt(cipherText, keyInverse));

            return PT;
        }
        public List<int> Decrypt3(List<int> cipherText, List<int> key)
        {
            List<int> PT = new List<int>();
            
            int determinantOfKey = key[0] * (key[4] * key[8] - key[5] * key[7])
                - key[1] * (key[3] * key[8] - key[6] * key[5])
                + key[2] * (key[3] * key[7] - key[6] * key[4]);
            determinantOfKey %= 26;

            //B
            int coff_c=26-determinantOfKey;
            int equ = 1;
            int b = 0;
            float c = 0;
            while (true)
            {
                c = (float)equ / (float)coff_c;
                Console.WriteLine(c);
                if (Math.Abs(c % 1) <= (Double.Epsilon * 100))
                    break;
                equ += 26;
            }
            b = 26 - (int)c;

            //ADD AND TRANSPOSE IN ONE STEP
            List<int> keyInverse = new List<int>();
            keyInverse.Add(b * (key[4] * key[8] - key[5] * key[7]) % 26); //0
            keyInverse.Add(-b * (key[1] * key[8] - key[2] * key[7]) % 26); //3
            keyInverse.Add(b * (key[1] * key[5] - key[2] * key[4]) % 26); //6

            keyInverse.Add(-b * (key[3] * key[8] - key[5] * key[6]) % 26); //1
            keyInverse.Add(b * (key[0] * key[8] - key[2] * key[6]) % 26); //4
            keyInverse.Add(-b * (key[0] * key[5] - key[2] * key[3]) % 26); //7

            keyInverse.Add(b * (key[3] * key[7] - key[4] * key[6]) % 26); //2
            keyInverse.Add(-b * (key[0] * key[7] - key[1] * key[6]) % 26); //5
            keyInverse.Add(b * (key[0] * key[4] - key[1] * key[3]) % 26); //8

            for(int i= 0; i < keyInverse.Count; i++)
            {
                if (keyInverse[i] < 0)
                    keyInverse[i] += 26;
            }

            PT.AddRange(Encrypt(cipherText, keyInverse));

            return PT;
        }

        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            if(key.Count==4)
                return Decrypt2(cipherText,key);
            return Decrypt3(cipherText, key);
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            List<int> CT = new List<int>();
            int m = (int)Math.Ceiling(Math.Sqrt(key.Count));

            int indexOfPlain;
            int total = 0;
            int countt = 0;
            for (int i = 0; i < plainText.Count; i+=m)
            {
                indexOfPlain = i;
                for(int j = 0; j < key.Count; j++)
                {
                    total += plainText[indexOfPlain]*key[j];
                    indexOfPlain++;
                    countt++;
                    if (countt == m)
                    {
                        if (total > 0)
                            CT.Add(total % 26);
                        else
                            CT.Add((total % 26) + 26);
                        total = 0;
                        indexOfPlain = i;
                        countt = 0;
                    }
                }
            }
            return CT;
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}
