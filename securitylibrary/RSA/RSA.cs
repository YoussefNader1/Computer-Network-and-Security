using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.RSA
{
    public class RSA
    {
   
        public int Encrypt(int p, int q, int M, int e)
        {
            int n = p * q;
            int phay_n = (p - 1) * (q - 1);

            int c = 1;
            c = M % n;
            for (int i = 1; i < e; i++)
                c = (c * M) % n;

            return c;

        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int n = p * q;
            int phay_n = (p - 1) * (q - 1);
            int d, M = -1;
            for (d = 0; d < n; d++)
                if (d * e % phay_n == 1)
                    break;
            M = C % n;
            for (int i = 1; i < d; i++)
            {
                M = (M * C) % n;
            }
            return M;

        }
    }
}
