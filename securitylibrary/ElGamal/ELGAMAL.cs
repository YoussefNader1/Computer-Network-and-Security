using SecurityLibrary.AES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            
            
            long c1=Moduls(alpha, k, q);
            int K = Moduls(y, k, q);
            Console.WriteLine(c1);
            long c2 = (K*m)%q;
            List<long> result =new  List<long>();
            result.Add(c1);
            result.Add(c2);
            return result;

        }
        public int Decrypt(int c1, int c2, int x, int q)
        {
            ExtendedEuclid algorithm = new ExtendedEuclid();
            int k=Moduls(c1 ,x, q);
            int h = Moduls(c1, q - 1 - x, q);
            int result = (c2 * h) % q;

            return result;


        }
      
        private int  Moduls(int number, int power, int m)
        {
          
             List<int> Mod = new List<int>();
            int mod = power % 2;
            int n = (power - mod) / 2;

            for (int i=0;i<n;i++)
            {
               Mod.Add((number*number)%m);
              

            }
            if (mod == 1)
                Mod.Add(number % m);


             


            while (Mod.Count!=2)
            {
                List<int> num = new List<int>();
                for (int i=0;i<Mod.Count;i=i+2)
                {
                    int result = 0;

                  
                    if (Mod.Count > i + 1)
                    {
                        result = (Mod[i] * Mod[i + 1]) % m;

                    }
                    else
                    {
                        result = Mod[i] % m;
                       
                    }
                    num.Add(result);
                }
                Mod = num;
            
            }

         
            int  res =( Mod[0] * Mod[1])%m;
            return res;
        }
       
    }
}
