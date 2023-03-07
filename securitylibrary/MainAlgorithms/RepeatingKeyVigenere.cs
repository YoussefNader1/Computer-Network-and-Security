using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        private char[,] matrix = new char[27, 27];
        private string alpha = "abcdefghijklmnopqrstuvwxyz";
        // table and key

        private string intial(string text, string key)
        {

            helper.vigenere_tableau table = new helper.vigenere_tableau();
            table.table(matrix, alpha);
            int length_streamkeyrep = text.Length - key.Length;
            string stream_key = key;
            int index = 0;
            for (int i = 0; i < length_streamkeyrep; i++)
            {

                stream_key = stream_key + key[index];
                index++;
                if (index == key.Length)
                    index = 0;

            }
            return stream_key;
        }
       
        
        public string Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            helper.vigenere_tableau table = new helper.vigenere_tableau();
            table.table(matrix, alpha);
            int index_pt;
            string key = "";
           
            for (int i=0;i<plainText.Length;i++)
            {
                index_pt = alpha.IndexOf(plainText[i])+1;
                int index = 0;
                for(int j=1;j<27;j++)
                {
                    if (matrix[index_pt, j] == cipherText[i])
                    {
                        index = j;

                        break;
                    }
                   
                }
                
                    key += matrix[0, index];
              
            }
            string check = key.Substring(0, 2);
            int remove=-1;
            for (int i = 2; i < key.Length; i++)
            {
                if(key[i]==check[0]&&key[i+1]==check[1])
                {
                    remove = i;
                    break;
                }
            }
            key = key.Replace(key, key.Substring(0,remove));
          
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
           

            string stream_key = intial(cipherText, key);
            string decrypt = "";
          
            int index_k;
            cipherText = cipherText.ToLower();
            for (int i = 0; i < stream_key.Length; i++)
            {
                index_k = alpha.IndexOf(stream_key[i]) + 1;
                int index = 0;
                
                for (int j=1;j<27;j++)
                {

                   
                    if (matrix[index_k, j] == cipherText[i])
                    {
                        index = j;
                       
                        break;
                    }
                }
                
                decrypt += matrix[0, index];

            }
          
            return decrypt;
           

        }

        public string Encrypt(string plainText, string key)
        {
            string stream_key = intial(plainText,key);

            string encrypt="";
            int index_pt;
            int index_ct;

            for( int i=0;i<stream_key.Length;i++)
            {
                index_pt = alpha.IndexOf(plainText[i])+1;
                index_ct = alpha.IndexOf(stream_key[i]) + 1;
                encrypt += matrix[index_pt, index_ct];

            }

            return encrypt.ToUpper();
        }
    }
}