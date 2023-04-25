using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
        public int get_puplic_key(int alpha,int privitKey,int q)
        {
            int puplicKey = 1;
            for (int i = 0; i < privitKey; i++)
            {
                puplicKey = (puplicKey * alpha) % q;
            }
            return puplicKey;

        }
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            int puplicKeya = get_puplic_key(alpha, xa, q);
            int puplicKeyb = get_puplic_key(alpha, xb, q);

            int keya = get_puplic_key(puplicKeyb, xa, q);
            int keyb = get_puplic_key(puplicKeya, xb, q);
            List<int> Keys = new List<int>();
            Keys.Add(keya);
            Keys.Add(keyb);

            return Keys;
            
        }
    }
}
