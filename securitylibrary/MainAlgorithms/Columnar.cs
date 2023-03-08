using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            List<int> key = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            String cipher = "";
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            //List<List<int>> x = Permute(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            for (int i = 4; i < plainText.Length / 3 + 1; i++)
            {
                Console.WriteLine(i);
                List<List<int>> posibleKeys = Permute(numbers(i));
                for (int j = 0; j < posibleKeys.Count; j++)
                {
                    cipher = Encrypt(plainText, posibleKeys[j]);
                    if (cipher == cipherText)
                    {
                        return posibleKeys[j];
                    }
                }
            }
            return key;

        }

        public string Decrypt(string cipherText, List<int> key)
        {
            int cipherLength = (int)Math.Ceiling((float)((float)cipherText.Length / (float)key.Count));
            char[,] cipher = new char[cipherLength, key.Count];
            Console.WriteLine("cipher No.of.rows: " + cipherLength + " No.of.columns: " + key.Count);

            int index = 0;
            int x = 0;
            for (int i = 0; i < key.Count; i++)
            {
                index = key.IndexOf(i + 1);
                for (int j = 0; j < cipherLength; j++)
                {
                    if (x < cipherText.Length)
                    {
                        cipher[j, index] = cipherText[x];
                        x++;
                    }
                    Console.WriteLine("r: " + i + " c: " + j + " index: " + x + " => " + cipher[j, index] + '\n');
                }
            }

            String plainText = "";
            for (int i = 0; i < cipherLength; i++)
            {
                for (int j = 0; j < key.Count; j++)
                {
                    plainText = plainText.Insert(plainText.Length, cipher[i, j].ToString());
                }
            }
            Console.WriteLine(plainText);
            return plainText;
        }

        public string Encrypt(string plainText, List<int> key)
        {
            int cipherLength = (int)Math.Ceiling((float)((float)plainText.Length / (float)key.Count));
            char[,] cipher = new char[cipherLength, key.Count];
            //Console.WriteLine("cipher No.of.rows: "+cipherLength+" No.of.columns: "+mainkey.Count);
            int x = 0;
            for (int i = 0; i < cipherLength; i++)
            {
                for (int j = 0; j < key.Count; j++)
                {

                    if (x >= plainText.Length)
                    {
                        //cipher[i, j] = 'x';
                        //Console.WriteLine("r: " + i + " c: " + j + " index: " + x + " => " + cipher[i, j]);
                        break;
                    }

                    cipher[i, j] = plainText[x];
                    x++;
                    //Console.WriteLine("r: " + i + " c: " + j + " index: " + x+" => " + cipher[i, j]+'\n');
                }
            }
            String cipherText = "";
            int index = 0;
            for (int i = 0; i < key.Count; i++)
            {
                index = key.IndexOf(i + 1);
                for (int j = 0; j < cipherLength; j++)
                {
                    cipherText = cipherText.Insert(cipherText.Length, cipher[j, index].ToString());
                    //Console.WriteLine("r: " + i + " c: " + j + " index: " + x + " => " + cipher[j, index] + '\n');
                }
            }
            Console.WriteLine(cipherText);
            return cipherText;
        }

        public static List<List<int>> Permute(int[] nums)
        {
            var list = new List<List<int>>();
            return DoPermute(nums, 0, nums.Length - 1, list);
        }

        public static List<List<int>> DoPermute(int[] nums, int start, int end, List<List<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref nums[start], ref nums[i]);
                    DoPermute(nums, start + 1, end, list);
                    Swap(ref nums[start], ref nums[i]);
                }
            }

            return list;
        }

        public static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
        public static int[] numbers(int number)
        {
            int[] numbers = new int[number];
            for (int i = 0; i < number; i++)
            {
                numbers[i] = i + 1;
            }
            return numbers;
        }


    }
}

