using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.helper
{
    class vigenere_tableau
    {
        public void table(char [,] matrix,string alpha)
        {
          
            matrix[0, 0] = ' ';
            //first row
            for (int i = 1; i < 27; i++)
            {

                matrix[0, i] = alpha[i - 1];
            }
            //first colum
            for (int i = 1; i < 27; i++)
            {
                matrix[i, 0] = alpha[i - 1];
            }

            //
            for (int i = 1; i < 27; i++)
            {
                int index = i;
                for (int j = 1; j < 27; j++)
                {

                    matrix[i, j] = alpha[index - 1];
                    index++;
                    if (index > 26)
                        index = 1;
                }
            }
          

        }
    }
}
