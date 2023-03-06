using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string NewKey(string key)
        {
            string alphabet = "abcdefghiklmnopqrstuvwxyz"; // j removed
            HashSet<char> NKey = new HashSet<char>();
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == 'j')
                {
                    NKey.Add('i');
                }
                else
                {
                    NKey.Add(key[i]);
                }
            }

            for (int i = 0; i < 25; i++)
            {
                NKey.Add(alphabet[i]); // j not exist!
            }

            string KKey = "";
            foreach (var value in NKey)
            {
                KKey += value;
            }

            return KKey;

        }

        public char[,] ModKey(string key)
        {
            int counter = 0;

            char[,] Mkey = new char[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Mkey[i, j] = key[counter++];
                }
            }

            return Mkey;
        }

        public Dictionary<char, Tuple<int, int>> KeyMatrixCharWithIndex(char[,] key)
        {
            Dictionary<char, Tuple<int, int>> KMatrix = new Dictionary<char, Tuple<int, int>>();
            //int counter = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //if (counter < 25)
                    //{
                        KMatrix.Add(key[i,j], new Tuple<int, int>(i, j));
                        //tmp.Add(Mkey.ElementAt(counter));
                        //counter++;
                    //}
                }
            }

            return KMatrix;
        }


        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public string ModifiedPlainText(string PT)
        {
            for (int i = 0; i < PT.Length - 1; i += 2)
            {
                if (PT[i] == PT[i + 1])
                {
                    PT = PT.Substring(0, i + 1) + 'x' + PT.Substring(i + 1);
                }
            }
            if (PT.Length % 2 == 1) PT += 'x';

            return PT;
        }

        public string Encrypt(string plainText, string key)
        {
            string CT = "";

            // build matrix
            string Nkey = NewKey(key);
            char[,] KeyMatrix = ModKey(Nkey);
            Dictionary<char, Tuple<int, int>> KMatrix = KeyMatrixCharWithIndex(KeyMatrix);

            //
            string PT = plainText;
            string PTMody = ModifiedPlainText(PT);
            //Console.WriteLine("Done");
            for (int i = 0; i < PTMody.Length; i+=2)
            {
                char c1 = PTMody[i], c2 = PTMody[i + 1];
                if (KMatrix[c1].Item1 == KMatrix[c2].Item1)
                {

                    CT += KeyMatrix[KMatrix[c1].Item1,(KMatrix[c1].Item2 + 1) % 5];
                    CT += KeyMatrix[KMatrix[c2].Item1,(KMatrix[c2].Item2 + 1) % 5];
                }
                else if (KMatrix[c1].Item2 == KMatrix[c2].Item2)
                {
                    CT += KeyMatrix[(KMatrix[c1].Item1 + 1) % 5, KMatrix[c1].Item2];
                    CT += KeyMatrix[(KMatrix[c2].Item1 + 1) % 5, KMatrix[c2].Item2];
                }

                else
                {
                    CT += KeyMatrix[KMatrix[c1].Item1,KMatrix[c2].Item2];
                    CT += KeyMatrix[KMatrix[c2].Item1,KMatrix[c1].Item2];
                }
            }
            CT.ToUpper();

            return CT;
        }
    }
}
