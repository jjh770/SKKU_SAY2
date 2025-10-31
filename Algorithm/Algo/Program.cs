using System;
using System.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int input = int.Parse(Console.ReadLine());
        int[] arr = new int[input];

        for (int i = 0; i < input; i++)
        {
            int num = int.Parse(Console.ReadLine());

            if (num == 0)
            {
                if (arr.Length == 0)
                {
                    Console.WriteLine(0);
                }
            }
        }
    }
}
