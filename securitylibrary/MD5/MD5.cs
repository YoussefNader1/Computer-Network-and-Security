using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.MD5
{
    public class MD5
    {
        public string GetHash(string text)
        {
            var msgBytes=Encoding.UTF8.GetBytes(text);
            string txtBIN = "";
            foreach (byte b in msgBytes)
            {
                txtBIN+=Convert.ToString(b, 2).PadLeft(8, '0');
            }
            var txtBINLen = Convert.ToString(txtBIN.Length, 2).PadLeft(8, '0');
            if (txtBIN.Length < 448)
            {
                txtBIN += "1";
                int remLen = 448 - txtBIN.Length+56;
                for (int i=0;i< remLen; i++)
                {
                    txtBIN += "0";
                }
                txtBIN += txtBINLen;
            }

            uint A = 0x67452301;
            uint B = 0xEFCDAB89;
            uint C = 0x98BADCFE;
            uint D = 0x10325476;

            List<uint> T=new List<uint>();
            for(int i = 1; i <= 64; i++)
            {
                T.Add((uint)(Math.Floor(Math.Abs(Math.Sin(i)) * Math.Pow(2, 32))));
            }

            uint F = (B & C) | ((~B) & D);
            uint G = (D & B) | ((~D) & C);
            uint H = B ^ C ^ D;
            uint I = C ^ (B | (~D));

            return "";
        }
    }
}
