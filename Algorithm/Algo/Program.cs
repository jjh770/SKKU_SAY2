using System;
using System.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int maxInt = int.MinValue;
        int row = 0;
        int col = 0;

        for (int i = 0; i < 9; i++)
        {
            string[] input = Console.ReadLine().Split(" ");
            int[] intInput = input.Select(int.Parse).ToArray();
            for(int j = 0; j < intInput.Length; j++)
            {
                if (intInput[j] > maxInt)
                {
                    row = i;
                    col = j;
                    maxInt = intInput[j];
                }
            }
        }
        Console.WriteLine(maxInt);
        Console.WriteLine($"{row + 1} {col + 1}");
    }
}
