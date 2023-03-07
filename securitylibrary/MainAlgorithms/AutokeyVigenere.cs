using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        private char[,] matrix = new char[27, 27];
        string alpha = "abcdefghijklmnopqrstuvwxyz";
        helper.vigenere_tableau table = new helper.vigenere_tableau();
        //table and key
     
       
        public string Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
       
            table.table(matrix, alpha);
            int index_pt;
            string key = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                index_pt = alpha.IndexOf(plainText[i]) + 1;
                int index = 0;
                for (int j = 1; j < 27; j++)
                {
                    if (matrix[index_pt, j] == cipherText[i])
                    {
                        index = j;

                        break;
                    }

                }
                if (key != "" && matrix[0, index] == plainText[0])
                    break;
                else
                key += matrix[0, index];

            }
          

            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            table.table(matrix, alpha);
            String decrypt = "";
            cipherText = cipherText.ToLower();
            string stream_key = key;
            int index_k;
          
            for (int i = 0; i < cipherText.Length; i++)
            {
                index_k = alpha.IndexOf(stream_key[i]) + 1;

       
                int index = 0;

                for (int j = 1; j < 27; j++)
                {


                    if (matrix[index_k, j] == cipherText[i])
                    {
                        index = j;
                       
                        break;
                    }
                }

                decrypt += matrix[0, index];
                if(stream_key.Length<cipherText.Length)
                    stream_key+= matrix[0, index]; 

            }
           
            return decrypt;
        }

        public string Encrypt(string plainText, string key)
        {
            table.table(matrix, alpha);
            string stream_key = key;
            int length_streamkeyrep = plainText.Length - key.Length;
            for (int i = 0; i < length_streamkeyrep; i++)
            {

                stream_key = stream_key + plainText[i];


            }
            string encrypt = "";
            int index_pt;
            int index_ct;

            for (int i = 0; i < stream_key.Length; i++)
            {
                index_pt = alpha.IndexOf(plainText[i]) + 1;
                index_ct = alpha.IndexOf(stream_key[i]) + 1;
                encrypt += matrix[index_pt, index_ct];

            }

            return encrypt;
        }
    }
}
