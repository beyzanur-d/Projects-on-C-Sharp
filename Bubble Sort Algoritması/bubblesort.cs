using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Kabarcik Siralama (Bubble Sort) Algoritmasi ile bir
            // dizinin elemanlarini küçükten büyükten  dogru siralar */

            int[] a = { 5, 4, 3, 2, 1 };

            int yedek,k,j,i;

            Console.Write("once : ");

            for (i = 0; i < a.Length; i++)
            {
                Console.Write(" " + a[i]);
            }

            //Bubble Sort

            for (k = 0; k < a.Length-1 ; k++)
            {
                for (j = 0; j < a.Length-1 ; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        yedek = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = yedek;
                    }
                }

                //Bubble Sort

                Console.Write("\n Sonra: ");
                for (i = 0; i < a.Length; i++)
                {
                    Console.Write(" "+a[i]);
                }

                Console.Write("\n");
                Console.ReadLine();
            }
        }
    }
}
