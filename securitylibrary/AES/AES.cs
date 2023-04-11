using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string plainText, string key)
        {
            string s= "0x193de3bea0f4e22b9ac68d2ae9f84808";
            string[,] plain2d = plain2dGenrator(s);
            string[,] subPlain2d = subBytes(plain2d);
            string[,] shiftPlain2d = shiftRows(subPlain2d);
            string[,] multiply_plan = mlti(shiftPlain2d);
           /* for(int i=0;i<4;i++)
            {
                for(int j=0;j<4;j++)
                {
                    Console.Write(multiply_plan[i, j] + "  ");
                }
                Console.WriteLine();
            }*/
            return null;
        }

        string[,] plain2dGenrator(string plainText)
        {
            string[,] plain2d = new string[4, 4];
            int x = 2;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    plain2d[j, i] = string.Concat(plainText[x], plainText[x + 1]);
                    x += 2;
                }
            }
            return plain2d;
        }
        public string[,] subBytes(string[,] plain2d)
        {
            string[,] subPlain2d = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    char char1 = plain2d[i, j][0];
                    char char2 = plain2d[i, j][1];
                    subPlain2d[i, j] = getFromS_Box(char1, char2);
                }
            }

            return subPlain2d;
        }
        public string getFromS_Box(char char1, char char2)
        {
            string str = null;

            int i = int.Parse(char1.ToString(), System.Globalization.NumberStyles.HexNumber);
            int j = int.Parse(char2.ToString(), System.Globalization.NumberStyles.HexNumber);

            str = S_Box[i, j];

            return str;
        }
        public static string[,] S_Box = {
            {  "63", "7c", "77", "7b", "f2", "6b", "6f", "c5", "30", "01", "67", "2b", "fe", "d7", "ab", "76" },
            { "ca", "82", "c9", "7d", "fa", "59", "47", "f0", "ad", "d4", "a2", "af", "9c", "a4", "72", "c0" },
            {  "b7", "fd", "93", "26", "36", "3f", "f7", "cc", "34", "a5", "e5", "f1", "71", "d8", "31", "15" },
            {  "04", "c7", "23", "c3", "18", "96", "05", "9a", "07", "12", "80", "e2", "eb", "27", "b2", "75" },
            {  "09", "83", "2c", "1a", "1b", "6e", "5a", "a0", "52", "3b", "d6", "b3", "29", "e3", "2f", "84" },
            {  "53", "d1", "00", "ed", "20", "fc", "b1", "5b", "6a", "cb", "be", "39", "4a", "4c", "58", "cf" },
            {  "d0", "ef", "aa", "fb", "43", "4d", "33", "85", "45", "f9", "02", "7f", "50", "3c", "9f", "a8" },
            {  "51", "a3", "40", "8f", "92", "9d", "38", "f5", "bc", "b6", "da", "21", "10", "ff", "f3", "d2" },
            {  "cd", "0c", "13", "ec", "5f", "97", "44", "17", "c4", "a7", "7e", "3d", "64", "5d", "19", "73" },
            {  "60", "81", "4f", "dc", "22", "2a", "90", "88", "46", "ee", "b8", "14", "de", "5e", "0b", "db" },
            {  "e0", "32", "3a", "0a", "49", "06", "24", "5c", "c2", "d3", "ac", "62", "91", "95", "e4", "79" },
            {  "e7", "c8", "37", "6d", "8d", "d5", "4e", "a9", "6c", "56", "f4", "ea", "65", "7a", "ae", "08" },
            {  "ba", "78", "25", "2e", "1c", "a6", "b4", "c6", "e8", "dd", "74", "1f", "4b", "bd", "8b", "8a" },
            {  "70", "3e", "b5", "66", "48", "03", "f6", "0e", "61", "35", "57", "b9", "86", "c1", "1d", "9e" },
            {  "e1", "f8", "98", "11", "69", "d9", "8e", "94", "9b", "1e", "87", "e9", "ce", "55", "28", "df" },
            {  "8c", "a1", "89", "0d", "bf", "e6", "42", "68", "41", "99", "2d", "0f", "b0", "54", "bb", "16" } };
       
        public string[,] shiftRows(string[,] subPlain2d)
        {
            string[,] shiftPlain2d = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (i == 0)
                    {
                        shiftPlain2d[i, j] = subPlain2d[i, j];
                    }
                    else { 
                    shiftPlain2d[i, (j +(4- i)) % 4] = subPlain2d[i, j];
                    }
                }
            }


            return shiftPlain2d;
        }




        //mix columns

        string[,] multiply = { { "2", "3", "1", "1" }, { "1", "2", "3", "1" }, { "1", "1", "2", "3" }, { "3", "1", "1", "2" } };
        private string[,] mlti(string[,] pt)
        {
            //convert pt_to binary
            string[,] mix_colums = new string[4, 4];
            string[,] bin = new string[4, 4];
            string[] arr = new string[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    char char1 = pt[i, j][0];
                    char char2 = pt[i, j][1];
                    string binary1 = Convert.ToString(Convert.ToInt64(char1.ToString(), 16), 2);
                    String binary2 = Convert.ToString(Convert.ToInt64(char2.ToString(), 16), 2);
                    binary1 = binary1.PadLeft(4, '0');
                    binary2 = binary2.PadLeft(4, '0');
                    string final_binary = binary1 + binary2;

                    bin[i, j] = final_binary;



                }
            }
            for (int l = 0; l < 4; l++)
            {
                for (int i = 0; i < 4; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {
                        if (multiply[i, j] == "2")
                        {
                            //shift
                            string num = bin[j, l].Substring(1) + "0";
                            if (bin[j, l][0] == '1')
                            {
                                num = xor(num, "00011011");
                            }

                            arr[j] = num;
                        }
                        else if (multiply[i, j] == "1")
                        {
                            arr[j] = bin[j, l];
                        }

                        else
                        {
                            string num = bin[j, l].Substring(1) + "0";
                            if (bin[j, l][0] == '1')
                            {
                                num = xor(num, "00011011");
                            }
                            num = xor(bin[j, l], num);
                            arr[j] = num;

                        }


                    }
                    mix_colums[i, l] = xor(arr[0], arr[1], arr[2], arr[3]);
                   
                }
            }

            return mix_colums;
        }
        private string xor(string num, string b7)
        {
            string x = "";

            for (int i = 0; i < 8; i++)
            {
                if (num[i] == b7[i])
                    x += '0';
                else
                    x += '1';
            }
            return x;
        }
        private string xor(string num1, string num2, string num3, string num4)
        {
           
            string x = "";

            for (int i = 0; i < 8; i++)
            {
                if (num1[i] == '0' && (num2[i] == '1' && num3[i] == '1' && num4[i] == '1') || num1[i] == '1' && (num2[i] == '0' && num3[i] == '0' && num4[i] == '0'))
                {
                    x += '1';
                }
                else if (num2[i] == '0' && (num1[i] == '1' && num3[i] == '1' && num4[i] == '1') || num2[i] == '1' && (num1[i] == '0' && num3[i] == '0' && num4[i] == '0'))
                {
                    x += '1';
                }
                else if (num3[i] == '0' && (num2[i] == '1' && num1[i] == '1' && num4[i] == '1') || num3[i] == '1' && (num2[i] == '0' && num1[i] == '0' && num4[i] == '0'))
                {
                    x += '1';
                }
                else if (num4[i] == '0' && (num2[i] == '1' && num3[i] == '1' && num1[i] == '1') || num4[i] == '1' && (num2[i] == '0' && num3[i] == '0' && num1[i] == '0'))
                {
                    x += '1';
                }
                else
                    x += '0';
            }
            
            string str1 = x.Substring(0, 4);
            string str2 = x.Substring(4, 4);
            string strHex = Convert.ToInt32(str1, 2).ToString("X").ToLower()+ Convert.ToInt32(str2, 2).ToString("X").ToLower();
            return strHex;
        }
        //around_key
        //{
        //  //   0     1     2     3     4     5     6     7     8      9    A     B      C    D     E    F  
        //    {0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76},//0
        //    {0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0},//1
        //    {0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15},//2
        //    {0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75},//3
        //    {0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84},//4
        //    {0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF},//5
        //    {0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8},//6
        //    {0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2},//7
        //    {0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73},//8
        //    {0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB},//9
        //    {0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79},//A
        //    {0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08},//B
        //    {0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A},//C
        //    {0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E},//D
        //    {0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF},//E
        //    {0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16} //F
        //};
    }
}
